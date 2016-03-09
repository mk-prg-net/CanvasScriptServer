//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.10.2012
//
//  Projekt.......: mko.BI
//  Name..........: BoWithChangeTracking.cs
//  Aufgabe/Fkt...: Implementiert eine Änderungsverfolgung für eine Geschäftsobjekt.
//                  Das Geschäftsobjekt wird über die zu überschreibende Methode GetCoreBo() bereitgestellt.
//                  Alle Eigenschaften des Geschäftsobjektes, für die eine Änderungsvervolgung bereitgestellt werden soll,
//                  solten überschrieben werden wie folgt:
//
//                  class MyChangeTracking : BoWithChangeTracking<TBo> {
//                    // Adds Changetracking for TBo.Prop1
//                    public TProp1 Prop1 {
//                        get { return GetCoreBo.Prop1;}
//                        set { SetProperty(value, (e, v) => e.Prop1 = v);}
//                  }
//
//                  Die Änderungen an einer Eigenschaft werden in einer Queue als Actions aufgezeichnet, und können jederzeit
//                  wiederholt über einem anderen Container, der ICoreData<TBo> implementiert, auf das Geschäftsobjekt angewendet werden.
//
//                  Ursprünglich Implementierung eines View, bei der die Aktualisierung auf die tatsächlich geänderten 
//                  Werte beschränkt wird. Dabei werden von den Settern Jobs in eine Queue eingestellt. Die 
//                  Jobs sind als schöngefinkelte Lambdaausdrücke realisert, die die Zuweisung des neuen Wertes an die 
//                  zugrundeliegende Entity- Eigenschaft beschreiben. 
//                  Ab dem 17.1.2015 aus dem Projekt mko.Db in mko.BI verschoben und von mkoIt.Db.BoBaseView in BoWithChangeTracking.cs umbenannt.
//                  Die Klasse wurde aus dem Datenbankkontext gelöst und zu einer Klasse mit Änderungsaufzeichnung verallgemeinert.
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 4.10.2012
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.1.2015
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.Algo.FunctionalProgramming;
using System.Diagnostics;

namespace mko.BI.ChangeTracking
{
    /// <summary>
    /// Implementiert ein Change- Tracking auf den Eigenschaften eines Geschäftsobjektes, das als Datenbasis in dieser
    /// Klasse dient
    /// </summary>
    /// <typeparam name="TBo">Typ der Datenbasis, für die das Change- Tracking durchzuführen ist</typeparam>
    public abstract class BoWithChangeTracking<TBo> : Bo.ICoreData<TBo>
    {

        /// <summary>
        /// Implementiert IWritable
        /// </summary>
        public TBo GetCore
        {
            get { return GetCoreBo(); }
        }

        /// <summary>
        /// Liefert das Geschäftsobjekt, für das eine Änderungsverfolgung hier implementiert wird
        /// </summary>
        /// <returns></returns>
        protected abstract TBo GetCoreBo();

        public enum ChangeStateEnum
        {

            // Eigenschaften wurden nicht geändert
            Unchanged,

            // Eigenschaften der View wurden geändert
            Modified,

        }

        /// <summary>
        /// Protokolliert, ob am Objekt Änderungen stattgefunden haben. 
        /// </summary>
        public ChangeStateEnum ChangeState
        {
            get
            {
                if (QueuedJobsForPropertiesUpdate.Any())
                    return ChangeStateEnum.Modified;
                else
                    return ChangeStateEnum.Unchanged;
            }
        }


        // Liste aller Updatefunktionen, die beim nächsten update abzuarbeiten sind
        public Queue<Action<TBo>> QueuedJobsForPropertiesUpdate = new Queue<Action<TBo>>();


        /// <summary>
        /// Hilfsfunktion, mittels der zuverlässig die Eigenschaft eines Objektes gelesen werden kann, falls das Objekt existiert.
        /// Wenn das Objekt nicht existiert, dann wird ein Nullable zurückgeliefert, dessen HasValue- Eigenschaft den Wert false hat.
        /// Sinn macht die Funktion beim Zugriff auf Objekte, die in einer 0-1 Beziehung zum Masterobjekt stehen. Z.B. steht das 
        /// Objekt Sterne_Planeten_Monde in einer 0-1 Beziehung zum Objekt Himmelskoerper in der KeplerDB:
        /// Himmelskoerper 1 -- 0..1 Sterne_Planeten_Monde
        /// Soll der Durchmesser eines Planeten ausgelesen werden, dann kann erfolgt dies über 
        /// Himmelskoerper.Sterne_Planeten_Monde.Aequatordurchmesser_in_km
        /// Wenn jedoch noch kein Sterne_Planeten_Monde zum Himmelskoerper existiert, dann schlägt der Zugriff fehl.
        /// GetPropertyIfObjectExists soll diesen Zugriff absichern
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertySelector"></param>
        /// <returns></returns>
        public Nullable<TProperty> GetPropertyIfObjectExists<TObject, TProperty>(TObject obj, Func<TObject, TProperty> PropertySelector)
        where TProperty : struct
        {
            return obj != null ? PropertySelector(obj) : (Nullable<TProperty>)null;
        }

        /// <summary>
        /// ... Überladung für Strings
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertySelector"></param>
        /// <returns></returns>
        public string GetPropertyIfObjectExists<TObject>(TObject obj, Func<TObject, string> PropertySelector)
        {
            return obj != null ? PropertySelector(obj) : "";
        }



        /// <summary>
        /// Fügt einen Updatejob hinzu. Ein Updatejob besteht aus dem neuen Wert und einem 
        /// Lambda- Ausdruck, der die Zuweisung des Wertes an das Datenbankentity beschreibt.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="Value"></param>
        /// <param name="UpdateFunction"></param>
        public void SetProperty<TProp>(TProp Value, Action<TProp, TBo> PropertySetter)
        {
            // Ausgewählte Eigenschaft im lokalen Entity der View setzen
            PropertySetter(Value, GetCore);

            // Aktualisierungsauftrag für externes Entity aufgeben
            var job = PropertySetter.Curry(Value);
            QueuedJobsForPropertiesUpdate.Enqueue(job);
        }


        /// <summary>
        /// Korrektere Bezeichnung: SetPropertyWithNullableValue
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="Value"></param>
        /// <param name="PropertySetter"></param>
        public void SetPropertyWithNullableValue<TProp>(Nullable<TProp> Value, Action<TProp, TBo> PropertySetter)
            where TProp : struct
        {
            if (Value.HasValue)
            {
                // Ausgewählte Eigenschaft im lokalen Entity der View setzen
                PropertySetter(Value.Value, GetCore);

                // Aktualisierungsauftrag für externes Entity aufgeben
                var job = PropertySetter.Curry(Value.Value);
                QueuedJobsForPropertiesUpdate.Enqueue(job);
            }
        }
       

        /// <summary>
        /// Alle aktualisierungsaufträge werden auf einem Entity ausgeführt, das z.B. aus einem 
        /// EF- Datenkontext einer Datenbank stammt
        /// </summary>
        /// <param name="exTBoTarget">Referenz auf externes Entity</param>
        public void UpdateExternalBo(Bo.ICoreData<TBo> exTBoTarget, bool PreserveChangeTrackingItems = false)
        {
            Debug.Assert(exTBoTarget != this);
            BoWithChangeTracking<TBo>.UpdateBo(exTBoTarget, QueuedJobsForPropertiesUpdate, PreserveChangeTrackingItems);
        }

        /// <summary>
        /// Implementieren des Löschens der Liste mit den Einträgen der Änderungsverfolgung
        /// </summary>
        public void DeleteAllChangeTrackingEntries()
        {
            QueuedJobsForPropertiesUpdate.Clear();
        }



        /// <summary>
        /// Führt alle Aktualisierungsaufträge aus der Queue auf dem Entity aus. 
        /// Wenn erfolgreich, dann ist die UpdateJobs Queue leer
        /// </summary>
        /// <param name="BoTarget">zu aktualisierendes Entity</param>
        /// <param name="QueuedJobsForPropertiesUpdate">Warteschlange mit Aktualsierungsaufträgen</param>
        public static void UpdateBo(Bo.ICoreData<TBo> BoTarget, Queue<Action<TBo>> QueuedJobsForPropertiesUpdate, bool PreserveChangeTrackingItems = false)
        {
            if (!PreserveChangeTrackingItems)
            {
                while (QueuedJobsForPropertiesUpdate.Any())
                {
                    // Der schnöngefinkelte Lambdaausdruck wird ausgeführt, wodurch die Aktualisierung der 
                    // Entity- Eigenschaft implementiert wird
                    QueuedJobsForPropertiesUpdate.Dequeue()(BoTarget.GetCore);
                }
            }
            else
            {
                UpdateBoAndPreserveChangeTrackingItems(BoTarget, QueuedJobsForPropertiesUpdate);
            }
        }

        /// <summary>
        /// Führt alle Aktualisierungsaufträge aus der UpdateJobs- Queue auf dem 
        /// Entity aus und stellt die Aktualisierungaufträge für eine wiederholte
        /// Ausführung in die Queue wieder ein.
        /// </summary>
        /// <param name="BoTarget">zu aktualisierendes Entity</param>
        /// <param name="QueuedJobsForPropertiesUpdate">Warteschlange mit Aktualisierungsaufträgen</param>
        static void UpdateBoAndPreserveChangeTrackingItems(Bo.ICoreData<TBo> BoTarget, Queue<Action<TBo>> QueuedJobsForPropertiesUpdate)
        {
            int Count = QueuedJobsForPropertiesUpdate.Count;
            for (int i = 0; i < Count; i++)
            {
                // Aktualisierungsauftrag der Warteschlange entnehmen
                var updJob = QueuedJobsForPropertiesUpdate.Dequeue();

                // Der schnöngefinkelte Lambdaausdruck wird ausgeführt, wodurch die Aktualisierung der 
                // Entity- Eigenschaft implementiert wird
                updJob(BoTarget.GetCore);
                // Ausgeführten Job wieder in Warteschlange einstellen
                QueuedJobsForPropertiesUpdate.Enqueue(updJob);
            }
        }

        
    }
}

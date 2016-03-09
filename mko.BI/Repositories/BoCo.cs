//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.11.2011
//
//  Projekt.......: mko.BI
//  Name..........: BoCoBase.cs
//  Aufgabe/Fkt...: Basisklasse für die Verarbeitung von Geschäftsobjektlisten
//                  Aus der Klasse mkoIt.DB.BoBase abgeleitet,
//                  die wiederumg aus GblDbLayer.OdsToLinq vom 13.11.2010
//                  abgeleitet wurde.
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: Herbst 2009
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.8.2011
//  Änderungen....: Integration von DynamicQuery, EntityCollection und Implementierung von 
//                  select in der Basisklasse
//
//  Version.......: 1.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.11.2011
//  Änderungen....: Erweitern des Sortieren bzgl. meherer Spalten 
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 13.3.2012
//  Änderungen....: Verschoben in Projekt mkoDb. Ziel ist die Verallgemeinerung auf 
//                  alle Projekte durch Loslösung von Web- Bibliothek.
//                  Eigenschaft SortDirection von Typ System.Web.UI.SortDirection auf 
//                  EnumSortDirection umgestellt.
//
//  Version.......: 1.4
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.5.2012
//  Änderungen....: Aufteilung in BoBase und BoBaseSqlToLinq zwecks nutzten der Basisklassenfunktionalität
//                  in Datasets <> SqlToLinq DataContext
//
//  Version.......: 1.5
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.7.2013
//  Änderungen....: Aufbauen auf Basisklasse BoBaseView, welche Änderungen aufzeichnet und an Entities nachvollzieht.
//                  Methoden GetEntitiesXXX(), GetEntitiyViewsXXX_AsQueryable() und GetEntityViews_AsObservable()... implementiert.
//                  Ersetzen die SelectXXX() Methoden.
//                  Update und Insert sind jetzt vollständig in Basisklasse implementiert.
//
//  Version.......: 1.6
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 8.8.2014
//  Änderungen....: Ersatz für SortByDerivat: 
//                  In der View sind statische Eigenschaften mit der Namenskonvention <PropertyName>_Sort anzulegen. Diese liefern
//                  im Getter einen Delegate der Art Func<IQueryable<TEntity>, bool, IQueryable<TEntity>>. Dieser Delegate stellt 
//                  den Sortierterm für die entsprechende Eigenschaft der View dar.
//                  Die Funktion Sort(...) prüft nun auf das vorhandensein einer solchen Eigenschaft. Ist sie vorhanden, dann wird mittels 
//                  des Delegate sortiert.
//                  Vorteil: Sortierlogik kann nun in der View gleich neben der Eigenschaft abgelegt, statt wie vorher weit entfernt in der 
//                  SortByDerivat. Zudem ist nun auch kein MapPropertyToColname- Attribut mehr notwendig.
//
//  Version.......: 2.0
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 3.1.2015
//  Änderungen....: Umgewandelt in abstrakte Klasse BoCoBase. Diese stellt nun ein Repository für Geschäftsobjekte dar, bei denen nicht mehr 
//                  die Darstellung in einer Datenbank/Speicher explizit berücksichtigt werden muss. In den Vorläufern war das Geschäftsobjekt 
//                  eine View (TEntityView), und seine Darstellung in der Datenbank war ein Entity (TEntity). 
//                  Jetzt werden nur noch Geschäftsobjekte direkt verwaltet. Damit lassen sich insbesondere Geschäftsobjekte nach dem Code- First Modell von 
//                  EntityFramework 6 verwalten.
//
//  Version.......: 2.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.9.2015
//  Änderungen....: In eine Implementierung der neuen Schnittstellen ICrud und IFilterAndSort verwandelt. Damit wird BoCo für 
//
//                  Dependency Injection einsetzbar
//  Version.......: 2.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 28.9.2015
//  Änderungen....: Member der Schnittstelle ICrud und IFilterAndSort umbenannt.

//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

//using System.Linq.Dynamic;


namespace mko.BI.Repositories
{

    public abstract class BoCoBase<TBo, TBoId> : Interfaces.IGetBo<TBo, TBoId>, Interfaces.ICrud<TBo, TBoId>, Interfaces.IFilterAndSort<TBo>
        where TBo : class //, new()
    {


        /// <summary>
        /// Spezielle Exceptionklasse
        /// </summary> 
        [Serializable]
        public class BoCoBaseException : System.ApplicationException
        {
            public static BoCoBaseException Create(string Message)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoCoBase", "?", "Bo: " + typeof(TBo).Name, Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoCoBaseException(msg);
            }

            public static BoCoBaseException Create(Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoBase", "?", "Bo: " + typeof(TBo).Name, ex.Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoCoBaseException(msg, ex.InnerException);
            }

            public static BoCoBaseException Create(string MethodName, Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoBase", MethodName, "Entity: " + typeof(TBo).Name, ex.Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoCoBaseException(msg, ex);
            }

            public static BoCoBaseException Create(object Obj, string MethodName, params string[] Messages)
            {
                var extMessages = new List<string>(Messages.Count() + 3);
                extMessages.Add("Entity: " + typeof(TBo).Name);
                extMessages.AddRange(Messages);
                string msg = mko.TraceHlp.FormatErrMsg(Obj, MethodName, extMessages.ToArray());
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoCoBaseException(msg);
            }


            public static BoCoBaseException Create(object Obj, string MethodName, Exception ex, params string[] Messages)
            {
                var extMessages = new List<string>(Messages.Count() + 3);
                extMessages.Add("Entity: " + typeof(TBo).Name);
                extMessages.AddRange(Messages);

                Debug.Assert(ex != null, "Im Parameter 3 (Exception ex) wurde einen null übergeben");
                extMessages.Add("Exception: " + ex.Message);
                string msg = mko.TraceHlp.FormatErrMsg(Obj, MethodName, extMessages.ToArray());
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoCoBaseException(msg, ex);
            }


            // Konstruktoren
            private BoCoBaseException(string message) : base(message) { }
            private BoCoBaseException(string message, Exception innerException) : base(message, innerException) { }
        }


        DefSortOrder<TBo> _DefaultSort;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoCoBase(DefSortOrder<TBo> DefaultSortCol)
        {
            _DefaultSort = DefaultSortCol;
        }

        //------------------------------------------------------------------------------------------
        // Sortieren


        protected List<DefSortOrder<TBo>> SortOrderDefs = new List<DefSortOrder<TBo>>();

        /// <summary>
        /// Definiert die Sortierreihenfolgen. Es kann hierarchisch nach mehreren Spalten sortiert werden.
        /// </summary>
        /// <param name="DefSortOrder"></param>
        public void DefSortOrders(params DefSortOrder<TBo>[] DefSortOrder){
            SortOrderDefs.Clear();
            SortOrderDefs.AddRange(DefSortOrder);
        }


        /// <summary>
        /// Sortieren bezüglich mehrerer Spalten wird hier implementiert
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IOrderedQueryable<TBo> MultiSort(IQueryable<TBo> tab)
        {
            if (SortOrderDefs.Any())
            {
                var firstOrder = SortOrderDefs.First();

                if (SortOrderDefs.Skip(1).Any())
                {
                    var otab = firstOrder.MainOrder(tab);
                    foreach (var defOrder in SortOrderDefs.Skip(1))
                    {
                        otab = defOrder.ThenOrder(otab);
                    }
                    return otab;
                }
                else
                    return firstOrder.MainOrder(tab);
            }
            else
                return _DefaultSort.MainOrder(tab);

        }



        //------------------------------------------------------------------------------------------
        // Filtern

        /// <summary>
        /// Liste aller Filter
        /// </summary>
        public Dictionary<Filter<TBo>, Filter<TBo>> AllFilter = new Dictionary<Filter<TBo>, Filter<TBo>>();

        /// <summary>
        /// Prüft, ob die Menge aktuell durch ein gegebenes Filter eingeschränkt wird
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public bool IsFilteredWith(Filter<TBo> Filter)
        {
            return AllFilter.ContainsKey(Filter);
        }


        /// <summary>
        /// Zuweisen eines Filter zur Liste der Filter
        /// </summary>
        /// <param name="srcFilter"></param>
        public void SetFilter(Filter<TBo> srcFilter)
        {
            AllFilter.Add(srcFilter, srcFilter);
        }

        private string RemoveFilterErrorMsg(Filter<TBo> flt)
        {
            if (string.IsNullOrEmpty(flt.Description))
                return "Ein " + flt.GetType().Name + " Filter ist bereits vorhanden";
            else
                return "Ein " + flt.GetType().Name + " Filter (" + flt.Description + ") ist bereits vorhanden";
        }

        /// <summary>
        /// Entfernt ein Filter aus dem Bo
        /// </summary>
        /// <param name="flt"></param>
        public void RemoveFilter(Filter<TBo> flt)
        {
            if (AllFilter.ContainsKey(flt))
                AllFilter.Remove(flt);
            else
                throw BoCoBaseException.Create(this, "RemoveFilter", RemoveFilterErrorMsg(flt));
        }

        public void RemoveAllFilters()
        {
            AllFilter.Clear();
        }

        /// <summary>
        /// Beschreibung der Filter zurückgeben
        /// </summary>
        /// <returns></returns>
        public string FilterDescription()
        {
            // Aufbauen der aktuellen Filterbeschreibung
            StringBuilder bldFilterDescr = new StringBuilder();
            foreach (var filterFunctor in AllFilter.Keys)
            {
                bldFilterDescr.AppendFormat("[{0}] ", filterFunctor.Description);
            }

            return bldFilterDescr.ToString();
        }

        /// <summary>
        /// Hier können vom Anwender Anmerkungen hinzugefügt werden. Diese werden 
        /// im EntitySetDescriptor aufgenommen
        /// </summary>
        /// 
        string _UserAnnotations = "";
        public string UserAnnotations
        {
            get
            {
                return _UserAnnotations;
            }
            set
            {
                _UserAnnotations = value;
            }
        }

        /// <summary>
        /// Eintrag in einem EntitySetDescriptor
        /// </summary>
        public class BoSetDescriptorEntry
        {
            public string EntryName { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// Gibt zusätzliche Informationen über die aktuell durch Filter definierte 
        /// Menge von Geschäftsobjekten zurück. Dazu gehören z.B. die Anzahl der Datensätze,
        /// Anmerkungen vom Benutzer und eine informelle Beschreibung der Filter.
        /// Diese Informationen können z.B. in Berichten im Kopf ausgegeben werden.
        /// </summary>
        /// <returns></returns>
        /// 
        Interfaces.BoSetDescriptorEntry[] Interfaces.IFilterAndSort<TBo>.GetBoSetDescriptor()
        {
            var descriptor = new List<Interfaces.BoSetDescriptorEntry>();

            descriptor.Add(new Interfaces.BoSetDescriptorEntry()
            {
                EntryName = "Anz. Datensätze",
                Description = CountFilteredBo().ToString()
            });

            descriptor.Add(new Interfaces.BoSetDescriptorEntry()
            {
                EntryName = "Angewendete Filter",
                Description = FilterDescription()
            });

            descriptor.Add(new Interfaces.BoSetDescriptorEntry()
            {
                EntryName = "Anmerkungen",
                Description = UserAnnotations
            });

            return descriptor.ToArray();
        }


        /// <summary>
        /// Muss in abgeleiteten Klassen mit der gewünschten Funktionalität zur Konstruktion eines Filters aus einer Auswahl
        /// und der Filterung überschrieben werden
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TBo> FilterBoCollection(IQueryable<TBo> tab)
        {
            if (tab == null)
                return null;
            if (AllFilter.Count == 0)
                // Fall: keine Filterung erwünscht
                return tab;
            else
            {
                // Anwenden aller Filter
                foreach (Filter<TBo> flt in AllFilter.Keys)
                {
                    // Hier wird sukkzesive eine "Where- Klausel" durch UND- Verknüpfung der Filterterme aufgebaut
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        // Test auf Id       

        /// <summary>
        /// Liefert einen Lambda- Ausdruck zurück, mittels der ein Entity auf seine ID abbildet
        /// Entity --> ID
        /// </summary>
        /// <returns></returns>
        public abstract Func<TBo, bool> GetBoIDTest(TBoId id);

        /// <summary>
        /// Erzeugt ein Filter, welches nur Datensätze passieren lässt mit der übergebenen Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public abstract Db.FilterFunctor<TEntity, TEntityId> CreateIdFilter(TEntityId Id);

        //----------------------------------------------------------------------------------------------------------------
        // Existenztests

        /// <summary>
        /// Prüft, ob die gefilterte Menge überhaupt ein Element enthält
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return BoCollection.Any();
        }

        /// <summary>
        /// Prüft, ob überhaupt ein Element in der Menge enthalten ist
        /// </summary>
        /// <returns></returns>
        public bool IsFilterdListNotEmpty()
        {
            try
            {
                return FilterBoCollection(BoCollection).Any();
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("IsFilterdListNotEmpty", ex);
            }
        }



        //----------------------------------------------------------------------------------------------------------------
        // Teilmengen von Entities oder EntityViews berechnen

        /// <summary>
        /// Zugriff auf alle Entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<TBo> GetAllBo()
        {
            try
            {
                return BoCollection;
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("GetEntities", ex);
            }
        }

        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TBo GetBo(TBoId id)
        {
            return BoCollection.FirstOrDefault(GetBoIDTest(id));
        }

        /// <summary>
        /// Wendet auf die Menge der Entities alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge
        /// </summary>
        /// <returns></returns>
        public IQueryable<TBo> GetFilteredListOfBo()
        {
            try
            {
                return FilterBoCollection(BoCollection);
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("ApplyFilter", ex);
            }
        }

        /// <summary>
        /// Wendet auf die Menge der Entites alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge. Diese wird anschließend sortiert
        /// </summary>
        /// <returns></returns>
        public IQueryable<TBo> GetFilteredAndSortedListOfBo()
        {
            try
            {
                return MultiSort(FilterBoCollection(BoCollection)).AsQueryable();
                
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("GetEntitiesFilteredAndSorted", ex);
            }
        }


        /// <summary>
        /// Zählt alle Entities nach der Filterung durch.
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public long CountFilteredBo()
        {
            try
            {
                return FilterBoCollection(BoCollection).Count();
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("CountFilteredEntities", ex);
            }
        }

        /// <summary>
        /// Zählt alle Entities durch
        /// </summary>
        /// <returns></returns>
        public long CountAllBo()
        {
            try
            {
                return BoCollection.Count();
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create("CountAllEntities", ex);
            }
        }


        //----------------------------------------------------------------------------------------------------------------
        // Einfügen, Löschen und Aktualisieren

        /// <summary>
        /// Liefert die Auflistung vom Typ TEntites aus dem EF- Objektkontext zurück
        /// </summary>
        public abstract IQueryable<TBo> BoCollection
        {
            get;
        }

        /// <summary>
        /// Erzeugt ein neues Entity. Kann bei der Implementierung eines Insert- Befehls 
        /// eines Geschäftsobjektes eingesetzt werden, um z.B. den Schlüssel des Entities zu definieren
        /// </summary>
        /// <returns></returns>
        public abstract TBo CreateBo();


        /// <summary>
        /// Hinzufügen eines Entity zu einer Entitycollection. Erst durch 
        /// SubmitChanges wird das Entity der Datenbank hinzugefügt.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void AddToCollection(TBo entity);


        /// <summary>
        /// Erzeugt ein neues Entity und fügt dieses sofort der Collection hinzu.
        /// Einsatz sinnvoll, wenn die Darstellung der Geschäftsobjekte in der Collection 
        /// erheblich von der Darstellung als TBo abweicht.
        /// </summary>
        /// <returns></returns>
        public abstract TBo CreateBoAndAddToCollection();

        /// <summary>
        /// Eine elementare Einfügeoperation
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TBo entity)
        {
            try
            {
                AddToCollection(entity);
                SubmitChanges();
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create(this, "Insert", ex);
            }
        }


        /// <summary>
        /// Ein Entity für das Löschen in der EntityCollection markieren
        /// </summary>
        /// <param name="entity"></param>
        public abstract void RemoveFromCollection(TBo entity);


        /// <summary>
        /// Löschen des durch die ID definierten Entity
        /// </summary>
        /// <param name="id"></param>
        public virtual void RemoveFromCollection(TBoId id)
        {
            try
            {
                if (BoCollection.Any(GetBoIDTest(id)))
                {
                    //var flt = CreateIdFilter(id);
                    //var entity = flt.filterImpl(EntityCollection).Single();
                    var DbEntity = BoCollection.Single(GetBoIDTest(id));
                    RemoveFromCollection(DbEntity);
                    SubmitChanges();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("ID nicht gefunden: " + id);
                }
            }
            catch (Exception ex)
            {
                throw BoCoBaseException.Create(this, "Delete", ex);
            }
        }

        /// <summary>
        /// Löschen aller Entities
        /// </summary>
        public abstract void RemoveAll();


        /// <summary>
        /// Aktualisierungen am ORMContext mit der Datenbank abgleichen
        /// </summary>
        public abstract void SubmitChanges();
        
    }
}



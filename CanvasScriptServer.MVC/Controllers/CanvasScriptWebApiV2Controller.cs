//<unit_header>
//----------------------------------------------------------------
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer.MVC
//  Name..........: CanvasScriptWebApiController.cs
//  Aufgabe/Fkt...: Implementiert das Backend für die Canvas- Edit Single Page Application
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CanvasScriptServer.MVC.Controllers
{
    public class CanvasScriptWebApiV2Controller : ApiController
    {
         ICanvasScriptServerUnitOfWork unitOfWork;

        public CanvasScriptWebApiV2Controller(ICanvasScriptServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Für ein gegeben Author wird ein Script mit einem gegebenen Namen gesucht
        /// </summary>
        /// <param name="AuthorName"></param>
        /// <param name="ScriptName"></param>
        /// <returns></returns>
        public ICanvasScript GetScriptById(string id)
        {
            var parts = id.Split(':');
            if (parts.Count() != 2)
                throw new HttpResponseException(new HttpResponseMessage() 
                            { 
                                StatusCode = HttpStatusCode.BadRequest, 
                                ReasonPhrase = "Schlüssel ungültig. Schüssel muss definiert sein durch: id=author:scriptname" 
                            });

            var script = unitOfWork.Scripts.GetBo(CanvasScriptKey.Create(parts[0], parts[1]));
            if (script == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return script;
        }

        /// <summary>
        /// Hilfsfunktion, um eine neues Zeitintervall zu erzeugen, dessen Beginn und Ende im darstellbaren Zeitraum eins
        /// Datenbankservers liegt.
        /// </summary>
        /// <returns></returns>
        private mko.BI.Bo.Interval<DateTime> CreateDateTimeInterval(){
            mko.BI.Bo.Interval<DateTime> Interval = new mko.BI.Bo.Interval<DateTime>();
            Interval.Begin = new DateTime(1900, 1, 1);
            Interval.End = new DateTime(9000, 12, 31);

            return Interval;
        }

        /// <summary>
        /// Liste aller zulässigen Querystring- Schlüssel
        /// </summary>
        public class QStringNames {

            /// <summary>
            /// Singelton
            /// </summary>
            public static QStringNames Instance{
                get{
                    if(_Instance == null)
                        _Instance = new QStringNames();
                    return _Instance;
                }
            }
            static QStringNames _Instance;

            public string AuthorName {
                get { return "author";}
            }

            public string OrderByAuthor
            {
                get { return "oderByAuthor"; }
            }

            public string ScriptNameLike{
                get{ return "script"; }                
            }

            public string OrderByScriptName
            {
                get { return "oderByScript"; }
            }

            public string CreatedBegin{
                get { return "cdateBeg";}
            }

            public string CreatedEnd {
                get { return "cdateEnd";}
            }

            public string OrderByCreated
            {
                get { return "orderByCreated"; }
            }

            public string ModifiedBegin{
                get { return "modBeg";}
            }

            public string ModifiedEnd {
                get { return "modEnd";}
            }

            public string OrderByModified
            {
                get { return "orderByModified"; }
            }
        }


        /// <summary>
        /// Alle Scripte, die bestimmten Einschränkungen, definiert im Querystring, genügen, werden zurückgegeben.        /// 
        /// </summary>
        /// <returns>Alle Scripte, die den im Querystring definierten Einschränkungen genügen</returns>
        public IEnumerable<ICanvasScript> GetScripts()
        {
            // Zugriff auf die Filterdefinitionen im Querystring
            var NameValuePairs = Request.GetQueryNameValuePairs();

            // Filter- und Sortierkriterien definieren
            var filterBuilder = unitOfWork.Scripts.getFilteredAndSortedSetBuilder();

            // Auf einen Autor mit einem gegebenen Namen einschränken
            SetFilter(NameValuePairs, QStringNames.Instance.AuthorName,  filterBuilder.defAuthor);
            SetOrderBy(NameValuePairs, QStringNames.Instance.OrderByAuthor, filterBuilder.OrderByAuthor);

            // Auf Scriptnamen einschränken, die ein Muster  enthalten
            SetFilter(NameValuePairs, QStringNames.Instance.ScriptNameLike, filterBuilder.defNameLike);
            SetOrderBy(NameValuePairs, QStringNames.Instance.OrderByScriptName, filterBuilder.OrderByName);


            // Auf Erstellungszeitraum einschränken
            SetTimeSpanFilter(filterBuilder, filterBuilder.defCreatedBetween, NameValuePairs, QStringNames.Instance.CreatedBegin, QStringNames.Instance.CreatedEnd);
            SetOrderBy(NameValuePairs, QStringNames.Instance.OrderByCreated, filterBuilder.OrderByCreated);

            // Auf Zeitraum der Modifikation einschränken
            SetTimeSpanFilter(filterBuilder, filterBuilder.defModifiedBetween, NameValuePairs, QStringNames.Instance.ModifiedBegin, QStringNames.Instance.ModifiedEnd);
            SetOrderBy(NameValuePairs, QStringNames.Instance.OrderByModified, filterBuilder.OrderByModified);           

            // Menge der Scripte filtern
            var ScriptSet = filterBuilder.GetSet();

            if (!ScriptSet.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return ScriptSet.Get();
        }

        /// <summary>
        /// Hilfsfunktion: Filter bezüglich String aus Querystringdaten definieren
        /// </summary>
        /// <param name="NameValuePairs"></param>
        /// <param name="KeyName"></param>
        /// <param name="defFilter"></param>
        private static void SetFilter(IEnumerable<KeyValuePair<string, string>> NameValuePairs, string KeyName, Action<string> defFilter)
        {
            if (NameValuePairs.Any(r => r.Key == KeyName))
                defFilter(NameValuePairs.Single(r => r.Key == KeyName).Value);
        }

        /// <summary>
        /// Hilfsfunktion: Sortierreihenfolge aus Querystringdaten ableiten
        /// </summary>
        /// <param name="NameValuePairs"></param>
        /// <param name="KeyName"></param>
        /// <param name="orderBy"></param>
        private static void SetOrderBy(IEnumerable<KeyValuePair<string, string>> NameValuePairs, string KeyName, Action<bool> orderBy)
        {
            if (NameValuePairs.Any(r => r.Key == KeyName))
                orderBy(NameValuePairs.Single(r => r.Key == KeyName).Value == "desc");
        }

        
        /// <summary>
        /// Hilfsfunktion zum definieren eines Filters aus Querystringdaten, das auf einen Zeitraum einschränkt
        /// </summary>
        /// <param name="filterBuilder"></param>
        /// <param name="defFilter"></param>
        /// <param name="NameValuePairs"></param>
        /// <param name="BeginValueName"></param>
        /// <param name="EndValueName"></param>
        private void SetTimeSpanFilter(CanvasScriptRepository.IFilteredAndSortedSetBuilder filterBuilder, Action<DateTime, DateTime> defFilter, IEnumerable<KeyValuePair<string, string>> NameValuePairs, string BeginValueName, string EndValueName)
        {
            // Filtern nach Erstellungsdatum
            {
                var IntervalBegin = CreateDateTimeInterval();
                var IntervalEnd = CreateDateTimeInterval();
                bool restricted = false;

                if (NameValuePairs.Any(r => r.Key == BeginValueName))
                {
                    restricted = true;
                    IntervalBegin.Begin = DateTime.Parse(NameValuePairs.Single(r => r.Key == BeginValueName).Value);
                }


                if (NameValuePairs.Any(r => r.Key == EndValueName))
                {
                    restricted = true;
                    IntervalEnd.End = DateTime.Parse(NameValuePairs.Single(r => r.Key == EndValueName).Value);
                }


                var Interval = IntervalBegin.Intersect(IntervalEnd);

                if (restricted && !Interval.Empty)
                {
                    defFilter(Interval.Begin, Interval.End);
                }
            }
        }


        /// <summary>
        /// Script aktualisieren
        /// </summary>
        /// <param name="scriptFromClient"></param>
        /// <returns></returns>
        public ICanvasScript Post([FromBody] Models.CanvasScriptsMgmt.ScriptSimple scriptFromClient)
        {
            var key = CanvasScriptKey.Create(scriptFromClient.userName, scriptFromClient.scriptName);

            var scriptBuilder = unitOfWork.Scripts.GetBoBuilder(key);
            scriptBuilder.setScript(scriptFromClient.scriptJson);
            unitOfWork.SubmitChanges();

            return unitOfWork.Scripts.GetBo(key);            
        }

    }
}

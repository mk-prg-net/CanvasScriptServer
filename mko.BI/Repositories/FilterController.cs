//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 10.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: FilterController.cs
//  Aufgabe/Fkt...: Basisklasse für Filtersteuerung in einer BoCoFacade
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
//  Datum.........: 5.10.2015
//  Änderungen....: Einschränkung von TBoCo auf IFilterAndSort abgeschwächt -> umbenannt von TBoCo in TFilterAndSortable
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories
{
    public abstract class FilterController<TFilter, TEntity, TEntityID, TRValue, TFilterAndSortable>
        where TFilter : mko.BI.Repositories.FilterFunctor<TEntity, TRValue>
        where TEntity : class
        where TFilterAndSortable : mko.BI.Repositories.Interfaces.IFilterAndSort<TEntity>
    {

        public FilterController(TFilterAndSortable boCo, TRValue RValueDefault)
        {
            _boCo = boCo;

            // Filter mit Standartparametern anlegen
            _Filter = CreateFilter(RValueDefault);
        }

        /// <summary>
        /// Klassenfabrik für ein neues Filter. In einer abgeleiteten Klasse, die den Kontroller für ein
        /// spezielles Filter implementiert, zu überschreiben.
        /// </summary>
        /// <param name="RValue"></param>
        /// <returns></returns>
        protected abstract TFilter CreateFilter(TRValue RValue);

        /// <summary>
        /// Innerer Zustand
        /// </summary>
        protected TFilterAndSortable _boCo;
        TFilter _Filter;

        /// <summary>
        /// Definition eines Filters
        /// </summary>
        public TRValue FilterDef
        {
            get
            {
                return _Filter.RValue;
            }
            set
            {
                // Filter darf nur im aubgeschalteten Zustand neu gesetzt werden (altes Filter in der BoCo noch gesetzt, und muss
                // erst einmal entfernt werden.
                if (On)
                    throw new Exception(mko.TraceHlp.FormatErrMsg(this, "FilterDef", "Ein neues Filter " + typeof(TFilter).Name + " darf nur neu definiert werden, wenn es deaktiviert ist"));
                else
                    _Filter = CreateFilter(value);

            }
        }

        /// <summary>
        /// Aktivieren/Deaktivieren eines Filters
        /// </summary>
        public bool On
        {
            get
            {
                return _boCo.IsFilteredWith(_Filter);
            }
            set
            {
                if (value)
                {
                    // Prüfung notwendig, um mehrfaches Zuweisen von true fehlerfrei zu ermöglichen
                    if (!_boCo.IsFilteredWith(_Filter))
                        _boCo.SetFilter(_Filter);
                }
                else
                {
                    // Prüfung notwendig, um mehrfaches Zuweisen von false fehlerfrei zu ermöglichen
                    if (_boCo.IsFilteredWith(_Filter))
                        _boCo.RemoveFilter(_Filter);
                }
            }
        }
    }
}

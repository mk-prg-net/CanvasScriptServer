//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, 2011
//
//  Projekt.......: mkoItDb
//  Name..........: FilterFunctor.cs
//  Aufgabe/Fkt...: Basisklassen von Objekten, die auf BoCo's anwendbare Filter darstellen.
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.BI.Repositories
{
    [Serializable]
    public abstract class Filter<TEntity>
    {

        public abstract IQueryable<TEntity> filterImpl(IQueryable<TEntity> srcTab);

        // Klartextbeschreibung des Filters für Dokumetationszwecke
        string _descr;
        public virtual string Description { 
            get{
                return _descr;
            }  
            set
            {
                _descr = GetType().Name + "=" + value;
            }
        }

        
    }

    [Serializable]
    public abstract class FilterFunctor<TEntity, TRvalue> : Filter<TEntity>       
    {
        // Default Konstruktor, um Instanzierung in Templates zu ermöglichen
        public FilterFunctor() { }

        public FilterFunctor(TRvalue rValueParam)
        {
            RValue = rValueParam;
        }

        public TRvalue RValue { get; set; }

        /// <summary>
        /// Rechtswert aus einer Zeichenkette einlesen
        /// </summary>
        /// <param name="RValueTxt"></param>
        public virtual void RValueParse(string RValueTxt)
        {
            throw new NotImplementedException("mko.BI.Repositories.FilterFunctor");
        }

        protected virtual string MakeFilterDescription(string Filtername)
        {
            return Filtername + "= " + RValue.ToString();
        }

        public override string Description
        {
            get
            {
                // Defaultbeschreibung, falls keine explizite Beschreibung definiert wurde
                if (string.IsNullOrEmpty(_description))
                    return MakeFilterDescription(GetType().Name);
                else
                    return _description;
            }
            set
            {
                _description = value;
            }
        }
        string _description;
       
    }
}

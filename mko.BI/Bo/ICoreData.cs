using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Bo
{
    /// <summary>
    /// Liefert eine Objekt, das die Grundlage der Datenbasis der die Schnittstelle implementierende Klassenbasis darstellt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICoreData<out T>
    {
        T GetCore { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;

namespace CanvasScriptServer.MVC.MyWebApi
{
    public class CanvasScriptServerXmlFormatterBuffered: System.Net.Http.Formatting.BufferedMediaTypeFormatter
    {
        public CanvasScriptServerXmlFormatterBuffered()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            var ret =  type == typeof(ICanvasScript);
            return ret;
        }

        public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
        {
            ICanvasScript script = (ICanvasScript)value;


            var xScript = new XElement(XName.Get("CanvasScript"),
                new XElement(XName.Get("Author"), script.AuthorName),
                new XElement(XName.Get("Name"), script.Name),
                new XElement(XName.Get("Created"), script.Created.ToString("s")),
                new XElement(XName.Get("Modified"), script.Modified.ToString("s")),
                new XElement(XName.Get("ScriptAsJson"), script.ScriptAsJson)
            );

            var xWriter = System.Xml.XmlWriter.Create(writeStream);
            var xScriptAsString = xScript.ToString();
            xScript.WriteTo(xWriter);
            xWriter.Flush();

        }

    }
}
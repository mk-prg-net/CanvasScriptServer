﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;

namespace CanvasScriptServer.MVC.MyWebApi
{
    public class CanvasScriptXmlFormatter : System.Net.Http.Formatting.MediaTypeFormatter
    {

        public CanvasScriptXmlFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type is ICanvasScript;
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            ICanvasScript script = (ICanvasScript)value;


            var xScript = new XElement(XName.Get("CanvasScript"),
                new XElement(XName.Get("Author"), script.AuthorName),
                new XElement(XName.Get("Name"), script.Name),
                new XElement(XName.Get("Created"), script.Created.ToString("s")),
                new XElement(XName.Get("Mikdified"), script.Modified.ToString("s")),
                new XElement(XName.Get("ScriptAsJson"), script.ScriptAsJson)
            );

            var xWriter = System.Xml.XmlWriter.Create(writeStream);
            var xScriptAsString = xScript.ToString();
            return System.Threading.Tasks.Task.Run(() => xScript.WriteTo(xWriter));
        }       
    }
}
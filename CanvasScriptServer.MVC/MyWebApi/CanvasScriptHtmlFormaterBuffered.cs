using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;


namespace CanvasScriptServer.MVC.MyWebApi
{
    public class CanvasScriptHtmlFormaterBuffered : System.Net.Http.Formatting.BufferedMediaTypeFormatter
    {
        public CanvasScriptHtmlFormaterBuffered()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
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
            var html = Westwind.Web.Mvc.ViewRenderer.RenderView("~/Views/CanvasScripts/View.cshtml", script);

            var writer = new System.IO.StreamWriter(writeStream);
            writer.Write(html);
            writer.Flush();           
            
        }
    }
}
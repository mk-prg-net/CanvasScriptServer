
//<unit_header>
//----------------------------------------------------------------
//
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 6.5.16
//  Änderungen....: Rückgabetypen umgestellt von String auf HttpResponseMessage
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;

namespace CanvasScriptServer.MVC.Controllers
{
    public class CanvasScriptWebApiController : ApiController
    {
        ICanvasScriptServerUnitOfWork unitOfWork;

        public CanvasScriptWebApiController(ICanvasScriptServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        public class CanvasScriptAsJsonContent : HttpContent
        {
            public CanvasScriptAsJsonContent(string ScriptAsJsonString)
            {
                Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");                
                _ScriptAsJsonString = ScriptAsJsonString;
            }
            string _ScriptAsJsonString;

            protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
            {
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                return writer.WriteAsync(_ScriptAsJsonString);
            }

            protected override bool TryComputeLength(out long length)
            {
                length = _ScriptAsJsonString.Length;
                return true;
            }
        }


        //
        public HttpResponseMessage Get(string userName, string scriptName)
        {
            var script = unitOfWork.Scripts.GetBo(CanvasScriptKey.Create(userName, scriptName));
            if(script == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //var response = Request.CreateResponse(HttpStatusCode.OK);
            var response = new HttpResponseMessage(HttpStatusCode.OK);            
            response.Content = new CanvasScriptAsJsonContent(script.ScriptAsJson);            

            return response;
        }

        public string Post([FromBody] Models.CanvasScriptsMgmt.ScriptSimple scriptFromClient)
        {
            var scriptBuilder = unitOfWork.Scripts.GetBoBuilder(CanvasScriptKey.Create(scriptFromClient.userName, scriptFromClient.scriptName));
            scriptBuilder.setScript(scriptFromClient.scriptJson);
            unitOfWork.SubmitChanges();

            return scriptFromClient.scriptJson;
        }

    }
}


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
    public class CanvasScriptWebApiController : ApiController
    {
        ICanvasScriptServerUnitOfWork<DB.Users, DB.Scripts> unitOfWork;

        public CanvasScriptWebApiController(ICanvasScriptServerUnitOfWork<DB.Users, DB.Scripts> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public string Get(string userName, string scriptName)
        {
            var script = unitOfWork.Scripts.GetBo(CanvasScriptKey.Create(userName, scriptName));
            if(script == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return script.ScriptAsJson;
        }

        public string Post([FromBody] Models.CanvasScriptsMgmt.ScriptSimple scriptFromClient)
        {
            var scriptBuilder = unitOfWork.Scripts.GetBoBuilder(CanvasScriptKey.Create(scriptFromClient.userName, scriptFromClient.scriptName));
            scriptBuilder.setScript(scriptFromClient.scriptJson);
            unitOfWork.Scripts.SubmitChanges();

            return scriptFromClient.scriptJson;
        }

    }
}

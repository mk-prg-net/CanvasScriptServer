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
        ICanvasScriptServerUnitOfWork unitOfWork;

        public CanvasScriptWebApiController(ICanvasScriptServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public string Get(string id)
        {
            var script = unitOfWork.Scripts.GetBo(id);
            if(script == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return script.ScriptAsJson;
        }

        public string Post([FromBody] Models.CanvasScriptsMgmt.ScriptSimple scriptFromClient)
        {
            var script = unitOfWork.Scripts.GetBo(scriptFromClient.name);
            script.ScriptAsJson = scriptFromClient.scriptJson;
            unitOfWork.Scripts.SubmitChanges();

            return scriptFromClient.scriptJson;
        }

    }
}

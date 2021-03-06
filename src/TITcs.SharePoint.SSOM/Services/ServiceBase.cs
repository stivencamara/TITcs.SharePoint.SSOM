﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;


namespace TITcs.SharePoint.SSOM.Services
{
    public abstract class ServiceBase : serviceBase, IHttpHandler, IRequiresSessionState
    {
        protected override void Process()
        {
            object result = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json; charset=utf-8";

            try
            {
                result = InvokeMethod();

            }
            catch (Exception e)
            {
                Logger.Logger.Unexpected("ServiceBase.ProcessRequest", e.Message);

                if (e.InnerException != null)
                {
                    Logger.Logger.Unexpected("ServiceBase.ProcessRequest.InnerException", e.InnerException.Message);
                }


                Context.Response.StatusCode = 500;
                Context.Response.TrySkipIisCustomErrors = true;

                result = Error(e);
            }

            Context.Response.Write(JsonConvert.SerializeObject(result));
        }
        
    }
}

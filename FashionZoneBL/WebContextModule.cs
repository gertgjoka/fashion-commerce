using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Objects;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL
{
    public class WebContextModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, EventArgs e)
        {

            var ctx = ((ObjectContext)HttpContext.Current.Items[Configuration.ContextKey]);

            if (ctx != null)
            {
                ctx.Dispose();
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items[Configuration.ContextKey] = new privEntities();
        }
    }
}

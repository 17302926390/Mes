using Mes.Api.App_Start;

using Swashbuckle.Application;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Mes.Api
{
    public static class SwaggerConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            // Use http://localhost:5000/swagger/ui/index to inspect API docs
            config
               .EnableSwagger(c =>
               {
                   c.SingleApiVersion("v1", "Mes.Api");
                   c.IncludeXmlComments(GetXmlCommentsPath("Mes.Model"));
                   var xmlFile = string.Format("{0}/bin/Mes.Api.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                   if (System.IO.File.Exists(xmlFile))
                   {
                       c.IncludeXmlComments(xmlFile);
                   }
                   c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                   c.CustomProvider((defaultProvider) => new SwaggerControllerDescProvider(defaultProvider, xmlFile));
               })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Mes.Api");
                    //c.EnableApiKeySupport("Authorization", "header");
                    c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Mes.Api.Scripts.SwaggerConfig.js");
                    //c.InjectJavaScript(Assembly.GetExecutingAssembly(), "Mes.Api.Scripts.api-key-header-auth.js");//取消注释是为了请求验证
                });
        }
        private static string GetXmlCommentsPath(string name)
        {
            return string.Format("{0}/bin/{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, name);
        }
    }
}
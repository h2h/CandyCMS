using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Candy.Framework.Mvc.Result
{
    /// <summary>
    /// 使用 json.net 序列化 json 对象
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
            var converters = new List<JsonConverter>();
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = converters
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            if (this.Data == null) return;

            var scriptSerialzer = JsonSerializer.Create(this.Settings);
            using (var sw = new StringWriter())
            {
                scriptSerialzer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }
}
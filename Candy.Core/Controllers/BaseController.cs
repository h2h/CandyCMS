using System.Web.Mvc;
using Candy.Framework.Mvc.Result;

namespace Candy.Core.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///  使用 JsonNetResult 替换默认的 JsonResult 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}
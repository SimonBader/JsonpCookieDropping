using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JsonpCookieDropping.Controllers
{
    public class CookieLeadsController : ApiController
    {
        [RequireHttps]
        public HttpResponseMessage Get(string vxid, string div, string zip, string cons)
        {
            var name = "cookieKey";
            var value = new {vxid, div, zip, cons};
            var valueAsJson = JsonConvert.SerializeObject(value);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var cookie = new CookieHeaderValue(name, valueAsJson)
            {
                Expires = DateTimeOffset.Now.AddMonths(1),
                Domain = Request.RequestUri.Host,
                Path = "/"
            };
            response.Headers.AddCookies(new[] { cookie });
            response.Content = new StringContent(string.Empty, Encoding.UTF8, "application/javascript");
            return response;
        }
    }
}

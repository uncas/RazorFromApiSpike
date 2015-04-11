using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RazorFromApiSpike.Web.Controllers
{
    public class ViewFromApiController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var html = await GetHtmlAsync();
            return Content(html);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var values = ReadFormValues(formCollection);
            return Content(
                string.Join(", ", values.Select(x => string.Format("({0},{1})", x.Key, x.Value))));
        }

        private static IEnumerable<KeyValuePair<string, string>> ReadFormValues(FormCollection formCollection)
        {
            return from string item in formCollection
                select new KeyValuePair<string, string>(item, formCollection[item]);
        }

        private static async Task<string> GetHtmlAsync()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:63607/api/apirenderrazor/5"))
                {
                    var viewFromApi = await response.Content.ReadAsAsync<ViewFromApi>();
                    return viewFromApi.Html;
                }
            }
        }
    }

    public class ViewFromApi
    {
        public string Html { get; set; }
    }
}
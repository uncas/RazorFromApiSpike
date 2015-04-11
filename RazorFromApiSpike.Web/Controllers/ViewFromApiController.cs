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
        public async Task<ActionResult> Index(FormCollection formCollection)
        {
            var values = ReadFormValues(formCollection).ToList();
            var validation = await PostAsync<ValidationFromApi>(values);
            if (!validation.IsValid)
                return Content("Invalid!");
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

        private static async Task<T> PostAsync<T>(IEnumerable<KeyValuePair<string, string>> values)
        {
            using (var client = new HttpClient())
            using (var content = new FormUrlEncodedContent(values))
            using (var response = await client.PostAsync("http://localhost:63607/api/apivalidate", content))
                return await response.Content.ReadAsAsync<T>();
        }
    }

    public class ValidationFromApi
    {
        public bool IsValid { get; set; }
    }

    public class ViewFromApi
    {
        public string Html { get; set; }
    }
}
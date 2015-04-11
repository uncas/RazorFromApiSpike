using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RazorFromApiSpike.Web.Controllers
{
    public class ViewFromApiController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var html = await GetHtmlAsync();
            return Content(html);
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
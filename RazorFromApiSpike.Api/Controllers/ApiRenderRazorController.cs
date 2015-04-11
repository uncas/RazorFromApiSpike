using System;
using System.Web.Http;
using RazorFromApiSpike.Api.Models;

namespace RazorFromApiSpike.Api.Controllers
{
    public class ApiRenderRazorViewModel
    {
        public ApiRenderRazorViewModel(DateTime date, string postBackUrl)
        {
            Date = date;
            PostBackUrl = postBackUrl;
        }

        public DateTime Date { get; private set; }
        public string PostBackUrl { get; private set; }
    }

    public class ApiRazorResult
    {
        public ApiRazorResult(string html)
        {
            Html = html;
        }

        public string Html { get; private set; }
    }

    public class ApiRenderRazorController : ApiController
    {
        // GET api/apirenderrazor/5
        public ApiRazorResult Get(int id)
        {
            return new ApiRazorResult(RenderHtml());
        }

        private static string RenderHtml()
        {
            return ViewRenderer.RenderView(
                "~/Views/ApiRenderRazor/Index.cshtml",
                new ApiRenderRazorViewModel(DateTime.Now, "/ViewFromApi"),
                true);
        }
    }
}
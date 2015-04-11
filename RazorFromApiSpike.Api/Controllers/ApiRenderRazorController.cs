using System;
using System.Web.Http;
using RazorFromApiSpike.Api.Models;

namespace RazorFromApiSpike.Api.Controllers
{
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

    public class ApiValidateController : ApiController
    {
        public ApiValidateResult Post(ApiValidateRequest request)
        {
            return new ApiValidateResult(request.IsValid());
        }
    }

    public class ApiValidateRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        public bool IsValid()
        {
            return IsValid(Name) && IsValid(Address) && IsValid(Country);
        }

        private static bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }

    public class ApiValidateResult
    {
        public ApiValidateResult(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsValid { get; private set; }
    }
}
using System;

namespace RazorFromApiSpike.Api.Models
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
}
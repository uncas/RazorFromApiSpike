namespace RazorFromApiSpike.Api.Models
{
    public class ApiRazorResult
    {
        public ApiRazorResult(string html)
        {
            Html = html;
        }

        public string Html { get; private set; }
    }
}
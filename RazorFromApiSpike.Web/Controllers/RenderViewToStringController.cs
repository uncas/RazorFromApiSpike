﻿using System;
using System.IO;
using System.Web.Mvc;

namespace RazorFromApiSpike.Web.Controllers
{
    /// <remarks>
    ///     See http://www.codemag.com/Article/1312081 listing 1:
    /// </remarks>
    public class RenderViewToStringController : Controller
    {
        public ActionResult Index()
        {
            var html = RenderViewToString(
                ControllerContext,
                "~/Views/RenderViewToString/Index.cshtml",
                DateTime.Now);
            return Content(html);
        }

        private static string RenderViewToString(ControllerContext context,
            string viewPath,
            object model = null,
            bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                    context.Controller.ViewData,
                    context.Controller.TempData,
                    sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
}
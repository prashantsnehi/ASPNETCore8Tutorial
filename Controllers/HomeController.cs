﻿using System.Text;
using System.Text.Json;
using ControllerExamples.CustomModelBinder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerExamples.Controllers
{
    //public class HomeController : Controller
    //{
    //    // GET: /<controller>/
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}

    public class HomeController : Controller
    {
        [Route("/")]
        public async Task<ContentResult> Index([FromHeader(Name = "User-Agent")] string userAgent)
        {
            string? accept = HttpContext.Request.Headers["Accept"],
                acceptEncoding = HttpContext.Request.Headers["Accept-Encoding"],
                acceptLanguage = HttpContext.Request.Headers["Accept-Language"],
                connection = HttpContext.Request.Headers["Connection"],
                host = HttpContext.Request.Headers["Host"];
            StringBuilder str = new StringBuilder();
            str.Append($"<h1>Hello from Hello Method</h1>")
               .Append("<ol>")
               .Append($"<li>Host: <span style='color:red; background-color: yellow;'>{host}</span></li>")
               .Append($"<li>SessionId: <span style='color: red; background-color: yellow;'>{HttpContext.Session.Id}</li>")
               .Append($"<li>Browser: <span style='color:red; background-color: yellow;'>{userAgent}</span></li>")
               .Append($"<li>Accept: <span style='color:red; background-color: yellow;'>{accept}</span></li>")
               .Append($"<li>Accept Encoding: <span style='color:red; background-color: yellow;'>{acceptEncoding}</span></li>")
               .Append($"<li>Accept Language: <span style='color:red; background-color: yellow;'>{acceptLanguage}</span></li>")
               .Append($"<li>Connection: <span style='color:red; background-color: yellow;'>{connection}</span></li>")
               .Append("</ol>");

            return await Task.FromResult(Content(str.ToString(), "text/html"));
        }

        [Route("getPersonDetail")]
        public async Task<JsonResult> GetPersonDetail() =>
            await Task.FromResult(Json(new Person() {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Prashant",
                LastName = "Snehi", Age = 46, Phone = "9810013306",
                Password = "abcd@123",
                ConfirmPassword = "abcd@123"
            }));

        [Route("fileDownload1")]
        public async Task<FileResult> fileDownload1() =>
            await Task.FromResult(new VirtualFileResult("/amazon.jpeg", "image/jpeg"));

        [Route("fileDownload2")]
        public async Task<FileResult> fileDownload2() =>
            await Task.FromResult(new PhysicalFileResult("/Users/psnehi/Pictures/amazon.jpeg", "image/jpeg"));

        [Route("fileDownload3")]
        public async Task<FileResult> fileDownload3() =>
            await Task.FromResult(new FileContentResult(System.IO.File.ReadAllBytes("/Users/psnehi/Pictures/amazon.jpeg"), "image/jpeg"));

        [Route("fileDownload4")]
        public async Task<FileResult> fileDownload4() =>
            await Task.FromResult(File(System.IO.File.ReadAllBytes("/Users/psnehi/Pictures/amazon.jpeg"), "image/jpeg"));
        [Route("persons")]
        [HttpPost]
        //[Bind(nameof(Person.FirstName), nameof(Person.LastName), nameof(Person.Email))]
        //[ModelBinder(BinderType = typeof(PersonModelBinder))]
        public async Task<IActionResult> Persons([FromBody][ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        {
            person.SessionId = HttpContext.Session.Id;
           
            if (!ModelState.IsValid)
            {
                /*
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var errorMsg in value.Errors)
                    {
                        errors.Add(errorMsg.ErrorMessage);
                    }
                }
                string error = string.Join("\n", errors);
                */
                string error = string.Join("\n", ModelState.SelectMany(val => val.Value.Errors)
                    .Select(err => err.ErrorMessage));

                return await Task.FromResult(BadRequest(error));
            }
            return await Task.FromResult(Json(person));
        }
    }
}


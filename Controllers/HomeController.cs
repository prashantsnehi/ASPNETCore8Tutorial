using Microsoft.AspNetCore.Mvc;

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
        public async Task<ContentResult> Index() =>
            await Task.FromResult(Content( "<h1>Hello from HelloMethod</h1>", "text/html"));

        [Route("person")]
        public async Task<JsonResult> Person() =>
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
        public async Task<IActionResult> Persons([FromBody] Person model)
        {
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
            return await Task.FromResult(Json(model));
        }
    }
}


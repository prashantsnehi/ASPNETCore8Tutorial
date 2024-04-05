using System.Text;
using System.Text.Json;
using ControllerExamples.CustomModelBinder;
using ControllerExamples.Model;
using ControllerExamples.ServiceContracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Options;
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
        private readonly ICityServices _cityServices;
        private readonly ICityServices _cityServices1;
        private readonly ICityServices _cityServices2;
        private readonly ICityServices _cityServices3;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IWeatherService _weatherService;



        // use of weatherapiconfig from appsettins.json as service
        private readonly WeatherAppConfig _config;
        private readonly OtherSettings _otherSettings;
        private readonly IHttpClientFactory _httpClient;

        public HomeController(ICityServices cityServices,
            ICityServices cityServices1,
            ICityServices cityServices2,
            ICityServices cityServices3,
            IWebHostEnvironment webHostEnvironment,
            IOptions<WeatherAppConfig> config,
            IOptions<OtherSettings> otherSettings,
            IHttpClientFactory httpClient,
            IWeatherService weatherService)
        {
            _cityServices = cityServices;
            _cityServices1 = cityServices1;
            _cityServices2 = cityServices2;
            _cityServices3 = cityServices3;
            _webHostEnvironment = webHostEnvironment;
            _config = config.Value;
            _otherSettings = otherSettings.Value;
            _httpClient = httpClient;
            _weatherService = weatherService;
        }

        //[Route("/")]
        public async Task<IActionResult> Index([FromHeader(Name = "User-Agent")] string userAgent)
        {
            //string? accept = HttpContext.Request.Headers["Accept"],
            //    acceptEncoding = HttpContext.Request.Headers["Accept-Encoding"],
            //    acceptLanguage = HttpContext.Request.Headers["Accept-Language"],
            //    connection = HttpContext.Request.Headers["Connection"],
            //    host = HttpContext.Request.Headers["Host"];
            //StringBuilder str = new StringBuilder();
            //str.Append($"<h1>Hello from Hello Method</h1>")
            //   .Append("<ol>")
            //   .Append($"<li>Host: <span style='color:red; background-color: yellow;'>{host}</span></li>")
            //   .Append($"<li>SessionId: <span style='color: red; background-color: yellow;'>{HttpContext.Session.Id}</li>")
            //   .Append($"<li>Browser: <span style='color:red; background-color: yellow;'>{userAgent}</span></li>")
            //   .Append($"<li>Accept: <span style='color:red; background-color: yellow;'>{accept}</span></li>")
            //   .Append($"<li>Accept Encoding: <span style='color:red; background-color: yellow;'>{acceptEncoding}</span></li>")
            //   .Append($"<li>Accept Language: <span style='color:red; background-color: yellow;'>{acceptLanguage}</span></li>")
            //   .Append($"<li>Connection: <span style='color:red; background-color: yellow;'>{connection}</span></li>")
            //   .Append("</ol>");

            var requestHeaders = new DefaultInformations()
            {
                Accept = HttpContext.Request.Headers["accept"],
                AcceptEncoding = HttpContext.Request.Headers["Accept-Encoding"],
                AcceptLanguage = HttpContext.Request.Headers["Accept-Language"],
                Connection = HttpContext.Request.Headers["Connection"],
                Host = HttpContext.Request.Headers["Host"],
                UserAgent = userAgent
            };

            ViewBag.InstanceId = _cityServices.InstanceId;
            ViewBag.InstanceId1 = _cityServices1.InstanceId;
            ViewBag.InstanceId2 = _cityServices2.InstanceId;
            ViewBag.InstanceId3 = _cityServices3.InstanceId;

            ViewBag.Cities = await _cityServices.GetCities();
            //var weatherConfig = _config.GetSection("WeatherAppConfig").Get<WeatherAppConfig>();

            ViewBag.WeatherConfig = _config;
            ViewBag.OtherSettings = _otherSettings;


            return await Task.FromResult(View(requestHeaders));
            //return await Task.FromResult(Content(str.ToString(), "text/html"));
        }

        //[Route("getPersonDetail")]
        public async Task<JsonResult> GetPersonDetail() =>
            await Task.FromResult(Json(new Person()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Prashant",
                LastName = "Snehi",
                Age = 46,
                Phone = "9810013306",
                Password = "abcd@123",
                ConfirmPassword = "abcd@123"
            }));

        //[Route("fileDownload1")]
        public async Task<FileResult> fileDownload1() =>
            await Task.FromResult(new VirtualFileResult("/amazon.jpeg", "image/jpeg"));

        //[Route("fileDownload2")]
        public async Task<FileResult> fileDownload2() =>
            await Task.FromResult(new PhysicalFileResult("/Users/psnehi/Pictures/amazon.jpeg", "image/jpeg"));

        //[Route("fileDownload3")]
        public async Task<FileResult> fileDownload3() =>
            await Task.FromResult(new FileContentResult(System.IO.File.ReadAllBytes("/Users/psnehi/Pictures/amazon.jpeg"), "image/jpeg"));

        //[Route("fileDownload4")]
        public async Task<FileResult> fileDownload4() =>
            await Task.FromResult(File(System.IO.File.ReadAllBytes("/Users/psnehi/Pictures/amazon.jpeg"), "image/jpeg"));

        //[Route("persons")]
        [HttpPost]
        //[Bind(nameof(Person.FirstName), nameof(Person.LastName), nameof(Person.Email))]
        //[ModelBinder(BinderType = typeof(PersonModelBinder))]
        public async Task<IActionResult> Persons([FromBody] Person person)
        {
            person.SessionId = HttpContext.Session.Id;
            person.PersonName = string.Concat(person.FirstName, " ", person.LastName);

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
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return await Task.FromResult(Json(person, options));
        }

        //[Route("get-weather-info")]
        [HttpGet]
        public async Task<IActionResult> GetWeatherInfo()
        {
            var requestHeaders = new DefaultInformations()
            {
                Accept = HttpContext.Request.Headers["accept"],
                AcceptEncoding = HttpContext.Request.Headers["Accept-Encoding"],
                AcceptLanguage = HttpContext.Request.Headers["Accept-Language"],
                Connection = HttpContext.Request.Headers["Connection"],
                Host = HttpContext.Request.Headers["Host"],
                UserAgent = HttpContext.Request.Headers["User-Agent"]
            };

            var response = await _weatherService.GetWeatherForcast();
            var responseString = await response.Content.ReadAsStringAsync();

            //return await Task.FromResult(Json(JsonSerializer.Serialize(requestHeaders,
            //    new JsonSerializerOptions()
            //    {
            //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //    })));
            return await Task.FromResult(Json(responseString));
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentInfo([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.SelectMany(val => val.Value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
                

            return await Task.FromResult(Json(JsonSerializer.Serialize(student)));
        }

    }
}


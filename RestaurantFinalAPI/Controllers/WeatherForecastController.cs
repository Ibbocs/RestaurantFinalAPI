using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.UserExceptions;
using RestaurantFinalAPI.Application.Models.ViewModels.UserVMS;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin,User")]
        public IEnumerable<WeatherForecast> Get()
        {


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult ChechValidation([FromQuery]VMUserCreate? model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }
            return BadRequest();
            
        }

        [HttpGet("check-log-and-ex-handling")]
        public IActionResult ExpcHandlingTest()
        {
            

            try
            {
                //try icindeki xeta exception handilinge getmir eger cacth olan ex gondermesem. Oz yaratdigim exception garax try cixacaq xetaya uygun olmalidi.
                checked
                {
                    int a = 568;
                    byte b = (byte)a;
                }

                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionDTO Dto = new();
                Dto.Message = "Dtodan hata message";

                //throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.UserCreateFailed()); - isledi
                throw new UserCreateFailedException("Baki");
                //Dto.Message = "Dtodan hata message";
                //Dto.InnerException = ex.InnerException ?? null;
                //throw new Application.Exceptions.UserExceptions.UserCreateFaildException("john_doe");

                //throw CustomExceptionFactory.CreateUserCreateFailedException();
                //throw new NotFoundUserException(ex.Message, ex.InnerException); //bele yazanda menin verdiyim xeta mesajin cixarmayacaq, ex geleni cixardacaq
                //throw new Exception(ex.Message, ex.InnerException);
                return BadRequest(); //bunu yazanda exception atmir, loga da yazmir 400 xeta kodu donur
            }
        }
    }
}
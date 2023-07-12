using ASP.NET_Core依赖注入详解.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Controllers
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
        // 定义私有的只读字段
        private readonly ICharacterRepository _characterRepository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICharacterRepository  characterRepository)
        {
            _logger = logger;
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            // 如果不存在则添加
            if (!_characterRepository.ListAll().Any())
            {
                //Character character = new Character() { CharList}
                //_characterRepository.Add(new Character(CharList: "Tom") {});
                _characterRepository.Add(new Character("Tom"));
                _characterRepository.Add(new Character("Jack"));
                _characterRepository.Add(new Character("Kevin"));
            }


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
            })
            .ToArray();
        }
    }
}

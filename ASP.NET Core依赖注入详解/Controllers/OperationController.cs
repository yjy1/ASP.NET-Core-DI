using ASP.NET_Core依赖注入详解.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationController : ControllerBase
    {
        // 定义私有字段
        private readonly IOperationTransientRepository _transientRepository;
        private readonly IOperationScopeRepository _scopeRepository;
        private readonly IOperationSingletonRepository _singletonRepository;

        /// <summary>
        /// 通过构造函数实现注入
        /// </summary>
        /// <param name="transientRepository"></param>
        /// <param name="scopeRepository"></param>
        /// <param name="singletonRepository"></param>
        public OperationController(IOperationTransientRepository transientRepository,
            IOperationScopeRepository scopeRepository,
            IOperationSingletonRepository singletonRepository)
        {
            _transientRepository = transientRepository;
            _scopeRepository = scopeRepository;
            _singletonRepository = singletonRepository;
        }
         
      
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //773d919d - 9eef - 4960 - a5cc - d52358fbd7b6
            string TransientGuid = _transientRepository.GetOperationId().ToString();
            //8e13f90b - d5bc - 4f00 - bd7d - c8293ba99e6d
            string ScopedGuid = _scopeRepository.GetOperationId().ToString();
            //e859b898 - f961 - 4604 - af92 - 805acb3b6246
            string SingletonGuid = _singletonRepository.GetOperationId().ToString();

            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                TransientGuid = TransientGuid,
                ScopedGuid = ScopedGuid,
                SingletonGuid = SingletonGuid,
            })
            .ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperDemoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        /// <summary>
        /// 消息发布者
        /// </summary>
        private readonly ICapPublisher _publisher;

        public PublishController(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpGet]
        public IActionResult WithoutTransaction()
        {
            _publisher.Publish("ERS.service.runn.task.time", DateTime.Now);
            return Ok();
        }

        [NonAction]
        //订阅消息
        [CapSubscribe("ERS.service.runn.task.time")]
        private Task CheckReceivedMsg(DateTime time)
        {
            Console.WriteLine(time);
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.International.Converters.PinYinConverter;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinYinConvertController : Controller
    {
        /// <summary>
        /// 简体中文转拼音
        /// </summary>
        /// <param name="simpleCHWord"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string simpleCHWord)
        {
            if (string.IsNullOrEmpty(simpleCHWord))
                return Content($"简体中文：{simpleCHWord},拼音：暂无");
            var pinyinBuilder = new StringBuilder();
            foreach (char obj in simpleCHWord)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    var pinyin = chineseChar.Pinyins[0].ToString();
                    pinyinBuilder.Append(pinyin.Substring(0, pinyin.Length - 1));
                }
                catch
                {
                    pinyinBuilder.Append(obj.ToString());
                }
            }
            return Content($"简体中文：{simpleCHWord},拼音：{pinyinBuilder.ToString()}");
        }
    }
}

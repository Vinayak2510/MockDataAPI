using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MockDataAPI.NewFolder;
using Newtonsoft.Json;

namespace MockDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        [HttpPost]
        public IActionResult Post([FromBody] InputDTO mockObject)
        {
            string input = JsonConvert.SerializeObject(mockObject.Fields);
            var dic = JsonConvert.DeserializeObject<Dictionary<string,Object>>(input);
            Random rnd = new Random();
            List<Object> list = new List<Object>();
            for (int i = 0; i < mockObject.Count; i++)
            {
                var dic1 = new Dictionary<string, Object>();
                foreach (var x in dic.Keys)
                {
                    switch (dic[x])
                    {
                        case "int":
                            dic1[x] = rnd.Next();
                            break;
                        case "string":
                            dic1[x] = "x";
                            break;
                        default:
                            dic1[x] = new DateTime();
                            break;
                    }
                }
                list.Add(dic1);
            }
            var result = JsonConvert.SerializeObject(list);
            return Ok(list);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class ModelFactory
    {
        public string Serialise(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return json;
        }

        public T Deserialise<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

    }
}
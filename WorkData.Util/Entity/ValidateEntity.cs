using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WorkData.Util.Entity
{
    public class ValidateEntity
    {
        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

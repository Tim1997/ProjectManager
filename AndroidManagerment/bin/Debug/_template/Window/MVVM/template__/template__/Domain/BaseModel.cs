using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__.Domain
{
    /// <summary>
    /// lớp cơ sở
    /// </summary>
    public class BaseModel : BaseBinding
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("LastUpdateDate")]
        public DateTime LastUpdateDate { get; set; }
    }
}

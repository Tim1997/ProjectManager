using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Base
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

    /// <summary>
    /// gọi sự kiện thông báo thay đổi giá trị 
    /// </summary>
    public class BaseBinding : INotifyPropertyChanged
    {
        public object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

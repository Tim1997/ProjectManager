using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProjectInfoDAO : Base.BaseModel
    {
        private string title;
        private string path;

        public string Title { get => title; set { title = value; OnPropertyChanged(); } }
        public string Path { get => path; set { path = value; OnPropertyChanged(); } }
    }
}

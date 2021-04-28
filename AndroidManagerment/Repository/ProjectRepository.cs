using Base;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProjectRepository : ManagerDB<ProjectDAO>
    {
        public ProjectRepository(string name)
        {
            InitCollection(name);
        }
    }
}

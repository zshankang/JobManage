using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManage.Job
{
    public class TestJob : Lib.IJob
    {
        public bool Exceute()
        {            
            return true;
        }
    }
}

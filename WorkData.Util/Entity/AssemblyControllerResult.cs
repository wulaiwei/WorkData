using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkData.Util.Entity
{
    public class AssemblyControllerResult
    {
        public AssemblyControllerResult()
        {
            this.Actions = new List<ActionDescriptionAttribute>();
        }

        /// <summary>  
        /// 控制器名称 
        /// </summary>  
        public string ControllerName { get; set; }


        /// <summary>  
        /// 控制器 Action 
        /// </summary>  
        public List<ActionDescriptionAttribute> Actions { get; set; }
    }
}

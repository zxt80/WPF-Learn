using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 系统导航实体菜单
    /// </summary>
    public class MenuBar:BindableBase
    {
        //图标
        public string Icon { get; set; }

        // 名称
        public string Title { get; set; }

        // 命名空间
        public string NameSpace { get; set; }
    }
}

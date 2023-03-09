using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 绑定3
{
    class BaseViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(
            [CallerMemberName] string propName="",          // 获取调用函数（准确讲应该是成员）名称
            [CallerFilePath] string sourceFilePath = "",    // 获取调用者所在文件
            [CallerLineNumber] int sourceLineNumber = 0)    // 获取调用者所在行号
        {
            this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propName));
        }
    }
}

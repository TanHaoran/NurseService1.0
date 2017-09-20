using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sxnqcc
{
    /// <summary>
    /// 枚举按钮状态
    /// </summary>
    public enum State
    {
        Normal = 1,//按钮默认时
        MouseOver = 2,//鼠标移上按钮时
        MouseDown = 3,//鼠标按下按钮时
        Disable = 4,//当不启用按钮时（也就是按钮属性Enabled==Ture时）
        Default = 5//控件得到Tab焦点时
    }

    public enum FormState
    {
        Browse,
        Add,
        Modify
    }
}

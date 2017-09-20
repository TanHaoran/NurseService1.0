using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sxnqcc
{
    public interface IBusinessControl
    {
        void DataBind();
        bool IsRefresh { get; set; }
    }
}

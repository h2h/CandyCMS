using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Framework
{
    public partial interface IWebHelper
    {
        /// <summary>
        /// 映射虚拟路径的物理磁盘路径。
        /// </summary>
        string MapPath(string path);
    }
}

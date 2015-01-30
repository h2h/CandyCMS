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
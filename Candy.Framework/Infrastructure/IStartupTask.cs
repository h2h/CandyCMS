namespace Candy.Framework.Infrastructure
{
    /// <summary>
    /// 运行任务接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        void Execute();

        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
    }
}
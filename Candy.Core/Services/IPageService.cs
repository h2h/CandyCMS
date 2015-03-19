using Candy.Core.Domain;

namespace Candy.Core.Services
{
    public partial interface IPageService
    {
        /// <summary>
        /// 根据 Id 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Page Get(int id);

        void Create(Page entity);

        void Delete(Page entity);

        void Delete(int id);
    }
}
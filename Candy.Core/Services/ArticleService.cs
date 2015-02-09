using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Candy.Core.Domain;
using Candy.Framework.Data;

namespace Candy.Core.Services
{
    public partial class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        public ArticleService(IRepository<Article> articleRepository)
        {
            this._articleRepository = articleRepository;
        }
        public void Create(Article entity)
        {
            this._articleRepository.Insert(entity);
        }

    }
}

using BlogApp.DAL.Models;
using BlogApp.DAL.UoW;
using BlogApp.DAL.UoW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class ArticleRepository : Repository<ArticleEntity>
    {
        public ArticleRepository(BlogDBContext db) : base(db)
        {
        }

        public async Task PostArticleAsync(ArticleEntity article)
        {
           
        }

        public async Task GetArcticleByAuthorAsync(Guid id)
        {
            
        }


    }
}

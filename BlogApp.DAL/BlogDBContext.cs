using Azure;
using BlogApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL
{
    //Пока без identity
    public class BlogDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

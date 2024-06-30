using BlogApp.DAL.Entities;
using BlogApp.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL
{
    public class BlogDBContext : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEntity>()
                .HasIndex(e => e.NormalizedEmail)
                .IsUnique();

            //builder.Entity<ArticleEntity>();
            //builder.Entity<UserEntity>();

            base.OnModelCreating(builder);
        }
    }
}

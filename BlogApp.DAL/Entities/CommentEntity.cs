using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Models
{
    public class CommentEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //Связь со статьей (один-ко-многим)
        public Guid ArticleID { get; set; } = new Guid();
        public ArticleEntity Article {  get; set; } = new ArticleEntity();

        //Связь с комментатором (пользователями) один-ко-многим (у юзера >=1 коммент)
        public string UserId { get; set; } = string.Empty;
        public UserEntity User { get; set; } = new UserEntity();
    }
}

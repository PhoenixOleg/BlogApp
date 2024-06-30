using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Models
{
    public class ArticleEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }

        public DateTime? PostDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        //Связь с авторами (пользователями) один-ко-многим (подразумеваем личный блог)
        public string? UserId { get; set; } 
        public UserEntity? User { get; set; } //= new UserEntity();

        //Связь с тегами (многие-ко-многим)
        public List<TagEntity> Tags { get; set; } = [];

        //Связь с комментами (один-ко-многим)
        public List<CommentEntity> Comments { get; set; } = [];

        //ToDo можно добавить и лайки/дизлайки, если ума хватит

        //ToDo можно сделать признак скрытия статьи модератором или автором для доработки
    }
}

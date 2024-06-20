using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Models
{
    public class ArticleEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DateTime PostDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = new DateTime();

        //Связь с авторами (пользователями) один-ко-многим (подразумеваем личный блог)
        public string UserId { get; set; } = string.Empty;
        public UserEntity User { get; set; } = new UserEntity();

        //Связь с тегами (многие-ко-многим)
        public List<TagEntity> Tags { get; set; } = [];

        //Связь с комментами (один-ко-многим)
        public List<CommentEntity> Comments { get; set; } = [];

        //ToDo можно добавить и лайки/дизлайки, если ума хватит

        //ToDo можно сделать признак скрытия статьи модератором или автором для доработки
    }
}

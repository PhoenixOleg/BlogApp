using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; } = new Guid();

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? AboutUser {  get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        //Связь со статьями один-ко-многим (подразумеваем личный блог)
        public List<ArticleEntity> Articles { get; set; } = [];

        //Связь с комментами один-ко-многим (у юзера >=1 коммент)
        public List<CommentEntity> Comments { get; set; } = [];

        //Тут связать с ролями ToDo???

        //ToDo можно сделать признак бана, заморозики (только читать) и срок этих статусов
    }
}

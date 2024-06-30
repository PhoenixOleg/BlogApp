using System.ComponentModel.DataAnnotations;

namespace BlogApp.BLL.ViewModels.Comment
{
    public class EditCommentViewModel
    {
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Поле \"Заголовок\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст комментария\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст комментария", Prompt = "Комментарий")]
        public string? Content { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;

        public Guid ArticleID { get; set; }

        public string? UserId { get; set; }
    }
}

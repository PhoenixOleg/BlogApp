using BlogApp.BLL.ViewModels.Tag;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.BLL.ViewModels.Article
{
    public class EditArticleViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле \"Название\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле \"Описание\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Краткое содержание")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле \"Текст статьи\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст статьи", Prompt = "Статья")]
        public string Content { get; set; } = string.Empty;

        public DateTime ModifyDate { get; set; } = DateTime.Now;

        //ToDo Тут надо дать возможность изменять теги
        [Display(Name = "Теги", Prompt = "Теги")]
        public List<ShowTagViewModel> Tags { get; set; } = new List<ShowTagViewModel>();
    }
}

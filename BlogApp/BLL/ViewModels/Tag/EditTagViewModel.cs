using System.ComponentModel.DataAnnotations;

namespace BlogApp.BLL.ViewModels.Tag
{
    public class EditTagViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Поле \"Название\" обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; } = string.Empty;
    }
}

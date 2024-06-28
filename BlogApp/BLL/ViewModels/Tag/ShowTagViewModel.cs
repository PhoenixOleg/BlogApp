using System.ComponentModel.DataAnnotations;

namespace BlogApp.BLL.ViewModels.Tag
{
    public class ShowTagViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
    }
}

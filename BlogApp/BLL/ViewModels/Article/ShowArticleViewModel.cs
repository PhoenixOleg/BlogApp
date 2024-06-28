using BlogApp.BLL.ViewModels.Tag;

namespace BlogApp.BLL.ViewModels.Article
{
    public class ShowArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DateTime PostDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;

        public List<ShowTagViewModel> Tags { get; set; } = new List<ShowTagViewModel>();

        //ToDo Comments
    }
}

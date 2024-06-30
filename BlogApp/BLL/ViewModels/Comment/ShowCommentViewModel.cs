namespace BlogApp.BLL.ViewModels.Comment
{
    public class ShowCommentViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Guid ArticleID { get; set; }

        public string? UserId { get; set; }
    }
}

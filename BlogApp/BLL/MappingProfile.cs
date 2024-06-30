using AutoMapper;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.Tag;
using BlogApp.DAL.Models;

namespace BlogApp.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateTagViewModel, TagEntity>();
            CreateMap<EditTagViewModel, TagEntity>();
            CreateMap<TagEntity, ShowTagViewModel>();

            CreateMap<CreateArticleViewModel, ArticleEntity>();
            CreateMap<EditArticleViewModel, ArticleEntity>();
            CreateMap<ArticleEntity, EditArticleViewModel>();

            CreateMap<ShowArticleViewModel, ArticleEntity>();
            CreateMap<ArticleEntity, ShowArticleViewModel>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tamcoc.DAL;
using Tamcoc.Models;

namespace Tamcoc.Controllers
{
    public class BaseController : Controller
    {
        public readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public IEnumerable<ArticleCategoryDto> ArticleCategoryDtos()
        {
            IEnumerable<ArticleCategoryDto> articleCategoryDtos;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    articleCategoryDtos = _unitOfWork.ArticleCategoryEnRepository.GetQuery(a => a.ArticleCategory.CategoryActive && a.LanguageId == 2, q => q.OrderBy(a => a.ArticleCategory.CategorySort)).Select(a => new ArticleCategoryDto
                    {
                        Id = a.ArticleCategoryId,
                        CategorySort = a.ArticleCategory.CategorySort,
                        CategoryName = a.CategoryName,
                        Description = a.Description,
                        Url = a.Url,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ParentId = a.ArticleCategory.ParentId,
                        ShowHome = a.ArticleCategory.ShowHome,
                        Image = a.ArticleCategory.Image,
                        ShowMenu = a.ArticleCategory.ShowMenu,
                        ShowFooter = a.ArticleCategory.ShowFooter,
                        CategoryActive = a.ArticleCategory.CategoryActive,
                        TypePost = a.ArticleCategory.TypePost,
                        RootUrl = a.ArticleCategory.ParentCategory.Url,
                        RootName = a.ArticleCategory.ParentCategory.CategoryName,
                        AboutImage = a.AboutImage,
                        FormationImage = a.ArticleCategory.FormationImage,
                        AboutText = a.AboutText,
                        MottoText = a.MottoText,
                        FormationText = a.FormationText,
                    });
                    break;
                case "fr":
                    articleCategoryDtos = _unitOfWork.ArticleCategoryFrRepository.GetQuery(a => a.ArticleCategory.CategoryActive && a.LanguageId == 3, q => q.OrderBy(a => a.ArticleCategory.CategorySort)).Select(a => new ArticleCategoryDto
                    {
                        Id = a.ArticleCategoryId,
                        CategorySort = a.ArticleCategory.CategorySort,
                        CategoryName = a.CategoryName,
                        Description = a.Description,
                        Url = a.Url,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ParentId = a.ArticleCategory.ParentId,
                        ShowHome = a.ArticleCategory.ShowHome,
                        ShowFooter = a.ArticleCategory.ShowFooter,
                        Image = a.ArticleCategory.Image,
                        ShowMenu = a.ArticleCategory.ShowMenu,
                        CategoryActive = a.ArticleCategory.CategoryActive,
                        TypePost = a.ArticleCategory.TypePost,
                        RootUrl = a.ArticleCategory.ParentCategory.Url,
                        RootName = a.ArticleCategory.ParentCategory.CategoryName,
                        AboutImage = a.AboutImage,
                        FormationImage = a.ArticleCategory.FormationImage,
                        AboutText = a.AboutText,
                        MottoText = a.MottoText,
                        FormationText = a.FormationText,
                    });
                    break;
                default:
                    articleCategoryDtos = _unitOfWork.ArticleCategoryRepository.GetQuery(a => a.CategoryActive, q => q.OrderBy(a => a.CategorySort)).Select(a => new ArticleCategoryDto
                    {
                        Id = a.Id,
                        CategorySort = a.CategorySort,
                        CategoryName = a.CategoryName,
                        Description = a.Description,
                        Url = a.Url,
                        TypePost = a.TypePost,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ParentId = a.ParentId,
                        ShowHome = a.ShowHome,
                        ShowFooter = a.ShowFooter,
                        Image = a.Image,
                        ShowMenu = a.ShowMenu,
                        CategoryActive = a.CategoryActive,
                        RootName = a.ParentCategory.CategoryName,
                        RootUrl = a.ParentCategory.Url,
                        AboutImage = a.AboutImage,
                        FormationImage = a.FormationImage,
                        AboutText = a.AboutText,
                        MottoText = a.MottoText,
                        FormationText = a.FormationText,
                    });
                    break;
            }
            return articleCategoryDtos;
        }
        public IQueryable<ArticleDto> ArticleDtos()
        {
            IQueryable<ArticleDto> articleDtos;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    articleDtos = _unitOfWork.ArticleEnRepository.GetQuery(a => a.Article.Active && a.LanguageId == 2, q => q.OrderBy(a => a.Article.Sort).ThenByDescending(a => a.Article.CreateDate)).Select(a => new ArticleDto
                    {
                        Id = a.ArticleId,
                        Subject = a.Subject,
                        Description = a.Description,
                        Image = a.Article.Image,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ArticleCategoryId = a.Article.ArticleCategoryId,
                        Body = a.Body,
                        CreateDate = a.Article.CreateDate,
                        Home = a.Article.Home,
                        ShowMenu = a.Article.ShowMenu,
                        Active = a.Article.Active,
                        CategoryName = a.Article.ArticleCategory.ArticleCategoryEns.FirstOrDefault(c => c.LanguageId == 2).CategoryName,
                        CategoryUrl = a.Article.ArticleCategory.ArticleCategoryEns.FirstOrDefault(c => c.LanguageId == 2).Url,
                        Url = a.Url,
                        ParentId = a.Article.ArticleCategory.ParentId,
                        TypePost = a.Article.ArticleCategory.TypePost,
                    });
                    break;
                case "fr":
                    articleDtos = _unitOfWork.ArticleFrRepository.GetQuery(a => a.Article.Active && a.LanguageId == 3, q => q.OrderBy(a => a.Article.Sort).ThenByDescending(a => a.Article.CreateDate)).Select(a => new ArticleDto
                    {
                        Id = a.ArticleId,
                        Subject = a.Subject,
                        Description = a.Description,
                        Image = a.Article.Image,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ArticleCategoryId = a.Article.ArticleCategoryId,
                        Body = a.Body,
                        CreateDate = a.Article.CreateDate,
                        Home = a.Article.Home,
                        ShowMenu = a.Article.ShowMenu,
                        Active = a.Article.Active,
                        CategoryName = a.Article.ArticleCategory.ArticleCategoryEns.FirstOrDefault(c => c.LanguageId == 3).CategoryName,
                        CategoryUrl = a.Article.ArticleCategory.ArticleCategoryEns.FirstOrDefault(c => c.LanguageId == 3).Url,
                        Url = a.Url,
                        ParentId = a.Article.ArticleCategory.ParentId,
                        TypePost = a.Article.ArticleCategory.TypePost,
                    });
                    break;
                default:
                    articleDtos = _unitOfWork.ArticleRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort).ThenByDescending(a => a.CreateDate)).Select(a => new ArticleDto
                    {
                        Id = a.Id,
                        Subject = a.Subject,
                        Description = a.Description,
                        Image = a.Image,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        ArticleCategoryId = a.ArticleCategoryId,
                        Body = a.Body,
                        CreateDate = a.CreateDate,
                        Home = a.Home,
                        ShowMenu = a.ShowMenu,
                        Active = a.Active,
                        Url = a.Url,
                        CategoryName = a.ArticleCategory.CategoryName,
                        CategoryUrl = a.ArticleCategory.Url,
                        ParentId = a.ArticleCategory.ParentId,
                        TypePost = a.ArticleCategory.TypePost
                    });
                    break;
            }
            return articleDtos;
        }
        public IEnumerable<BannerDto> BannerDtos()
        {
            IEnumerable<BannerDto> bannerDtos;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    bannerDtos = _unitOfWork.BannerEnRepository.GetQuery(a => a.Banner.Active && a.LanguageId == 2, q => q.OrderBy(a => a.Banner.Sort)).Select(a => new BannerDto
                    {
                        Id = a.BannerId,
                        BannerName = a.BannerName,
                        Video = a.Video,
                        Image = a.Image,
                        Home = a.Home,
                        ListImage = a.ListImage,
                        Slogan = a.Slogan,
                        GroupId = a.Banner.GroupId,
                        Sort = a.Banner.Sort,
                        Content = a.Content,
                        Url = a.Url
                    });
                    if (!bannerDtos.Any())
                    {
                        bannerDtos = _unitOfWork.BannerRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort)).Select(a => new BannerDto
                        {
                            Id = a.Id,
                            BannerName = a.BannerName,
                            Video = a.Video,
                            Image = a.Image,
                            ListImage = a.ListImage,
                            Home = a.Home,
                            Slogan = a.Slogan,
                            GroupId = a.GroupId,
                            Sort = a.Sort,
                            Content = a.Content,
                            Url = a.Url
                        });
                    }
                    break;
                case "fr":
                    bannerDtos = _unitOfWork.BannerFrRepository.GetQuery(a => a.Banner.Active && a.LanguageId == 3, q => q.OrderBy(a => a.Banner.Sort)).Select(a => new BannerDto
                    {
                        Id = a.BannerId,
                        BannerName = a.BannerName,
                        Video = a.Video,
                        Home = a.Home,
                        Image = a.Image,
                        ListImage = a.ListImage,
                        Slogan = a.Slogan,
                        GroupId = a.Banner.GroupId,
                        Sort = a.Banner.Sort,
                        Content = a.Content,
                        Url = a.Url
                    });
                    if (!bannerDtos.Any())
                    {
                        bannerDtos = _unitOfWork.BannerRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort)).Select(a => new BannerDto
                        {
                            Id = a.Id,
                            BannerName = a.BannerName,
                            Home = a.Home,
                            Video = a.Video,
                            Image = a.Image,
                            ListImage = a.ListImage,
                            Slogan = a.Slogan,
                            GroupId = a.GroupId,
                            Sort = a.Sort,
                            Content = a.Content,
                            Url = a.Url
                        });
                    }
                    break;
                default:
                    bannerDtos = _unitOfWork.BannerRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort)).Select(a => new BannerDto
                    {
                        Id = a.Id,
                        BannerName = a.BannerName,
                        Image = a.Image,
                        Video = a.Video,
                        ListImage = a.ListImage,
                        Home = a.Home,
                        Slogan = a.Slogan,
                        GroupId = a.GroupId,
                        Content = a.Content,
                        Sort = a.Sort,
                        Url = a.Url
                    });
                    break;
            }
            return bannerDtos;
        }
        public IEnumerable<FeedbackDto> FeedbackDtos()
        {
            IEnumerable<FeedbackDto> feedbackDtos;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    feedbackDtos = _unitOfWork.FeedbackEnRepository.GetQuery(a => a.Feedback.Active && a.LanguageId == 2 , p => p.OrderBy(a => a.Feedback.Sort)).Select(a => new FeedbackDto
                    {
                        Id = a.FeedbackId,
                        Name = a.Name,
                        Classify = a.Classify,
                        Content = a.Content,
                        Star = a.Star,
                        Image = a.Image,
                    });
                    break;
                case "fr":
                    feedbackDtos = _unitOfWork.FeedbackFrRepository.GetQuery(a => a.Feedback.Active && a.LanguageId == 3, p => p.OrderBy(a => a.Feedback.Sort)).Select(a => new FeedbackDto
                    {
                        Id = a.FeedbackId,
                        Name = a.Name,
                        Classify = a.Classify,
                        Content = a.Content,
                        Star = a.Star,
                        Image = a.Image,
                    });
                    break;
                default:
                    feedbackDtos = _unitOfWork.FeedbackRepository.GetQuery(a => a.Active, p => p.OrderBy(a => a.Sort)).Select(a => new FeedbackDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Classify = a.Classify,
                        Star = a.Star,
                        Sort = a.Sort,
                        Content = a.Content,
                        Image = a.Image,
                    });
                    break;
            }
            return feedbackDtos;
        }
        public IQueryable<RoomDto> RoomDtos()
        {
            IQueryable<RoomDto> roomDtos;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    roomDtos = _unitOfWork.RoomEnRepository.GetQuery(a => a.Room.Active && a.LanguageId == 2, q => q.OrderBy(a => a.Room.Sort).ThenByDescending(a => a.Room.CreateDate)).Select(a => new RoomDto
                    {
                        Id = a.RoomId,
                        Name = a.Name,
                        Description = a.Description,
                        ListImage = a.Room.ListImage,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        Body = a.Body,
                        CreateDate = a.Room.CreateDate,
                        Home = a.Room.Home,
                        ShowMenu = a.Room.ShowMenu,
                        Active = a.Room.Active,
                        Url = a.Url,
                        People = a.People,
                        View = a.View,
                        Price = a.Price,
                    });
                    break;
                case "fr":
                    roomDtos = _unitOfWork.RoomFrRepository.GetQuery(a => a.Room.Active && a.LanguageId == 3, q => q.OrderBy(a => a.Room.Sort).ThenByDescending(a => a.Room.CreateDate)).Select(a => new RoomDto
                    {
                        Id = a.RoomId,
                        Name = a.Name,
                        Description = a.Description,
                        ListImage = a.Room.ListImage,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        Body = a.Body,
                        CreateDate = a.Room.CreateDate,
                        Home = a.Room.Home,
                        ShowMenu = a.Room.ShowMenu,
                        Active = a.Room.Active,
                        Url = a.Url,
                        People = a.People,
                        View = a.View,
                        Price = a.Price,
                    });
                    break;
                default:
                    roomDtos = _unitOfWork.RoomRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort).ThenByDescending(a => a.CreateDate)).Select(a => new RoomDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        ListImage = a.ListImage,
                        TitleMeta = a.TitleMeta,
                        DescriptionMeta = a.DescriptionMeta,
                        Body = a.Body,
                        CreateDate = a.CreateDate,
                        Home = a.Home,
                        ShowMenu = a.ShowMenu,
                        Active = a.Active,
                        Url = a.Url,
                        People = a.People,
                        View = a.View,
                        Price = a.Price,
                        Sort = a.Sort,
                    });
                    break;
            }
            return roomDtos;
        }
        public ConfigSiteDto ConfigSiteDto()
        {
            ConfigSiteDto configSiteDto;
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en":
                    configSiteDto = (ConfigSiteDto)HttpContext.Application["ConfigSiteEn"] ?? (ConfigSiteDto)HttpContext.Application["ConfigSite"];
                    break;
                case "fr":
                    configSiteDto = (ConfigSiteDto)HttpContext.Application["ConfigSiteFr"] ?? (ConfigSiteDto)HttpContext.Application["ConfigSite"];
                    break;
                default:
                    configSiteDto = (ConfigSiteDto)HttpContext.Application["ConfigSite"];
                    break;
            }
            return configSiteDto;
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tamcoc.Models;

namespace Tamcoc.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BannerDto> BannerDtos { get; set; }
        public IEnumerable<BannerDto> Album { get; set; }
        public IEnumerable<ArticleCategoryDto> ArticleCategoryDtos { get; set; }
        public IEnumerable<RoomDto> RoomDtos { get; set; }
        public IEnumerable<ArticleDto> ArticleDtos { get; set; }
        public IEnumerable<FeedbackDto> FeedbackDtos { get; set; }
        public ConfigSiteDto ConfigSiteDto { get; set; }
    }
    public class HeaderViewModel
    {
        public IEnumerable<ArticleCategoryDto> ArticleCategoryDtos { get; set; }
        public IEnumerable<RoomDto> RoomDtos { get; set; }
        public ConfigSiteDto ConfigSiteDto { get; set; }

    }
    public class AllRoomViewModel
    {
        public PagedList.IPagedList<RoomDto> RoomList { get; set; }
    }
    public class RoomDetialViewModel
    {
        public RoomDto RoomDto { get; set; }
    }
    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public ConfigSiteDto ConfigSiteDto { get; set; }
    }
    public class AlbumViewModel
    {
        public ConfigSiteDto ConfigSiteDto { get; set; }
        public IEnumerable<BannerDto> Album { get; set; }

    }
    public class FormOrderViewModel
    {
        public Order Order { get; set; }
        //public IEnumerable<KindOfRoom> KindOfRooms { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
    public class FormDetailViewModel
    {
        public Order Order { get; set; }
        public RoomDto RoomDto { get; set; }

    }
    public class IntroductViewModel
    {
        public ConfigSiteDto ConfigSiteDto { get; set; }

    }
    public class ArticleCategoryViewModel
    {
        public ArticleCategoryDto CategoryDto { get; set; }
        public IPagedList<ArticleDto> ArticleDtos { get; set; }
        public IEnumerable<ArticleCategoryDto> CategoryDtos { get; set; }
    }
    public class ArticleDetailsViewModel
    {
        public ArticleDto ArticleDto { get; set; }
        public IEnumerable<ArticleDto> ArticleDtos { get; set; }
    }
    public class FooterViewModel
    {
        public ConfigSiteDto ConfigSiteDto { get; set; }
        public IEnumerable<ArticleCategoryDto> ArticleCategoryDtos { get; set; }
    }
    public class SearchViewModel
    {
        public string Keywords { get; set; }
        public IPagedList<RoomDto> RoomDtos { get; set; }
    }
}
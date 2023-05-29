using Helpers;
using PagedList;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Tamcoc.Filters;
using Tamcoc.Models;
using Tamcoc.ViewModel;

namespace Tamcoc.Controllers
{
    public class HomeController : BaseController
    {
        private static string Email => WebConfigurationManager.AppSettings["email"];
        private static string Password => WebConfigurationManager.AppSettings["password"];

        #region Home
        [ChildActionOnly]
        public PartialViewResult Header()
        {
            var model = new HeaderViewModel
            {
                ArticleCategoryDtos = ArticleCategoryDtos().Where(a => a.ShowMenu && (a.TypePost == TypePost.Article || a.TypePost == TypePost.Service)).OrderBy(a => a.CategorySort),
                RoomDtos = RoomDtos().Where(a => a.ShowMenu).OrderByDescending(a => a.CreateDate),
                ConfigSiteDto = ConfigSiteDto(),
            };
            return PartialView(model);
        }
        public PartialViewResult Footer()
        {
            var model = new FooterViewModel
            {
                ConfigSiteDto = ConfigSiteDto(),
                ArticleCategoryDtos = ArticleCategoryDtos(),
            };
            return PartialView(model);
        }
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*(vcms|article|banner|contact|uploader|service)).*$)}", Order = 1)]
        [Route(Order = 2)]
        public ActionResult Index()
        {
            var album = BannerDtos().Where(a => a.GroupId == 2);
            var count = album.Count();
            var model = new HomeViewModel
            {
                BannerDtos = BannerDtos().Where(a => a.GroupId == 2),
                Album = BannerDtos().Where(a => a.GroupId == 3 && a.Home),
                ArticleCategoryDtos = ArticleCategoryDtos(),
                ArticleDtos = ArticleDtos().Where(a => a.Active && a.TypePost == TypePost.Article).OrderByDescending(a => a.CreateDate),
                ConfigSiteDto = ConfigSiteDto(),
                FeedbackDtos = FeedbackDtos(),
                RoomDtos = RoomDtos(),
            };
            return View(model);
        }
        #endregion

        #region Rooom

        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/rooms", Order = 1)]
        [Route("{culture=vi}/phong", Order = 2)]
        public ActionResult Room(int? page)
        {
            var pageNumber = page ?? 1;
            var room = RoomDtos().Where(a => a.Active).OrderByDescending(a => a.CreateDate);
            var model = new AllRoomViewModel
            {
                RoomList = room.ToPagedList(pageNumber, 9),
            };
            return View(model);
        }
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/room/{url}.html", Order = 1)]
        [Route("{culture=vi}/phong/{url}.html", Order = 2)]
        public ActionResult RoomDetail(string url)
        {
            var room = RoomDtos().FirstOrDefault(a => a.Active && a.Url == url);
            if (room == null)
            {
                return RedirectToAction("Index");
            }
            var model = new RoomDetialViewModel
            {
                RoomDto = room,
            };
            return View(model);
        }
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/search", Order = 1)]
        [Route("{culture=vi}/tim-kiem", Order = 2)]
        public ActionResult Search(int? page, string keywords)
        {
            var pageNumber = page ?? 1;
            var pageSize = 12;

            var newkey = keywords.Trim();
            var rooms = RoomDtos().Where(l => l.Active && l.Name.Contains(newkey)).OrderByDescending(a => a.CreateDate);

            if (string.IsNullOrEmpty(newkey))
            {
                return RedirectToAction("Index");
            }

            var model = new SearchViewModel
            {
                RoomDtos = rooms.ToPagedList(pageNumber, pageSize),
                Keywords = keywords,
            };
            return View(model);
        }
        #endregion

        #region Article
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/blog/{url}", Order = 1)]
        [Route("{culture=vi}/blog/{url}", Order = 2)]
        public ActionResult ArticleCategory(int? page, string url)
        {
            var category = ArticleCategoryDtos().FirstOrDefault(a => a.CategoryActive && a.Url == url);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var articles = ArticleDtos().Where(
                a => a.Active && (a.ArticleCategoryId == category.Id || a.ParentId == category.Id)).OrderByDescending(a => a.CreateDate);
            var pageNumber = page ?? 1;

            if (articles.Count() == 1)
            {
                var fi = articles.First();
                return RedirectToAction("ArticleDetail", new { url = fi.Url });
            }
            var model = new ArticleCategoryViewModel
            {
                CategoryDto = category,
                ArticleDtos = articles.ToPagedList(pageNumber, 12),
                CategoryDtos = ArticleCategoryDtos(),
            };
            return View(model);
        }
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/blog/{url}.html", Order = 1)]
        [Route("{culture=vi}/blog/{url}.html", Order = 2)]
        public ActionResult ArticleDetail(string url)
        {
            var article = ArticleDtos().FirstOrDefault(a => a.Url == url && a.Active);
            var articles = ArticleDtos().Where(
                a => a.Id != article.Id && a.Active && (a.ArticleCategoryId == article.ArticleCategoryId || a.ParentId == article.Id)).OrderByDescending(a => a.CreateDate).Take(12);
            if (article == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ArticleDetailsViewModel
            {
                ArticleDto = article,
                ArticleDtos = articles,
            };
            return View(model);
        }
        #endregion

        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/contact-us", Order = 1)]
        [Route("{culture=vi}/lien-he", Order = 2)]
        public ActionResult Contact()
        {

            var model = new ContactViewModel
            {
                ConfigSiteDto = ConfigSiteDto(),
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken, LanguageFilters]
        public JsonResult ContactForm(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Please enter the true format" });
            }
            _unitOfWork.ContactRepository.Insert(model.Contact);
            _unitOfWork.Save();
            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.Contact.FullName},</p>" +
                       $"<p>Số điện thoại: {model.Contact.Mobile},</p>" +
                       $"<p>Email: {model.Contact.Email},</p>" +
                       $"<p>Nội dung: {model.Contact.Body}</p>" +
                       $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSiteDto().Email, Email, Email, Password, ConfigSiteDto().Title));

            return Json(new { status = true, msg = "We will get back to you as soon as possible !!" });
        }

        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/album", Order = 1)]
        [Route("{culture=vi}/thu-vien", Order = 2)]
        public ActionResult Album()
        {
            var model = new AlbumViewModel
            {
                Album = BannerDtos().Where(a => a.GroupId == 3),
            };
            return View(model);
        }

        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/booking", Order = 1)]
        [Route("{culture=vi}/dat-phong", Order = 2)]
        public PartialViewResult FormOrder()
        {
            var kind = _unitOfWork.KindOfRoomRepository.Get();
            var model = new FormOrderViewModel
            {
                KindOfRooms = kind,
            };
            return PartialView(model);
        }
        public PartialViewResult FormOderDetail(int id)
        {
            var room = RoomDtos().Where(a => a.Id == id).FirstOrDefault();
            var model = new FormDetailViewModel
            {
                RoomDto = room,
            };
            return PartialView(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult OderDetail(FormDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, msg = "Please enter the true format" });
            }
            model.Order.CreateDate = DateTime.Now;
            _unitOfWork.OrderRepository.Insert(model.Order);
            _unitOfWork.Save();
            var subject = "Email liên hệ từ website: " + Request.Url?.Host;
            var body = $"<p>Tên người liên hệ: {model.Order.CustomerInfo.FullName},</p>" +
                       $"<p>Số điện thoại: {model.Order.CustomerInfo.Mobile},</p>" +
                       $"<p>Email: {model.Order.CustomerInfo.Email},</p>" +
                       $"<p>Địa chỉ: {model.Order.CustomerInfo.Address},</p>" +
                       $"<p>Loại phòng: {model.Order.Name},</p>" +
                       $"<p>Số người lớn: {model.Order.CustomerInfo.Adults},</p>" +
                       $"<p>Số người trẻ em: {model.Order.CustomerInfo.Children},</p>" +
                       $"<p>Nội dung: {model.Order.CustomerInfo.Body}</p>" +
                       $"<p>Đây là hệ thống gửi email tự động, vui lòng không phản hồi lại email này.</p>";
            Task.Run(() => HtmlHelpers.SendEmail("gmail", subject, body, ConfigSiteDto().Email, Email, Email, Password, ConfigSiteDto().Title));

            return Json(new { status = true, msg = "We will get back to you as soon as possible !!" });
        }
        [HttpGet, LanguageFilters]
        [Route("{culture:regex(^(?!.*vi).*$)}/about", Order = 1)]
        [Route("{culture=vi}/ve-chung-toi", Order = 2)]
        public ActionResult About()
        {
            var model = new IntroductViewModel
            {
                ConfigSiteDto = ConfigSiteDto(),
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult SubcribeForm(string email)
        {
            var isEmail = new EmailAddressAttribute().IsValid(email);
            if (!isEmail || string.IsNullOrEmpty(email))
            {
                return Json(new { status = false, msg = "Invalid email, please try again!" });
            }

            Subcribe model = new Subcribe { Email = email };

            _unitOfWork.SubcribeRepository.Insert(model);
            _unitOfWork.Save();
            return Json(new { status = true, msg = "Personal registration you believe successful!" });
        }
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
using Helpers;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Tamcoc.DAL;
using Tamcoc.Models;
using Tamcoc.ViewModel;

namespace Tamcoc.Controllers
{
    [Authorize]
    public class BannerController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        #region Banner
        public ActionResult ListBanner(int? page, int groupId = 0, string result = "")
        {
            ViewBag.Banner = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var banners = _unitOfWork.BannerRepository.GetQuery(orderBy: q => q.OrderBy(a => a.GroupId).ThenBy(a => a.Sort));
            if (groupId > 0)
            {
                banners = banners.Where(a => a.GroupId == groupId);
            }
            var model = new ListBannerViewModel
            {
                Banners = banners.ToPagedList(pageNumber, pageSize),
            };
            return View(model);
        }
        public ActionResult Banner()
        {
            var model = new BannerViewModel()
            {
                Banner = new Banner() { Sort = 1, Active = true }
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Banner(BannerViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;

                var video = Request.Files["Banner.Video"];
                model.Banner.ListImage = fc["Pictures"];
                if (video != null && video.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(video.FileName, "mp4"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                        isPost = false;
                    }
                    else
                    {
                        if (video.ContentLength > 100 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 100MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(video.FileName);
                            model.Banner.Video = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            video.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                if (isPost)
                {
                    _unitOfWork.BannerRepository.Insert(model.Banner);
                    _unitOfWork.Save();

                    return RedirectToAction("ListBanner", new { result = "success" });
                }
            }
            return View(model);
        }
        public ActionResult EditBanner(int bannerId = 0)
        {
            var banner = _unitOfWork.BannerRepository.GetById(bannerId);
            if (banner == null)
            {
                return RedirectToAction("ListBanner");
            }
            var model = new BannerViewModel
            {
                Banner = banner,
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditBanner(BannerViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;

                var banner = _unitOfWork.BannerRepository.GetById(model.Banner.Id);
                var video = Request.Files["Banner.Video"];
                if (video != null && video.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(video.FileName, "mp4"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                        isPost = false;
                    }
                    else
                    {
                        if (video.ContentLength > 100 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 100MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(video.FileName);
                            banner.Video = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            video.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }
                if (isPost)
                {
                    banner.ListImage = fc["Pictures"] == "" ? null : fc["Pictures"];
                    banner.GroupId = model.Banner.GroupId;
                    banner.BannerName = model.Banner.BannerName;
                    banner.Slogan = model.Banner.Slogan;
                    banner.Sort = model.Banner.Sort;
                    banner.Active = model.Banner.Active;
                    banner.Url = model.Banner.Url;
                    banner.Home = model.Banner.Home;
                    banner.Content = model.Banner.Content;
                    _unitOfWork.Save();

                    return RedirectToAction("ListBanner", new { result = "update" });
                }
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteBanner(int bannerId = 0)
        {
            var banner = _unitOfWork.BannerRepository.GetById(bannerId);
            if (banner == null)
            {
                return false;
            }
            HtmlHelpers.DeleteFile(Server.MapPath("/images/banners/" + banner.Image));
            _unitOfWork.BannerRepository.Delete(banner);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateBannerQuick(int sort = 1, bool active = false, int bannerId = 0)
        {
            var banner = _unitOfWork.BannerRepository.GetById(bannerId);
            if (banner == null)
            {
                return false;
            }
            banner.Sort = sort;
            banner.Active = active;

            _unitOfWork.Save();
            return true;
        }

        public ActionResult UpdateBannerEn(int bannerId, int result = 0)
        {
            ViewBag.Result = result;
            var banner = _unitOfWork.BannerRepository.GetById(bannerId);
            return View(banner);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateBannerEn(FormCollection fc)
        {
            var bannerId = Convert.ToInt32(fc["BannerId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var groupId = Convert.ToInt32(fc["GroupId"]);
            var name = fc["Name"];
            var listImg = fc["ListImg"];
            var slogan = fc["Slogan"];
            var content = fc["Content"];
            var url = fc["Url"];
            var Video = "";
            var albumLang = _unitOfWork.BannerEnRepository.GetQuery(a => a.BannerId == bannerId && a.LanguageId == langId).SingleOrDefault();
            var video = Request.Files["Video"];
            if (video != null && video.ContentLength > 0)
            {
                if (!HtmlHelpers.CheckFileExt(video.FileName, "mp4"))
                {
                    ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                }
                else
                {
                    if (video.ContentLength > 4000 * 1024)
                    {
                        ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                    }
                    else
                    {
                        var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                        HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                        var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(video.FileName);
                        Video = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                        video.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                    }
                }
            }
            if (albumLang == null)
            {
                _unitOfWork.BannerEnRepository.Insert(new BannerEn
                {
                    BannerId = bannerId,
                    ListImage = listImg,
                    GroupId = groupId,
                    Video = Video,
                    LanguageId = langId,
                    BannerName = name,
                    Slogan = slogan,
                    Content = content,
                    Url = url,
                });
                _unitOfWork.Save();
                return RedirectToAction("UpdateBannerEn", new { bannerId, result = 1 });
            }
            albumLang.BannerName = name;
            albumLang.Slogan = slogan;
            albumLang.Video = Video;
            albumLang.ListImage = listImg;
            albumLang.GroupId = groupId;
            albumLang.Url = url;
            albumLang.Content = content;
            _unitOfWork.Save();
            return RedirectToAction("UpdateBannerEn", new { bannerId, result = 1 });
        }
        public ActionResult UpdateBannerFr(int bannerId, int result = 0)
        {
            ViewBag.Result = result;
            var banner = _unitOfWork.BannerRepository.GetById(bannerId);
            return View(banner);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateBannerFr(FormCollection fc)
        {
            var bannerId = Convert.ToInt32(fc["BannerId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var groupId = Convert.ToInt32(fc["GroupId"]);
            var name = fc["Name"];
            var listimg = fc["ListImg"];
            var slogan = fc["Slogan"];
            var content = fc["Content"];
            var url = fc["Url"];
            var Video = "";

            var albumLang = _unitOfWork.BannerFrRepository.GetQuery(a => a.BannerId == bannerId && a.LanguageId == langId).SingleOrDefault();
            var video = Request.Files["Video"];
            if (video != null && video.ContentLength > 0)
            {
                if (!HtmlHelpers.CheckFileExt(video.FileName, "mp4"))
                {
                    ModelState.AddModelError("", @"Chỉ chấp nhận định dạng mp4");
                }
                else
                {
                    if (video.ContentLength > 4000 * 1024)
                    {
                        ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                    }
                    else
                    {
                        var imgPath = "/images/banners/" + DateTime.Now.ToString("yyyy/MM/dd");
                        HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                        var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(video.FileName);
                        Video = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                        video.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                    }
                }
            }
            if (albumLang == null)
            {
                _unitOfWork.BannerFrRepository.Insert(new BannerFr
                {
                    BannerId = bannerId,
                    Video = Video,
                    ListImage = listimg,
                    GroupId = groupId,
                    LanguageId = langId,
                    BannerName = name,
                    Slogan = slogan,
                    Content = content,
                    Url = url,
                });
                _unitOfWork.Save();
                return RedirectToAction("UpdateBannerFr", new { bannerId, result = 1 });
            }
            albumLang.BannerName = name;
            albumLang.Slogan = slogan;
            albumLang.ListImage = listimg;
            albumLang.Video = Video;
            albumLang.GroupId = groupId;
            albumLang.Url = url;
            albumLang.Content = content;
            _unitOfWork.Save();
            return RedirectToAction("UpdateBannerFr", new { bannerId, result = 1 });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
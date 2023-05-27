using System.Web.Mvc;
using Tamcoc.DAL;
using Tamcoc.ViewModel;
using Tamcoc.Models;
using System.Linq;
using PagedList;
using System;
using Helpers;
using System.IO;
using System.Drawing.Text;

namespace SamLife.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        #region Contact
        public ActionResult ListContact(int? page, string name)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var contact = _unitOfWork.ContactRepository.GetQuery(orderBy: l => l.OrderByDescending(a => a.Id));

            if (!string.IsNullOrEmpty(name))
            {
                contact = contact.Where(l => l.FullName.Contains(name));
            }
            var model = new ListContactViewModel
            {
                Contacts = contact.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteContact(int contactId = 0)
        {
            var contact = _unitOfWork.ContactRepository.GetById(contactId);
            if (contact == null)
            {
                return false;
            }
            _unitOfWork.ContactRepository.Delete(contact);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Feedback
        public ActionResult ListFeedback(int? page, string name, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var feedback = _unitOfWork.FeedbackRepository.GetQuery(orderBy: l => l.OrderBy(a => a.Sort));

            var model = new ListFeedbackViewModel
            {
                Feedbacks = feedback.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        public ActionResult Feedback()
        {
            var model = new Feedback
            {
                Sort = 1,
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Feedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType != "image/jpeg" & file.ContentType != "image/png" && file.ContentType != "image/gif")
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            model.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;

                            var newImage = System.Drawing.Image.FromStream(file.InputStream);
                            var fixSizeImage = HtmlHelpers.FixedSize(newImage, 600, 600, false);
                            HtmlHelpers.SaveJpeg(Server.MapPath(Path.Combine(imgPath, imgFileName)), fixSizeImage, 90);
                        }
                    }
                }
                if (isPost)
                {
                    _unitOfWork.FeedbackRepository.Insert(model);
                    _unitOfWork.Save();

                    return RedirectToAction("ListFeedback", new { result = "success" });
                }
            }
            return View(model);
        }
        public ActionResult UpdateFeedback(int feedbackId = 0)
        {
            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return RedirectToAction("ListFeedback");
            }
            return View(feedback);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeedback(Feedback model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var isPost = true;
                var feedback = _unitOfWork.FeedbackRepository.GetById(model.Id);

                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    if (!HtmlHelpers.CheckFileExt(file.FileName, "jpg|jpeg|png|gif|svg"))
                    {
                        ModelState.AddModelError("", @"Chỉ chấp nhận định dạng jpg, png, gif, jpeg, svg");
                        isPost = false;
                    }
                    else
                    {
                        if (file.ContentLength > 4000 * 1024)
                        {
                            ModelState.AddModelError("", @"Dung lượng lớn hơn 4MB. Hãy thử lại");
                            isPost = false;
                        }
                        else
                        {
                            var imgPath = "/images/feedbacks/" + DateTime.Now.ToString("yyyy/MM/dd");
                            HtmlHelpers.CreateFolder(Server.MapPath(imgPath));
                            var imgFileName = DateTime.Now.ToFileTimeUtc() + Path.GetExtension(file.FileName);

                            feedback.Image = DateTime.Now.ToString("yyyy/MM/dd") + "/" + imgFileName;
                            file.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
                        }
                    }
                }

                if (isPost)
                {
                    feedback.Sort = model.Sort;
                    feedback.Name = model.Name;
                    feedback.Classify = model.Classify;
                    feedback.Content = model.Content;
                    feedback.Star = model.Star;
                    feedback.Active = model.Active;
                    feedback.Name = model.Name;
                    //feedback.Classify = model.Classify;


                    _unitOfWork.FeedbackRepository.Update(feedback);
                    _unitOfWork.Save();

                    return RedirectToAction("ListFeedback", new { result = "update" });
                }
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteFeedback(int feedbackId = 0)
        {
            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            if (feedback == null)
            {
                return false;
            }
            _unitOfWork.FeedbackRepository.Delete(feedback);
            _unitOfWork.Save();
            return true;
        }

        public ActionResult UpdateFeedbackEn(int feedbackId, int result = 0)
        {
            ViewBag.Result = result;
            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            return View(feedback);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeedbackEn(FormCollection fc)
        {
            var feedbackId = Convert.ToInt32(fc["FeedbackId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var name = fc["Name"];
            var img = fc["Img"];
            var star = Convert.ToInt32(fc["Star"]);
            var clastify = fc["Classify"];
            var content = fc["Content"];
            var feedbackEn = _unitOfWork.FeedbackEnRepository.GetQuery(a => a.FeedbackId == feedbackId && a.LanguageId == langId).SingleOrDefault();

            if (feedbackEn == null)
            {
                _unitOfWork.FeedbackEnRepository.Insert(new FeedbackEn
                {
                    FeedbackId = feedbackId,
                    LanguageId = langId,
                    Name = name,
                    Star = star,
                    Image = img,
                    Classify = clastify,
                    Content = content,
                });
                _unitOfWork.Save();
                return RedirectToAction("UpdateFeedbackEn", new { feedbackId, result = 1 });
            }
            feedbackEn.Name = name;
            feedbackEn.Star = star;
            feedbackEn.Classify = clastify;
            feedbackEn.Image = img;
            feedbackEn.Content = content;
            _unitOfWork.Save();
            return RedirectToAction("UpdateFeedbackEn", new { feedbackId, result = 1 });
        }
        public ActionResult UpdateFeedbackFr(int feedbackId , int result = 0)
        {
            ViewBag.Result = result;
            var feedback = _unitOfWork.FeedbackRepository.GetById(feedbackId);
            return View(feedback);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeedbackFr(FormCollection fc)
        {
            var feedbackId = Convert.ToInt32(fc["FeedbackId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var star = Convert.ToInt32(fc["Star"]);
            var name = fc["Name"];
            var img = fc["Img"];
            var clastify = fc["Classify"];
            var content = fc["Content"];
            var feedbackFr = _unitOfWork.FeedbackFrRepository.GetQuery(a => a.FeedbackId == feedbackId && a.LanguageId == langId).SingleOrDefault();

            if (feedbackFr == null)
            {
                _unitOfWork.FeedbackFrRepository.Insert(new FeedbackFr
                {
                    FeedbackId = feedbackId,
                    LanguageId = langId,
                    Star = star,
                    Name = name,
                    Image = img,
                    Classify = clastify,
                    Content = content,
                });
                _unitOfWork.Save();
                return RedirectToAction("UpdateFeedbackFr", new { feedbackId, result = 1 });
            }
            feedbackFr.Name = name;
            feedbackFr.Star = star;
            feedbackFr.Image = img;
            feedbackFr.Classify = clastify;
            feedbackFr.Content = content;
            _unitOfWork.Save();
            return RedirectToAction("UpdateFeedbackFr", new { feedbackId, result = 1 });
        }
        #endregion

        #region Subcribe
        public ActionResult ListSubcribe(int? page, string name)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var contact = _unitOfWork.SubcribeRepository.GetQuery(orderBy: l => l.OrderByDescending(a => a.Id));

            if (!string.IsNullOrEmpty(name))
            {
                contact = contact.Where(l => l.Email.Contains(name));
            }
            var model = new ListSubcribeViewModel
            {
                Subcribes = contact.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteSubcribe(int subId = 0)
        {
            var sub = _unitOfWork.SubcribeRepository.GetById(subId);
            if (sub == null)
            {
                return false;
            }
            _unitOfWork.SubcribeRepository.Delete(sub);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        #region Order
        public ActionResult ListOrder(int? page, string name)
        {

            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var contact = _unitOfWork.OrderRepository.GetQuery(a => a.RoomId != null, l => l.OrderByDescending(a => a.Id));
            if (!string.IsNullOrEmpty(name))
            {
                contact = contact.Where(l => l.CustomerInfo.FullName.ToLower().Contains(name.ToLower()));
            }
            var model = new ListOrderViewModel
            {
                Orders = contact.ToPagedList(pageNumber, pageSize),
                Name = name,
                //Articles = _unitOfWork.ArticleRepository.GetQuery(a => a.Active, q => q.OrderBy(a => a.Sort)),
                Rooms = _unitOfWork.RoomRepository.GetQuery(a => a.Active),
            };
            return View(model);
        }
        [HttpPost]
        public bool DeleteOrder(int orderId = 0)
        {
            var Order = _unitOfWork.OrderRepository.GetById(orderId);
            if (Order == null)
            {
                return false;
            }
            _unitOfWork.OrderRepository.Delete(Order);
            _unitOfWork.Save();
            return true;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
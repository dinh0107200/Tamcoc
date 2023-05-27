using System.Linq;
using System.Web.Mvc;
using Tamcoc.DAL;
using Tamcoc.ViewModel;
using PagedList;
using Tamcoc.Models;
using Helpers;
using System;

namespace Tamcoc.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        #region Room
        public ActionResult ListRoom(int? page, string name, string result = "")
        {
            ViewBag.Result = result;
            var pageNumber = page ?? 1;
            const int pageSize = 15;
            var room = _unitOfWork.RoomRepository.GetQuery(orderBy: l => l.OrderByDescending(a => a.Id));

            if (!string.IsNullOrEmpty(name))
            {
                room = room.Where(l => l.Name.ToLower().Contains(name.ToLower()));
            }
            var model = new ListRoomViewModel
            {
                Rooms = room.ToPagedList(pageNumber, pageSize),
                Name = name
            };
            return View(model);
        }
        public ActionResult Room()
        {
            var model = new InsertRoomViewModel()
            {
                Room = new Room() { Sort = 1, Active = true },
               KindOfRooms = _unitOfWork.KindOfRoomRepository.Get(orderBy: o => o.OrderBy(a => a.Sort)),
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Room(InsertRoomViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                model.Room.ListImage = fc["Pictures"];
                model.Room.Url = HtmlHelpers.ConvertToUnSign(null, model.Room.Url ?? model.Room.Name);

                var count = _unitOfWork.RoomRepository.GetQuery(a => a.Url == model.Room.Url).Count();
                if (count > 1)
                {
                    model.Room.Url += "-" + DateTime.Now.Millisecond;
                    _unitOfWork.Save();
                }
                _unitOfWork.RoomRepository.Insert(model.Room);
                _unitOfWork.Save();
                return RedirectToAction("ListRoom", new { result = "success" });
            }
            return View(model);
        }
        public ActionResult UpdateRoom(int roomId = 0)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            if (room == null)
            {
                return RedirectToAction("ListRoom");
            }
            var model = new InsertRoomViewModel
            {
                Room = room,
                KindOfRooms = _unitOfWork.KindOfRoomRepository.Get(orderBy: o => o.OrderBy(a => a.Sort)),
            };
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateRoom(InsertRoomViewModel model, FormCollection fc)
        {
            var room = _unitOfWork.RoomRepository.GetById(model.Room.Id);
            if (room == null)
            {
                return RedirectToAction("ListRoom");
            }
            if (ModelState.IsValid)
            {
                room.ListImage = fc["Pictures"] == "" ? null : fc["Pictures"];
                room.Url = HtmlHelpers.ConvertToUnSign(null, model.Room.Url ?? model.Room.Name);
                room.Name = model.Room.Name;
                room.Body = model.Room.Body;
                room.Active = model.Room.Active;
                room.Description = model.Room.Description;
                room.Home = model.Room.Home;
                room.TitleMeta = model.Room.TitleMeta;
                room.DescriptionMeta = model.Room.DescriptionMeta;
                room.Sort = model.Room.Sort;
                room.ShowMenu = model.Room.ShowMenu;
                room.KindOfRoomsId = model.Room.KindOfRoomsId;
                room.Price = model.Room.Price;
                room.People = model.Room.People;
                room.View = model.Room.View;
                _unitOfWork.Save();

                var count = _unitOfWork.RoomRepository.GetQuery(a => a.Url == room.Url).Count();
                if (count > 1)
                {
                    room.Url += "-" + DateTime.Now.Millisecond;
                    _unitOfWork.Save();
                }
                return RedirectToAction("ListRoom", new { result = "update" });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteRoom(int roomId = 0)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            if (room == null)
            {
                return false;
            }
            _unitOfWork.RoomRepository.Delete(room);
            _unitOfWork.Save();
            return true;
        }
        #endregion
        #region Language
        public ActionResult UpadateRoomEn(int roomId , int result = 0)
        {
            ViewBag.Result = result;
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            return View(room);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpadateRoomEn(FormCollection fc)
        {
            var roomId = Convert.ToInt32(fc["roomId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var name = fc["Name"];
            var price = fc["Price"];
            var people = fc["People"];
            var sort = Convert.ToInt32(fc["Sort"]);
            var view = fc["View"];
            var titleMeta = fc["TitleMeta"];
            var descriptionMeta = fc["DescriptionMeta"];
            var body = fc["Body"];
            var description = fc["Description"];
            var url = fc["Url"];

            var conceptLang = _unitOfWork.RoomEnRepository.GetQuery(a => a.RoomId == roomId && a.LanguageId == langId).SingleOrDefault();

            if (conceptLang == null)
            {
                _unitOfWork.RoomEnRepository.Insert(new RoomEn
                {
                    RoomId = roomId,
                    LanguageId = langId,
                    Name = name,
                    Body = body,
                    Price = price,
                    People = people,
                    Sort = sort,
                    View = view,
                    Description = description,
                    TitleMeta = titleMeta,
                    DescriptionMeta = descriptionMeta,
                    Url = HtmlHelpers.ConvertToUnSign(null, url ?? name)
                });
                _unitOfWork.Save();
                return RedirectToAction("UpadateRoomEn", new { roomId, result = 1 });
            }

            conceptLang.Name = name;
            conceptLang.View = view;
            conceptLang.People = people;
            conceptLang.Price = price;
            conceptLang.Sort = sort;
            conceptLang.Description = description;
            conceptLang.Body = body;
            conceptLang.TitleMeta = titleMeta;
            conceptLang.DescriptionMeta = descriptionMeta;
            conceptLang.Url = HtmlHelpers.ConvertToUnSign(null, url ?? name);

            _unitOfWork.Save();

            return RedirectToAction("UpadateRoomEn", new { roomId, result = 1 });
        }
        public ActionResult UpadateRoomFr(int roomId, int result = 0)
        {
            ViewBag.Result = result;
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            return View(room);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpadateRoomFr(FormCollection fc)
        {
            var roomId = Convert.ToInt32(fc["roomId"]);
            var langId = Convert.ToInt32(fc["LangId"]);
            var sort = Convert.ToInt32(fc["Sort"]);
            var name = fc["Name"];
            var price = fc["Price"];
            var people = fc["People"];
            var view = fc["View"];
            var titleMeta = fc["TitleMeta"];
            var descriptionMeta = fc["DescriptionMeta"];
            var body = fc["Body"];
            var description = fc["Description"];
            var url = fc["Url"];

            var conceptLang = _unitOfWork.RoomFrRepository.GetQuery(a => a.RoomId == roomId && a.LanguageId == langId).SingleOrDefault();

            if (conceptLang == null)
            {
                _unitOfWork.RoomFrRepository.Insert(new RoomFr
                {
                    RoomId = roomId,
                    LanguageId = langId,
                    Name = name,
                    Body = body,
                    Sort = sort,
                    Price = price,
                    People = people,
                    View = view,
                    Description = description,
                    TitleMeta = titleMeta,
                    DescriptionMeta = descriptionMeta,
                    Url = HtmlHelpers.ConvertToUnSign(null, url ?? name)
                });
                _unitOfWork.Save();
                return RedirectToAction("UpadateRoomFr", new { roomId, result = 1 });
            }

            conceptLang.Name = name;
            conceptLang.View = view;
            conceptLang.Sort = sort;
            conceptLang.People = people;
            conceptLang.Price = price;
            conceptLang.Description = description;
            conceptLang.Body = body;
            conceptLang.TitleMeta = titleMeta;
            conceptLang.DescriptionMeta = descriptionMeta;
            conceptLang.Url = HtmlHelpers.ConvertToUnSign(null, url ?? name);

            _unitOfWork.Save();

            return RedirectToAction("UpadateRoomFr", new { roomId, result = 1 });
        }
        #endregion
        #region Property
        [ChildActionOnly]
        public ActionResult ListProperty()
        {
            var model = new ListPropertyViewModel
            {
                KindOfRooms = _unitOfWork.KindOfRoomRepository.Get(orderBy: o => o.OrderBy(a => a.Sort)),
            };
            return PartialView(model);
        }
        public ActionResult Property(string result = "", int typeId = 0)
        {
            ViewBag.PropertyResult = result;
            var model = new PropertyViewModel
            {
                Sort = 1,
                KindOfRoom = new KindOfRoom { Sort = 1 },
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Property(PropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.KindOfRoom.Name = model.Name;
                model.KindOfRoom.Sort = model.Sort;
                _unitOfWork.KindOfRoomRepository.Insert(model.KindOfRoom);

                _unitOfWork.Save();
                return RedirectToAction("Property", new { result = "success" });
            }
            return View(model);
        }
        public ActionResult UpdateProperty( int kindofRoom = 0, int typeId = 0)
        {
            var kindOfRoom = _unitOfWork.KindOfRoomRepository.GetById(kindofRoom);

            if (kindOfRoom == null)
            {
                return RedirectToAction("Property");
            }
            var model = new PropertyViewModel
            {
                KindOfRoom = kindOfRoom,
            };

            if (kindOfRoom != null)
            {
                model.Name = kindOfRoom.Name;
                model.Sort = kindOfRoom.Sort;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProperty(PropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.KindOfRoom != null)
                {
                    var kind = _unitOfWork.KindOfRoomRepository.GetById(model.KindOfRoom.Id);
                    if (kind == null)
                    {
                        return RedirectToAction("Property");
                    }

                    kind.Name = model.Name;
                    kind.Sort = model.Sort;
                    _unitOfWork.KindOfRoomRepository.Update(kind);
                }

                _unitOfWork.Save();
                return RedirectToAction("Property", new { result = "update" });
            }
            return View(model);
        }
        [HttpPost]
        public bool DeleteProperty(int kindOfRoom = 0)
        {
            var kind = _unitOfWork.KindOfRoomRepository.GetById(kindOfRoom);

            if (kind == null)
            {
                return false;
            }
            if (kind != null)
            {
                _unitOfWork.KindOfRoomRepository.Delete(kind);
            }
            _unitOfWork.Save();
            return true;
        }
        #endregion
    }
}
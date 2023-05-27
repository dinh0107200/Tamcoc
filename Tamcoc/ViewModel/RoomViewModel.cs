using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Tamcoc.Models;

namespace Tamcoc.ViewModel
{
    public class ListRoomViewModel
    {
        public PagedList.IPagedList<Room> Rooms { get; set; }
        public string Name { get; set; }
    }
    public class InsertRoomViewModel
    {
        public Room Room { get; set; }
        public IEnumerable<KindOfRoom> KindOfRooms { get; set; }
    }

    public class ListPropertyViewModel
    {
        public IEnumerable<KindOfRoom>  KindOfRooms{ get; set; }
    }
    public class PropertyViewModel
    {
        public KindOfRoom KindOfRoom { get; set; }
        [Display(Name = "Loại phòng"), Required(ErrorMessage = "Hãy nhập loại phòng"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên")]
        public int Sort { get; set; }
    }
}
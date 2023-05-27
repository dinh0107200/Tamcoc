using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Tamcoc.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Display(Name = "Tên phòng", Description = "Tiêu đề dài tối đa 150 ký tự"),
         Required(ErrorMessage = "Hãy nhập tiêu đề"), StringLength(150, ErrorMessage = "Tối đa 150 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thông tin phòng"), Required(ErrorMessage = "Hãy nhập trích dẫn ngắn"), UIHint("TextArea")]
        public string Description { get; set; }

        [Display(Name = "Chi tiết phòng"), UIHint("EditorBox")]
        public string Body { get; set; }

        [Display(Name = "Danh sách ảnh"), UIHint("UploadMultiFile")]
        public string ListImage { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Ngày đăng")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        [Display(Name="Giá tiền")]
        public string Price { get; set; }
        [Display(Name="Số người tiêu chuẩn")]
        public string People { get; set; }
        [Display(Name = "Hướng nhìn")]
        public string View { get; set; }

        [Display(Name = "Hiện trang chủ")]
        public bool Home { get; set; }
        [Display(Name = "Hiện menu")]
        public bool ShowMenu { get; set; }
        [StringLength(300)]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }

        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        public int? KindOfRoomsId { get; set; }
        public virtual ICollection<RoomEn> RoomEns { get; set; }
        public virtual ICollection<RoomFr> RoomFrs  { get; set; }
        public virtual ICollection<KindOfRoom> KindOfRooms { get; set; }


        public Room()
        {
            CreateDate = DateTime.Now;
            Active = true;
        }
    }
    public class RoomEn
    {
        [Key, Column(Order = 1)]
        public int RoomId { get; set; }
        [Key, Column(Order = 2)]
        public int LanguageId { get; set; }
        [Display(Name = "Tên phòng", Description = "Tiêu đề dài tối đa 150 ký tự"),
         Required(ErrorMessage = "Hãy nhập tiêu đề"), StringLength(150, ErrorMessage = "Tối đa 150 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thông tin phòng"), Required(ErrorMessage = "Hãy nhập trích dẫn ngắn"), UIHint("TextArea")]
        public string Description { get; set; }
        [Display(Name = "Giá tiền")]
        public string Price { get; set; }
        [Display(Name = "Số người tiêu chuẩn")]
        public string People { get; set; }
        [Display(Name = "Hướng nhìn")]
        public string View { get; set; }

        [Display(Name = "Chi tiết phòng"), UIHint("EditorBox")]
        public string Body { get; set; }
        [StringLength(300)]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }

        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        public virtual Room Room { get; set; }
        public virtual Language Language { get; set; }
    }
    public class RoomFr
    {
        [Key, Column(Order = 1)]
        public int RoomId { get; set; }
        [Key, Column(Order = 2)]
        public int LanguageId { get; set; }
        [Display(Name = "Tên phòng", Description = "Tiêu đề dài tối đa 150 ký tự"),
         Required(ErrorMessage = "Hãy nhập tiêu đề"), StringLength(150, ErrorMessage = "Tối đa 150 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thông tin phòng"), Required(ErrorMessage = "Hãy nhập trích dẫn ngắn"), UIHint("TextArea")]
        public string Description { get; set; }
        [Display(Name = "Giá tiền")]
        public string Price { get; set; }
        [Display(Name = "Số người tiêu chuẩn")]
        public string People { get; set; }
        [Display(Name = "Hướng nhìn")]
        public string View { get; set; }

        [Display(Name = "Chi tiết phòng"), UIHint("EditorBox")]
        public string Body { get; set; }
        [StringLength(300)]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }

        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        public virtual Room Room { get; set; }
        public virtual Language Language { get; set; }
    }
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string ListImage { get; set; }
        public string Price { get; set; }
        public string People { get; set; }
        public string View { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }    
        public bool Home { get; set; }
        public bool ShowMenu { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public string TitleMeta { get; set; }
        public string DescriptionMeta { get; set; }

    }
    public class KindOfRoom
    {
        public int Id { get; set; }
        [Display(Name = "Loại phòng", Description = "Độ dài tối đa 50 ký tự"), StringLength(50, ErrorMessage = "Tối đa 50 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên")]
        public int Sort { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Room>Rooms { get; set; }
        public KindOfRoom()
        {
            CreateDate = DateTime.Now;
        }
    }
}
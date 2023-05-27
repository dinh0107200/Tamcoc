using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tamcoc.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [Display(Name = "Họ tên", Description = "Tên dài tối đa 100 ký tự"),
       Required(ErrorMessage = "Hãy nhập Họ tên"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Chức vụ", Description = "Dài tối đa 100 ký tự"),
         StringLength(100, ErrorMessage = "Tối đa 100 ký tự"),
         UIHint("TextBox")]
        public string Classify { get; set; }
        [Display(Name = "Phản hồi mấy sao"), UIHint("NumberBox"), Range(1, 5, ErrorMessage = "Chỉ chọn từ 1 đến 5")]
        public int Star { get; set; }
        [Display(Name = "Lời nhận xét"), StringLength(700, ErrorMessage = "Tối đa 700 ký tự"), UIHint("TextArea")]
        public string Content { get; set; }

        [Display(Name = "Ảnh đại diện"), UIHint("ImageFeedback")]
        public string Image { get; set; }

        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public virtual ICollection<FeedbackEn> FeedbackEn { get; set; }
        public virtual ICollection<FeedbackFr> FeedbackFr { get; set; }
        public Feedback()
        {
            Active = true;
            Sort = 1;
        }
    }
    public class FeedbackEn
    {
        [Key, Column(Order = 1)]
        public int FeedbackId { get; set; }
        [Key, Column(Order = 2)]
        public int LanguageId { get; set; }
        [Display(Name = "Họ tên", Description = "Tên dài tối đa 100 ký tự"),
       Required(ErrorMessage = "Hãy nhập Họ tên"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Chức vụ", Description = "Dài tối đa 100 ký tự"),
         StringLength(100, ErrorMessage = "Tối đa 100 ký tự"),
         UIHint("TextBox")]
        public string Classify { get; set; }
        [Display(Name = "Phản hồi mấy sao"), UIHint("NumberBox"), Range(1, 5, ErrorMessage = "Chỉ chọn từ 1 đến 5")]
        public int Star { get; set; }
        [Display(Name = "Lời nhận xét"), StringLength(700, ErrorMessage = "Tối đa 700 ký tự"), UIHint("TextArea")]
        public string Content { get; set; }

        [Display(Name = "Ảnh đại diện"), UIHint("ImageFeedback")]
        public string Image { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public virtual Language Language { get; set; }
        public virtual Feedback Feedback { get; set; }

    }
    public class FeedbackFr
    {
        [Key, Column(Order = 1)]
        public int FeedbackId { get; set; }
        [Key, Column(Order = 2)]
        public int LanguageId { get; set; }
        [Display(Name = "Họ tên", Description = "Tên dài tối đa 100 ký tự"),
       Required(ErrorMessage = "Hãy nhập Họ tên"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Chức vụ", Description = "Dài tối đa 100 ký tự"),
         StringLength(100, ErrorMessage = "Tối đa 100 ký tự"),
         UIHint("TextBox")]
        public string Classify { get; set; }
        
        [Display(Name = "Lời nhận xét"), StringLength(700, ErrorMessage = "Tối đa 700 ký tự"), UIHint("TextArea")]
        public string Content { get; set; }
        [Display(Name = "Phản hồi mấy sao"), UIHint("NumberBox"), Range(1, 5, ErrorMessage = "Chỉ chọn từ 1 đến 5")]
        public int Star { get; set; }

        [Display(Name = "Ảnh đại diện"), UIHint("ImageFeedback")]
        public string Image { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Language Language { get; set; }
    }
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Classify { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Sort { get; set; }
        public bool Active { get; set; }
    }
}
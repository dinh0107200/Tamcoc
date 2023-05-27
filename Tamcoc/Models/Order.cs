using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Tamcoc.Properties;

namespace Tamcoc.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Tên ")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "Id dự án")]
        public int? RoomId { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual Room Room { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
    }
    [ComplexType]
    public class CustomerInfo
    {
        [Display(ResourceType = typeof(Resources), Name = "FullName"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), UIHint("TextBox"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string FullName { get; set; }
        [Display(ResourceType = typeof(Resources) , Name = "Address"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required" ), UIHint("TextBox"), StringLength(300, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "NumberPhone"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), Phone(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "InValid"), UIHint("TextBox")]
        public string Mobile { get; set; }
        [Display(Name = "Email"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength"), EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "InValid"), UIHint("TextBox")]
        public string Email { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "CheckIn"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), UIHint("TextBox"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string CheckIn { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "CheckOut"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), UIHint("TextBox"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string CheckOut { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Children"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), UIHint("TextBox"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string Children { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Adults"), Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required"), UIHint("TextBox"), StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Maxlength")]
        public string Adults { get; set; }
        [Display(ResourceType = typeof(Resources), Name = "Body"), DataType(DataType.MultilineText), StringLength(4000)]
        public string Body { get; set; }
    }
}
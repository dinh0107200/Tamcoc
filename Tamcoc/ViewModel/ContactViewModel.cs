using System.Collections.Generic;
using System.Web.Mvc;
using Tamcoc.Models;

namespace Tamcoc.ViewModel
{
    public class ListContactViewModel
    {
        public PagedList.IPagedList<Contact> Contacts { get; set; }
        public string Name { get; set; }
    }
    public class ListSubcribeViewModel
    {
        public PagedList.IPagedList<Subcribe> Subcribes { get; set; }
        public string Name { get; set; }
    }

    public class ListFeedbackViewModel
    {
        public PagedList.IPagedList<Feedback> Feedbacks { get; set; }
        public string Name { get; set; }
    }
    public class ListOrderViewModel
    {
        public PagedList.IPagedList<Order> Orders { get; set; }
        public string Name { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

    }
}
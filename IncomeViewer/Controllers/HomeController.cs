using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IncomeViewer.Models;
using Microsoft.Owin.Security;

namespace IncomeViewer.Controllers
{
    public class PageInfo
    {
        public string SortFieldId { get; set; }
        public string SortDirection { get; set; }
    }

    public class DataItem
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public DataItem(int id, string name, decimal quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }



    public class HomeController : Controller
    {

        private List<DataItem> data;

        public HomeController()
        {
            data = new List<DataItem>();

            data.Add(new DataItem(1, "cc", 23));
            data.Add(new DataItem(2, "dd", 45));
            data.Add(new DataItem(3, "aa", 87));
        }


//        public ActionResult Index()
//        {
//            var pageInfo = new PageInfo();
//            pageInfo.SortFieldId = "Id";
//            pageInfo.SortDirection = "asc";
//            
//            var model = data.OrderBy(d => d.Id).ToList();
//            ViewBag.PageInfo = pageInfo;
//
//
//            var vm = new ViewModel<DataItem>();
//            vm.AddTable(model);
//
//
//            return View(vm);
//        }

        public ActionResult Index()
        {
            var sort = Request.QueryString["sort"];
            sort = string.IsNullOrEmpty(sort) ? "Id" : sort;

            var sortDirection = Request.QueryString["direction"];
            sortDirection = string.IsNullOrEmpty(sortDirection) ? "asc" : sortDirection;

            var list = data.ToList();
            
            var prop = TypeDescriptor.GetProperties(typeof (DataItem)).Find(sort, true);
            if(sortDirection=="asc")
                list = list.OrderBy(a => prop.GetValue(a)).ToList();
            else
                list = list.OrderByDescending(a => prop.GetValue(a)).ToList();
            

            var vm = new ViewModel<DataItem>();
            vm.AddTable(list);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(PageInfo pageInfo)
        {
            IQueryable<DataItem> query = null;

            switch (pageInfo.SortFieldId)
            {
                case "Id":
                    query = pageInfo.SortDirection == "asc"
                        ? data.OrderBy(a => a.Id).AsQueryable()
                        : data.OrderByDescending(a => a.Id).AsQueryable();
                    break;
                case "Name":
                    query = pageInfo.SortDirection == "asc"
                        ? data.OrderBy(a => a.Name).AsQueryable()
                        : data.OrderByDescending(a => a.Name).AsQueryable();
                    break;
            }

            ViewBag.PageInfo = pageInfo;


            var vm = new ViewModel<DataItem>();
            vm.AddTable(query.ToList());
            return View(vm);
        }

        
    }
}
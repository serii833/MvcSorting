using System;
using System.Web.Mvc;
using IncomeViewer.Controllers;
using IncomeViewer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IncomeVIewer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new HomeController() ;
            var res = controller.Index() as ViewResult;
            var vm = res.Model as ViewModel<DataItem>;

            var rows = vm.Table;

            var q = 1;
        }
    }
}

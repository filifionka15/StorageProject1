using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using StorageProject.Models;

namespace StorageProject.Controllers
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsIndexViewResult()
        {
            HomeController controller = new HomeController();
            var result = controller.Index();
            //Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);


        }
        [Test]
        public void AvgMaterialsIndexNotNull()
        {
            StorageEntities4 db = new StorageEntities4();
            AvgMaterialsController controller = new AvgMaterialsController();
            var result = controller.Index("11.11.2019","13.11.2019");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }


    }
}
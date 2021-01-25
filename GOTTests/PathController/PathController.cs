using System;
using Xunit;
using GOT.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace GOTTests.PathController {
    public class PathController {
        [Fact]
        public async Task DeleteConfirmedReturnsToIndex() {
            //Arrange
            //We need tempData mock because controller initialized outside of ASP.NET has no TempData dict
            //var controller = new GOT.Controllers.PathController();
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            //controller.TempData = tempData;

            int deletedEntityId = 1;

           // var result = await controller.DeleteConfirmed(deletedEntityId);

            //var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}

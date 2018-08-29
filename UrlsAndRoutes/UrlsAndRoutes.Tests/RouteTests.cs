using System;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {
            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Action - process the route
            var result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null)
        {
            bool ValCompare(object v1, object v2)
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            }

            var result = ValCompare(routeResult.Values["controller"], controller) && ValCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                var propInfo = propertySet.GetType().GetProperties();
                foreach (var pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name) && ValCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private void TestRouteFail(string url)
        {
            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Action - process the route
            var result = routes.GetRouteData(CreateHttpContext(url));
            // Assert
            Assert.IsTrue(result?.Route == null);
        }

        [TestMethod]
        public void TestImcomingRoutes()
        {
            // For MyRoute
            //// check for the URL that is hoped for
            //TestRouteMatch("~/Admin/Index", "Admin", "Index");

            //// check that the values are being obtained from the segments
            //TestRouteMatch("~/One/Two", "One", "Two");

            //// ensure that too many or few segments fails to match
            //TestRouteFail("~/Admin/Index/Segment");
            //TestRouteFail("~/Admin");

            // For MyOtherRoute route
            //TestRouteMatch("~/", "Home", "Index");
            //TestRouteMatch("~/Customer", "Customer", "Index");
            //TestRouteMatch("~/Customer/List", "Customer", "List");
            //TestRouteFail("~/Cutomer/List/All");
            //TestRouteMatch("~/Shop/Index", "Home", "Index");

            // For all routes with default value id = "DefaultId"
            //TestRouteMatch("~/", "Home", "Index", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer", "Customer", "Index", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer/List", "Customer", "List", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "All" });
            //TestRouteFail("~/Customer/List/All/Delete");

            // For all routes with optional param with no *catchall
            //TestRouteMatch("~/", "Home", "Index");
            //TestRouteMatch("~/Customer", "Customer", "Index");
            //TestRouteMatch("~/Customer/List", "Customer", "List");
            //TestRouteMatch("~/Customer/List/All", "Customer", "List");
            //TestRouteFail("~/Customer/List/All/Delete");

            // For all routes with default param in controller with *catchall
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All", "Customer", "List", new {id = "All"});
            TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", new { id = "All", catchall = "Delete"});
            TestRouteMatch("~/Customer/List/All/Delete/Perm", "Customer", "List", new { id = "All", catchall = "Delete/Perm"});
        }
    }
}

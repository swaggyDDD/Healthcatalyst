namespace Catalyst.Tests.TestHelpers.Fakes
{
    using System.IO;
    using System.Security.Principal;
    using System.Web;

    public class FakeHttpContext
    {
        public static HttpContext Get()
        {
            return new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""), 
                new HttpResponse(new StringWriter()));
        }

        public static HttpContext GetLoggedIn(string userName = "username")
        {
            var ctx = Get();
            ctx.User = new GenericPrincipal(
            new GenericIdentity(userName),
            new string[0]);

            return ctx;
        }

        public static HttpContext GetLoggedOut()
        {
            var ctx = Get();
            ctx.User = new GenericPrincipal(
            new GenericIdentity(string.Empty),
            new string[0]);

            return ctx;
        }
    }
}
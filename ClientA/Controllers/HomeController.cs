using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace ClientA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                var casUrl = ConfigurationManager.AppSettings["CasUrl"].ToString();
                var appUrl = Request.Url;
                return Redirect(casUrl + "/Cas/CasLogin?app=" + appUrl);
            }
            return View();
        }

        public ActionResult Login(string token)
        {
            var casUrl = ConfigurationManager.AppSettings["CasUrl"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(casUrl + $"/Cas/Validate?token={token}");
            Stream myResponseStream = request.GetResponse().GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            if (string.IsNullOrEmpty(retString))
            {
                return Redirect(casUrl + "/Cas/CasLogin");
            }
            var userid = retString;
            Session["UserID"] = userid;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using CAS.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CAS.App.Controllers
{
    public class CasController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            LoginUser user = new LoginUser { };
            if (Session["UserID"] != null)
            {
                user.UserID = (int)Session["UserID"];
            }
            else
            {
                return RedirectToAction("LoginIndex", "Cas");
            }
            return View();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginIndex()
        {
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public ActionResult Login(LoginUser loginUser)
        {
            if (loginUser.UserID == 92225)
            {
                Session["UserID"] = loginUser.UserID;
                if (!string.IsNullOrEmpty(loginUser.App))
                {
                    return Redirect($"{loginUser.App}");
                }
                return RedirectToAction("Index", "Cas");
            }
            else
            {
                return RedirectToAction("LoginIndex", "Cas");
            }
        }

        public ActionResult CasLogin(string app)
        {
            if (IsConfigedApp(app) && Session["UserID"] != null)
            {
                var loginUserID = (int)Session["UserID"];
                var cacheKey = Guid.NewGuid().ToString("N");
                HttpRuntime.Cache.Insert(cacheKey, loginUserID);
                return Redirect(app + "/Home/Login?token=" + cacheKey);
            }
            return Redirect($"/Cas/LoginIndex?app={app}");
        }

        public string Validate(string token)
        {
            var loginUserID = HttpRuntime.Cache.Get(token);
            return loginUserID.ToString();
        }

        #region 
        public bool IsConfigedApp(string app)
        {
            return true;
        }
        #endregion
    }
}
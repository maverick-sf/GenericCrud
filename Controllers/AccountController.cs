using Final_assignment.Models;
using Final_assignment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Final_assignment.Controllers
{


    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController()
        {
            _accountRepository = new AccountRepository();
        }
      // [Bind(Include = "Email")]
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }                                   
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreasteAsync(model);
                ModelState.Clear();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [Route("login")]
        [Route("account/login")]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

    }
}
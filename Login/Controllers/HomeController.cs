using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        RegtrationtestEntities db = new RegtrationtestEntities();
        public ActionResult Create()
        {
            User obj = new User();
            return View("Create", obj);
        }

        [HttpPost]
        public ActionResult Create(User obj)
        {
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(obj.Emailid);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(obj);

                }
                obj.Password = Crypto.Hash(obj.Password);
                db.Users.Add(obj);
                db.SaveChanges();
            }
            var Message = "Registration Successfull " + obj.Emailid;
            ViewBag.Message = Message;
            return View("Create", obj);
        }


        public ActionResult Display()
        {
            var datashow = db.Users.ToList();
            return View("Display", datashow);
        }

        //Display to the textbox
        public ActionResult Update(int personid)
        {
            var personrec = (from item in db.Users
                             where item.ID == personid
                             select item).FirstOrDefault();
            return View("Update", personrec);
        }

        [HttpPost]
        public ActionResult Update(User obj)
        {

            var personrec = (from item in db.Users
                             where item.ID == obj.ID
                             select item).FirstOrDefault();

            personrec.Firstname = obj.Firstname;
            personrec.LastName = obj.LastName;
            personrec.Address = obj.Address;
            personrec.Mobileno = obj.Mobileno;
            personrec.Emailid = obj.Emailid;
            personrec.Password = obj.Password;


            db.SaveChanges();

            var datashow = db.Users.ToList();
            return View("Display", datashow);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(log login)
        {
            if (ModelState.IsValid)
            {
                var _passWord = Crypto.Hash(login.Password);
                bool Isvalid = db.Users.Any(x => x.Emailid == login.Emailid && x.Password == _passWord);

                if (Isvalid)
                {
                    return RedirectToAction("Display", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email id and Password not match");
                }
            }
            return View("Login", login);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            var v = db.Users.Where(a => a.Emailid == emailID).FirstOrDefault();
            return v != null;
        }
    }
}
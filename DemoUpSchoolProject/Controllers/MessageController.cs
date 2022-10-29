﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoUpSchoolProject.Models.Entities;

namespace DemoUpSchoolProject.Controllers
{
    public class MessageController : Controller
    {
        UpSchoolDbPortfolioEntities db = new UpSchoolDbPortfolioEntities();
        public ActionResult Inbox()
        {
            var mail = Session["MemberEmail"].ToString();
            var values = db.TblMessage.Where(x => x.ReceiverMail == mail).ToList();
            return View(values);
        }  
        public ActionResult OutBox()
        {
            var mail = Session["MemberEmail"].ToString();
            var values = db.TblMessage.Where(x => x.SenderMail == mail).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(TblMessage p)
        {
            var mail = Session["MemberEmail"].ToString();
            p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.SenderMail = mail;
            p.SenderNameSurname = db.TblMember.Where(x => x.MemberEmail == mail).Select(y => y.MemberName + " " + y.MemberSurname).FirstOrDefault();
            p.ReceiverNameSurname = db.TblMember.Where(x => x.MemberEmail == p.ReceiverMail).Select(y => y.MemberName + " " + y.MemberSurname).FirstOrDefault();
            db.TblMessage.Add(p);
            db.SaveChanges();
            return RedirectToAction("Outbox");
        }
        public ActionResult MessageDetails()
        {
            return View();
        }



    }
}
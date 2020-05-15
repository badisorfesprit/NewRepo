using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web.Controllers
{
    public class PublicationController : Controller

        
    {

        IUserService userserv = new UserService();
        IPublicationService pubserv = new PublicationService();
        IReplyService repserv = new ReplyService();
        ILikeService likeserv = new LikeService();

        // GET: Publication
        public ActionResult Index()
        {
            var Publications = new List<PublicationVM>();
            foreach (Publication p in pubserv.GetMany())
            {
                Publications.Add(new PublicationVM()
                {
                    title = p.title,
                    description = p.description,
                    creationDate = p.creationDate,
                    image = p.image,
                    nomuser = p.nomuser,
                    ownerimg = p.ownerimg,
                    OwnerId = p.OwnerId,
                    PublicationId = p.PublicationId,
                    visibility = (VisibilityVM)p.visibility
                });

            }
            return View(Publications);
        }

        // GET: Publication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publication/Create
        [HttpPost]
        public ActionResult Create(PublicationVM PublicationVM, HttpPostedFileBase Image)
        {
            Publication PublicationDomain = new Publication();


            PublicationDomain.title = PublicationVM.title;
            PublicationDomain.creationDate = DateTime.UtcNow;
            PublicationDomain.description = PublicationVM.description;
            PublicationDomain.visibility = (Visibility)PublicationVM.visibility;
            PublicationDomain.image = Image.FileName;
            PublicationDomain.nomuser = "stella007"; //User.Identity.GetUserName();
            PublicationDomain.OwnerId = 1;//User.Identity.GetUserId();
            PublicationDomain.ownerimg = "Capture d’écran(7).png"; // MyUser.GetById(User.Identity.GetUserId()).image;




            pubserv.Add(PublicationDomain);
            pubserv.Commit();

            var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path);
            return RedirectToAction("Index");
        }

        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Publication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

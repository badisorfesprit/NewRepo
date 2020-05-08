using Solution.Domain.Entities;
using Solution.Web.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Solution.Web.Controllers
{
    public class ReclamationPController : Controller
    {
        IReclamationPService Service = new ReclamationPService();
        // GET: ReclamationP
        public ActionResult Index(string searchString)
        {
            List<ReclamationPVM> reclams = new List<ReclamationPVM>();
            List<ReclamationP> reclamationsP = Service.GetMany().ToList();
            return View(reclamationsP);
        }

        // GET: ReclamationP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReclamationP p;
            p = Service.GetById((int)id);
            if (p == null)
            {

                return HttpNotFound();
            }
            ReclamationPVM rvm = new ReclamationPVM()
            {
                ReclamationPId = p.ReclamationPId,
                Nom = p.Nom,
                Ville = p.Ville,
                Genre = (GenrePVM)p.Genre,
                CodePostale = p.CodePostale,
                Comment = p.Comment,
                DateReclamation = p.DateReclamation,
                ComplaintType = (ComplaintPVM)p.ComplaintType,

            };

            return View(rvm);
        }

        // GET: ReclamationP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReclamationP/Create
        [HttpPost]
        public ActionResult Create(ReclamationPVM ReclamPVM)
        {
            ReclamationP r = new ReclamationP();

            r.ReclamationPId = ReclamPVM.ReclamationPId;
            r.Nom = ReclamPVM.Nom;
            r.Ville = ReclamPVM.Ville;
            r.Genre = (Genre)ReclamPVM.Genre;
            r.CodePostale = ReclamPVM.CodePostale;
            r. Comment = ReclamPVM.Comment;
            r.DateReclamation = ReclamPVM.DateReclamation;
            r.ComplaintType = (ComplaintP)ReclamPVM.ComplaintType;


            Service.Add(r);
            Service.Commit();
            return (RedirectToAction("Index"));
        }

        // GET: ReclamationP/Edit/5
        public ActionResult Edit(int id)
        {
            ReclamationP p = Service.GetById((int)id);

            ReclamationPVM pm = new ReclamationPVM()
            {
                Nom = p.Nom,
                Ville = p.Ville,
                Genre = (GenrePVM)p.Genre,
                CodePostale =p.CodePostale,
                ComplaintType = (ComplaintPVM)p.ComplaintType,
                DateReclamation = p.DateReclamation,
                Comment = p.Comment,



             

        };
            return View(pm);
        }

        // POST: ReclamationP/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReclamationP comp)
        {
            try
            {

                ReclamationP r = Service.GetById((int)id);
                r.Nom = comp.Nom;
                r.Ville = comp.Ville;
                r.Genre = (Genre)comp.Genre;
                r.CodePostale = comp.CodePostale;
                r.Comment = comp.Comment;
                r.DateReclamation = comp.DateReclamation;
                r.ComplaintType = (ComplaintP)comp.ComplaintType;
                Service.Update(r);
                Service.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(comp);
            }

        }

        // GET: ReclamationP/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReclamationP/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ReclamationP comp = Service.GetById(id);


            Service.Delete(comp);
            Service.Commit();
            return RedirectToAction("Index");


            return View();
        }
    }
}

using Solution.Domain.Entities;
using Solution.Web.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

//using Solution.Service;

namespace Solution.Web.Controllers
{
    public class ReclamationController : Controller
    {
        
        IReclamationService Service = new ReclamationService();
        // GET: Reclamation
        public ActionResult Index(string searchString)
        {
            List<ReclamationVM> reclams = new List<ReclamationVM>();
            List<Reclamation> reclamations = Service.GetMany().ToList();
            return View(reclamations);
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation p;
            p = Service.GetById((int)id);
            if (p == null)
            { 

                return HttpNotFound();
            }
            ReclamationVM rvm = new ReclamationVM()
            {
                ComplaintType = (ComplaintVM)p.ComplaintType,
                DateReclamation = p.DateReclamation,
                //OwnerId=p.OwnerId,
                Comment = p.Comment

            };

            return View(rvm);
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(ReclamationVM ReclamVM)
        {
            Reclamation r = new Reclamation();

            r.ReclamationId = ReclamVM.ReclamationId;
            r.Comment = ReclamVM.Comment;
            r.DateReclamation = ReclamVM.DateReclamation;
            r.ComplaintType = (Complaint)ReclamVM.ComplaintType;


            
            Service.Add(r);
            Service.Commit();
            return (RedirectToAction("Index"));



        }

        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {
            Reclamation p = Service.GetById((int)id);
          
            ReclamationVM pm = new ReclamationVM()
            {
                ComplaintType = (ComplaintVM)p.ComplaintType,
                DateReclamation = p.DateReclamation,
                Comment = p.Comment,
                
            };
            return View(pm);
        }

        // POST: Reclamation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Reclamation comp)
        {

            try
            {

                Reclamation r = Service.GetById((int)id);
                r.ComplaintType = (Complaint)comp.ComplaintType;
                r.DateReclamation = comp.DateReclamation;
                r.Comment = comp.Comment;
                Service.Update(r);
                Service.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(comp);
            }


        }

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reclamation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Reclamation comp = Service.GetById(id);

            
                Service.Delete(comp);
                Service.Commit();
                return RedirectToAction("Index");
           
           
            return View();
        }

        public ActionResult Dashboard()
        {
            var list = Service.GetMany();
            List<int> repartition = new List<int>();
            var states = list.Select(x => x.ComplaintType).Distinct();
            foreach (var item in states)
            {

                repartition.Add(list.Count(x => x.ComplaintType == item));
            }
           var rep = repartition;
            ViewBag.type = states;
            ViewBag.REP = repartition.ToList();


            return View();
        }


    }
}

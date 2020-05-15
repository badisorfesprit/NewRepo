using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Solution.Domain.Entities;
using Solution.Web.Models;
using Solution.Service;
using Solution.Domain.Entities;
using Solution.Web.Models;
using System.Net.Http;
using System.Net;

namespace Solution.Web.Controllers
{
    public class WSReclamationController : ApiController
    {

        IReclamationService Service = new ReclamationService();
        ReclamationService rs = new ReclamationService();
        ResponseService responseService = new ResponseService();
        IUserService us = new UserService();
        // GET: WSReclamation
        // get Reclamation sent bu userId , request de type get
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/sent/{id}")]
        public HttpResponseMessage GetSentComplaints(int id)
        {
            var events = rs.GetMany(t => t.sender.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, events, Configuration.Formatters.JsonFormatter);
        }
        // get Reclamation received bu userId , equest de type get
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/received/{id}")]
        public HttpResponseMessage GetReceivedComplaints(int id)
        {

            var events = rs.GetMany(t => t.receiver.Id == id);

            return Request.CreateResponse(HttpStatusCode.OK, events, Configuration.Formatters.JsonFormatter);
        }
        // get  Reclamation by id , equest de type get
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/{id}")]
        public HttpResponseMessage GetComplaints(int id)
        {

            var events = rs.GetById(id);

            return Request.CreateResponse(HttpStatusCode.OK, events, Configuration.Formatters.JsonFormatter);
        }
        // Delete Reclamation , equest de type Delete
        [System.Web.Http.HttpDelete]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/{id}")]
        public HttpResponseMessage deleteComplaint(int id)
        {
            try
            {
                rs.Delete(rs.GetById(id));

                return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, Configuration.Formatters.JsonFormatter);
            }
        }
        // Ajouter Reclamation , equest de type post
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/{id}")]
        public HttpResponseMessage addComplaint(int id, ReclamationVM rec)
        {
            try
            {
                Reclamation r = new Reclamation();
                r.Comment = rec.Comment;
                r.DateReclamation = DateTime.Now;
                r.ComplaintType = (Complaint)rec.ComplaintType;
                r.senderID = rec.senderID;
                r.receiverID = rec.senderID;
                rs.Add(r);
                rs.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, Configuration.Formatters.JsonFormatter);
            }
        }

        //update Reclamation , request de type put
        [System.Web.Http.HttpPut]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reclamation/{id}")]
        public HttpResponseMessage updateComplaint(int id,ReclamationVM rec)
        {

            try
            {
                Reclamation r = Service.GetById((int)id);
                r.ComplaintType = (Complaint)rec.ComplaintType;
                r.DateReclamation = DateTime.Now;
                r.Comment = rec.Comment;
                Service.Update(r);
                Service.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, Configuration.Formatters.JsonFormatter);
            }
            
        }
        // get Response  by ReclamationID , equest de type get
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reponse/{id}")]
        public HttpResponseMessage getReponsesByReclamation(int id)
        {

            try
            {
                Reclamation rec = rs.GetById(id);
                if (rec !=  null)
                {
                    var reps = responseService.GetMany(t => t.reclamationID == id);
                    return Request.CreateResponse(HttpStatusCode.OK, reps, Configuration.Formatters.JsonFormatter);

                }
                else
                {
                    var reps = responseService.GetMany(t => t.reclamationID == id);
                    return Request.CreateResponse(HttpStatusCode.OK, reps, Configuration.Formatters.JsonFormatter);

                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, Configuration.Formatters.JsonFormatter);
            }

        }
        // add Response  , equest de type get
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Reponse/{id}")]
        public HttpResponseMessage addResponse(int id,ResponseVM resp)
        {

            try
            {
                Reclamation reclam = rs.GetById((int)id);
                var sender = us.GetById((int)resp.authorID);
                Response r = new Response();
                r.response = resp.response;
                r.DateResponse = DateTime.Now;
                r.authorId = resp.authorID;
                r.reclamationID = resp.reclamationID;

                responseService.Add(r);
                responseService.Commit();
                if (reclam.receiver.Id == resp.authorID)
                {
                    reclam.state = ComplaintState.traited;
                    rs.Update(reclam);
                    rs.Commit();
                }
                return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, Configuration.Formatters.JsonFormatter);
            }

        }

    }
}
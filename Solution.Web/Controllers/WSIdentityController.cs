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

namespace Solution.Web.Controllers
{
    public class WSIdentityController : ApiController
    {
        // GET: WSIdentity
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        UserService us = new UserService();

        public WSIdentityController()
        {
        }

        public WSIdentityController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //


        //
        // POST: /Account/Login
        [System.Web.Http.HttpPost]

        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();

                return Ok(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok(model);
                case SignInStatus.LockedOut:
                    return Ok();
                case SignInStatus.RequiresVerification:
                    return Ok();
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return Ok();
            }
        }
        // POST: /Account/Register
        [System.Web.Http.HttpPost]

        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (model.Role == "Parent")
            {
                var user = new Parent
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PasswordHash = model.Password,
                    DateOfBirth = model.BirthDate,
                    Role = model.Role
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //  await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Pour plus d'informations sur l'activation de la confirmation du compte et la réinitialisation du mot de passe, consultez http://go.microsoft.com/fwlink/?LinkID=320771
                    // Envoyer un message électronique avec ce lien
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");

                    return Ok(model);

                }

                // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
                return Ok("Echec");
            }
            
            else
            {

                var user = new KinderGardeen
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PasswordHash = model.Password,
                    DateOfBirth = model.BirthDate,
                    Role = model.Role
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //  await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Pour plus d'informations sur l'activation de la confirmation du compte et la réinitialisation du mot de passe, consultez http://go.microsoft.com/fwlink/?LinkID=320771
                    // Envoyer un message électronique avec ce lien
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");

                    return Ok(model);

                }

                // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
                return Ok("Echec");
            }

        }
        
        
        // POST: /Account/Test
        [System.Web.Http.HttpGet]

        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Test")]
        public async Task<IHttpActionResult> Test()
        {
            return Ok("OK");

        }


        // POST: /Account/Test
        [System.Web.Http.HttpGet]

        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/Users")]
        public  IEnumerable<User> getAllUsers()
        {
           
            var users = us.GetMany();
            return users;

        }
    }
}

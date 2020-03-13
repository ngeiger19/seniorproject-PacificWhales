using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Harmony.DAL;
using Harmony.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Validation;
using Calendar.ASP.NET.MVC5;

namespace Harmony.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        HarmonyContext db = new HarmonyContext();
        public AccountController()
        { 
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
            string data = sr.ReadToEnd();
            JArray arr = JArray.Parse(data);
            List<SelectListItem> stateList = new List<SelectListItem>();
            for (int i = 0; i < arr.Count(); i++)
            {
                stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
            }
            ViewData.Clear();
            // ViewBag.State = stateList;
            ViewData["State"] = stateList;
            sr.Dispose();
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
              //  var user = new ApplicationUser { UserName = model.FirstName + " " + model.LastName, Email = model.Email};
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    await this.UserManager.AddToRoleAsync(user.Id, model.Role);
                    /*StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
                    string data = sr.ReadToEnd();
                    JArray arr = JArray.Parse(data);
                    for (int i = 0; i < arr.Count(); i++)
                    {
                        model.stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
                    }*/
                    User HarmonyUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = user.Email,
                        City = model.City,
                        State = model.State,
                        Description = model.Description,
                        ASPNetIdentityID = user.Id
                    };
                    db.Users.Add(HarmonyUser);
                    
                    if (model.Role == "VenueOwner")
                    {
                        VenueType venueType = new VenueType
                        {
                            TypeName = model.VenueType
                        };
                        Venue venue = new Venue
                        {
                            VenueName = model.VenueName,
                            AddressLine1 = model.AddressLine1,
                            AddressLine2 = model.AddressLine2,
                            City = model.VenueCity,
                            State = model.VenueState,
                            ZipCode = model.ZipCode,
                            UserID = HarmonyUser.ID,
                            VenueTypeID = venueType.ID
                        };
                            db.VenueTypes.Add(venueType);
                            db.Venues.Add(venue);
                    }
                    if(model.Role == "Musician")
                    {
                        List<Genre> genres = new List<Genre>();
                        List<Instrument> instruments = new List<Instrument>();
                        List<BandMember> bandmembers = new List<BandMember>();
                        List<string> genreList = new List<string>();
                        List<string> instrumentList = new List<string>();
                        List<string> bandmemberList = new List<string>();
                        genreList = model.GenreName.Split(',').ToList();
                        instrumentList = model.InstrumentName.Split(',').ToList();
                        bandmemberList = model.BandMemberName.Split(',').ToList();
                        for (int i = 0; i < genreList.Count(); i++)
                        {
                            genres.Add(new Genre
                            {
                                GenreName = genreList[i]
                            });
                            db.Genres.Add(genres[i]);
                            HarmonyUser.Genres.Add(genres[i]);
                        }
                        for (int i = 0; i < instrumentList.Count(); i++)
                        {
                            instruments.Add(new Instrument
                            {
                                InstrumentName = instrumentList[i],
                            });
                            db.Instruments.Add(instruments[i]);
                        }
                        for (int i = 0; i < bandmemberList.Count(); i++)
                        {
                            bandmembers.Add(new BandMember
                            {
                                BandMemberName = bandmemberList[i],
                                UserID = HarmonyUser.ID
                            });
                            bandmembers[i].Instruments.Add(instruments[i]);
                            db.BandMembers.Add(bandmembers[i]);

                           // db.BandMember_Instrument.Add(new BandMember_Instrument { BandMemberID = bandmembers[i].ID, InstrumentID = instruments[i].ID });     
                        }

                    }
                    
                    await db.SaveChangesAsync();
                    return RedirectToAction("Welcome", "Home");
                }
                AddErrors(result);
            }
            StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
            string data = sr.ReadToEnd();
            JArray arr = JArray.Parse(data);
            List<SelectListItem> stateList = new List<SelectListItem>();
            for (int i = 0; i < arr.Count(); i++)
            {
                stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
            }
            // ViewBag.State = stateList;
            ViewData["State"] = stateList;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification: 
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        await this.UserManager.AddToRoleAsync(user.Id, model.Role);
                        /*StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
                        string data = sr.ReadToEnd();
                        JArray arr = JArray.Parse(data);
                        for (int i = 0; i < arr.Count(); i++)
                        {
                            model.stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
                        }*/
                        User HarmonyUser = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = user.Email,
                            City = model.City,
                            State = model.State,
                            Description = model.Description,
                            ASPNetIdentityID = user.Id
                        };
                        db.Users.Add(HarmonyUser);

                        if (model.Role == "VenueOwner")
                        {
                            VenueType venueType = new VenueType
                            {
                                TypeName = model.VenueType
                            };
                            Venue venue = new Venue
                            {
                                VenueName = model.VenueName,
                                AddressLine1 = model.AddressLine1,
                                AddressLine2 = model.AddressLine2,
                                City = model.VenueCity,
                                State = model.VenueState,
                                ZipCode = model.ZipCode,
                                UserID = HarmonyUser.ID,
                                VenueTypeID = venueType.ID
                            };
                            db.VenueTypes.Add(venueType);
                            db.Venues.Add(venue);
                        }
                        if (model.Role == "Musician")
                        {
                            List<Genre> genres = new List<Genre>();
                            List<Instrument> instruments = new List<Instrument>();
                            List<BandMember> bandmembers = new List<BandMember>();
                            List<string> genreList = new List<string>();
                            List<string> instrumentList = new List<string>();
                            List<string> bandmemberList = new List<string>();
                            genreList = model.GenreName.Split(',').ToList();
                            instrumentList = model.InstrumentName.Split(',').ToList();
                            bandmemberList = model.BandMemberName.Split(',').ToList();
                            for (int i = 0; i < genreList.Count(); i++)
                            {
                                genres.Add(new Genre
                                {
                                    GenreName = genreList[i]
                                });
                                db.Genres.Add(genres[i]);
                                HarmonyUser.Genres.Add(genres[i]);
                            }
                            for (int i = 0; i < instrumentList.Count(); i++)
                            {
                                instruments.Add(new Instrument
                                {
                                    InstrumentName = instrumentList[i],
                                });
                                db.Instruments.Add(instruments[i]);
                            }
                            for (int i = 0; i < bandmemberList.Count(); i++)
                            {
                                bandmembers.Add(new BandMember
                                {
                                    BandMemberName = bandmemberList[i],
                                    UserID = HarmonyUser.ID
                                });
                                bandmembers[i].Instruments.Add(instruments[i]);
                                db.BandMembers.Add(bandmembers[i]);

                                // db.BandMember_Instrument.Add(new BandMember_Instrument { BandMemberID = bandmembers[i].ID, InstrumentID = instruments[i].ID });     
                            }

                        }
                        await db.SaveChangesAsync();
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }
            /*StreamReader sr = new StreamReader(Server.MapPath("~/Content/states_hash.json"));
            string data = sr.ReadToEnd();
            JArray arr = JArray.Parse(data);
            List<SelectListItem> stateList = new List<SelectListItem>();
            for (int i = 0; i < arr.Count(); i++)
            {
                stateList.Add(new SelectListItem { Text = (string)arr[i]["name"], Value = (string)arr[i]["name"] });
            }
            // ViewBag.State = stateList;
            ViewData["State"] = stateList;*/
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId; 
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
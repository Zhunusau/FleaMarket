using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.ViewModels;
using Microsoft.Owin.Security;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserViewModelFactory userDtoModelFactory;
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(
            IAuthenticationService authenticationService, 
            IUserViewModelFactory userDtoModelFactory,
            IAuthenticationManager authenticationManager)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.userDtoModelFactory = userDtoModelFactory ?? throw new ArgumentNullException(nameof(userDtoModelFactory));
            this.authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var operationDetails = await authenticationService.CreateUser(userDtoModelFactory.CreateUserDto(signUpViewModel));
                if (operationDetails.Succeeded)
                {
                    var token = await authenticationService.GenerateEmailConfirmationTokenAsync(signUpViewModel.Email);
                    var callBack = Url.Action("ConfirmEmail", "Account", new {email = signUpViewModel.Email, code = token }, protocol: Request.Url.Scheme);
                    await authenticationService.SendMessageAsync(signUpViewModel.Email, callBack);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(signUpViewModel);
        }

        public async Task<ActionResult> ConfirmEmail(string email, string code)
        {
            var result = await authenticationService.ConfirmEmailAsync(email, code);
            if (code == null && !result.Succeeded)
            {
                return View("Error");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInViewModel signInViewModel)
        {
            var userDto = userDtoModelFactory.CreateUserDto(signInViewModel);

            if (ModelState.IsValid)
            {
                var claim = await authenticationService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid password or email.");
                }
                else
                {
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(signInViewModel);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            authenticationManager.SignOut();
            return RedirectToAction("SignIn");
        }
    }
}
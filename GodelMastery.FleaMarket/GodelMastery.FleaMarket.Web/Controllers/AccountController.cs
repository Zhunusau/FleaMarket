using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserDtoModelFactory userDtoModelFactory;
        
        public AccountController(IAuthenticationService authenticationService, IUserDtoModelFactory userDtoModelFactory)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.userDtoModelFactory = userDtoModelFactory ?? throw new ArgumentNullException(nameof(userDtoModelFactory));
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
                if (operationDetails.Succedeed)
                {
                    var code = await authenticationService.GenerateEmailConfirmationTokenAsync(signUpViewModel.Email);
                    var callBack = Url.Action("ConfirmEmail", "Account", new {email = signUpViewModel.Email, code = code }, protocol: Request.Url.Scheme);
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
            if (code == null && !result.Succedeed)
            {
                return View("Error");
            }
            return View();
        }
    }
}
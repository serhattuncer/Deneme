using Microsoft.AspNetCore.Mvc;

namespace Dastone.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login() {  return View(); }

        public new IActionResult NotFound() {  return View(); }

        public IActionResult InternalError() {  return View(); }

        public IActionResult LockScreen() {  return View(); }

        public IActionResult RecoverPassword() {  return View(); }

        public IActionResult Register() {  return View(); }
    }
}

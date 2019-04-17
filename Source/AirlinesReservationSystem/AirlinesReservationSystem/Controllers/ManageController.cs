using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.ViewModels;
using System.Collections.Generic;

namespace AirlinesReservationSystem.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket _booking = new Areas.Admin.ReponsitoryModel.ReponsitoryBookingTicket();
        private Areas.Admin.ReponsitoryModel.ReponsitoryDiscountDetail _discount = new Areas.Admin.ReponsitoryModel.ReponsitoryDiscountDetail();
        private Areas.Admin.ReponsitoryModel.ReponsitoryPrices _price = new 
            Areas.Admin.ReponsitoryModel.ReponsitoryPrices();
        private Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight _seatby = new 
            Areas.Admin.ReponsitoryModel.ReponsitorySeatDetailByFlight();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
                }
            
            ModelState.AddModelError("", "Failed to add phone number");
            return View(model);
            // Generate the token and send it
            //if (UserManager.SmsService != null)
            //{
            //    var message = new IdentityMessage
            //    {
            //        Destination = model.Number,
            //        Body = "Your security code is: " + code
            //    };
            //    await UserManager.SmsService.SendAsync(message);
            //}


        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        public ActionResult Payment()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var bookingPay = _booking.SelectAll().Where(x => x.UserId == userid)
                .Where(x => x.ReservationModId == 1).ToList();
           
            
            var Listpayment = new List<PaymentViewModel>();
            foreach (var item in bookingPay)
            {
                var discount = _discount.SelectById(item.DiscountId);
                var price = _price.SelectById(item.PriceId);
                var seat = _seatby.SelectById(item.SeatDetailByFlightId);
                PaymentViewModel ticket = new PaymentViewModel
                {
                    FlightID = item.FlightId,
                    Location_Depart = item.Flight.Router.AirportDepart.Location.City,
                    Location_Arrival = item.Flight.Router.AirportArrival.Location.City,
                    Dept_Time = item.Flight.Dept_Time,
                    Arr_Time = item.Flight.Arr_Time,
                    Price = price.Price1,
                    Discount = discount.Discount,
                    SeatDetailByFlightId = item.SeatDetailByFlightId,
                    SeatNumber = seat.SeatNumber.SeatNo,
                    Booking_TicketId = item.Booking_TicketId,
                    PassengerFirstName = item.PassengerFirstName,
                    PassengerLastName = item.PassengerLastName,
                    PassengerEmail = item.PassengerEmail,
                    PassengerNumberId = item.PassengerNumberId,
                    PassengerPhoneNumber = item.PassengerPhoneNumber
                };
                if (ticket.Discount == null)
                {
                    ticket.Total = ticket.Price;
                }
                else
                {
                    ticket.Total = ticket.Price - (ticket.Price * (int)ticket.Discount / 100);
                }
                
                Listpayment.Add(ticket);

            }
            TempData["listpayment"] = Listpayment.ToList();
            return View(Listpayment);
        }

        public ActionResult Cancel(int BookingId, int seatId)
        {
            Booking_Ticket booking = new Booking_Ticket();
            booking = _booking.SelectById(BookingId);
            booking.ReservationModId = 3;
            _booking.Update(booking);
            _booking.Save();
            SeatDetailByFlight seat = new SeatDetailByFlight();
            seat = _seatby.SelectById(seatId);
            seat.SeatStatus = true;
            _seatby.Update(seat);
            _seatby.Save();
            return RedirectToAction("Payment");
        }


        public ActionResult HistoryBooking()
        {
            string userid = HttpContext.User.Identity.GetUserId();
            var bookingPay = _booking.SelectAll().Where(x => x.UserId == userid)
                .Where(x => x.ReservationModId != 1).ToList();


            var Listpayment = new List<PaymentViewModel>();
            foreach (var item in bookingPay)
            {
                var discount = _discount.SelectById(item.DiscountId);
                var price = _price.SelectById(item.PriceId);
                var seat = _seatby.SelectById(item.SeatDetailByFlightId);
                PaymentViewModel ticket = new PaymentViewModel
                {
                    FlightID = item.FlightId,
                    Location_Depart = item.Flight.Router.AirportDepart.Location.City,
                    Location_Arrival = item.Flight.Router.AirportArrival.Location.City,
                    Dept_Time = item.Flight.Dept_Time,
                    Arr_Time = item.Flight.Arr_Time,
                    Price = price.Price1,
                    Discount = discount.Discount,
                    SeatDetailByFlightId = item.SeatDetailByFlightId,
                    SeatNumber = seat.SeatNumber.SeatNo,
                    Booking_TicketId = item.Booking_TicketId,
                    PassengerFirstName = item.PassengerFirstName,
                    PassengerLastName = item.PassengerLastName,
                    PassengerEmail = item.PassengerEmail,
                    PassengerNumberId = item.PassengerNumberId,
                    PassengerPhoneNumber = item.PassengerPhoneNumber
                };
                if (ticket.Discount == null)
                {
                    ticket.Total = ticket.Price;
                }
                else
                {
                    ticket.Total = ticket.Price - (ticket.Price * (int)ticket.Discount / 100);
                }

                Listpayment.Add(ticket);

            }

            return View(Listpayment);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}
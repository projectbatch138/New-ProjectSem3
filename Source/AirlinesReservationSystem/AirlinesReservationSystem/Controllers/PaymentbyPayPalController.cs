using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.ViewModels;
using PayPal.Api;
namespace AirlinesReservationSystem.Controllers
{
    public class PaymentbyPayPalController : Controller
    {
        private Payment payment;
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList()
            {
                items = new List<Item>()
            };
            List<PaymentViewModel> listpayment = (List<PaymentViewModel>)TempData["listpayment"];
            double total = 0;
            foreach (var ticket in listpayment)
            {
                listItems.items.Add(new Item()
                {
                    name = "Ticket ASP Airline",
                    currency = "USD",
                    price = (ticket.Price - (ticket.Price * ticket.Discount / 100)).ToString(),
                    quantity = "1",
                    sku = "sku"
                });
                total = total + (ticket.Price - (ticket.Price * (double)ticket.Discount / 100));
            }

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            //Do the configuration RedirectURLs here with RedirectURLs obj

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            //Create Deltail obj 
            var details = new Details()
            {
                tax = "1",
                shipping="0",
                subtotal = total.ToString(),
            };

            //Create amount obj 
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };

            //Create transaction 

            var transactionList = new List<Transaction>
            {
                new Transaction()
                {
                    description = "ASP Airline transaction description",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    item_list = listItems
                }
            };

            Payment payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            /*
            try
            {
                return payment.Create(apiContext);
            }
            catch (PayPal.PayPalException ex)
            {
                Response.Write("Error Paypal Exception: " + ex.Message);
            }
            */
            return payment.Create(apiContext);
        }

        //Create Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment()
            {
                id = paymentId
            };
            return payment.Execute(apiContext, paymentExecution);
        }

        //Create PaymentWithPaypal method 
        public ActionResult PaymentWithPaypal()
        {
            //gettings context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //Create a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PaymentbyPayPal/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirecUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirecUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirecUrl);
                }
                else
                {
                    // This one will be executed when we have received all payment params from previous call
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {

                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
            }

            return View("Succsess");
        }

    }
}
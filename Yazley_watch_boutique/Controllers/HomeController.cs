using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Net.Mail;
using PayPal.Api;

namespace Yazley_watch_boutique.Controllers
{
    public class HomeController : Controller
    {
        yazleyEntities db = new yazleyEntities();
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult TheWatchGuide()
        {
            yazleyEntities1 db = new yazleyEntities1();
            var list = db.thewatchguides.ToList();
            return View(list);
        }

        public ActionResult BoutiqueDisplay()
        {
            yazleyEntities1 db = new yazleyEntities1();
            var list = db.boutiques.ToList();
            return View(list);
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult BoutiqueDetails()
        {
            return View();
        }

        public ActionResult Search_display(string search)
        {
            
            return View(db.products.Where(x => x.productName == search || x.brand==search ||search==null).ToList());
        }

        public ActionResult Display(String gender)
        {
            using (yazleyEntities a = new yazleyEntities())
            {
                var data = a.products.Include(c=>c.brand1).Include(c=>c.calibre).Where(g => g.gender.Equals(gender)).ToList();
                return View(data);
            }
        }

        public ActionResult Acces_Display(String category)
        {
            using (yazleyEntities a = new yazleyEntities())
            {
                var data = a.products.Include(c => c.brand1).Include(c => c.calibre).Where(c => c.category.Equals(category)).ToList();
                return View(data);
            }
        }

        public ActionResult PlaceOrder(List<int> cartProduct)
        { 
           return View();
        }
        [HttpGet]
        public ActionResult Inquiry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Inquiry(Inquiry inq,int id)
        {
            if (ModelState.IsValid)
            {
               
                Inquiry inquiry = db.Inquiries.Create();
                inquiry.Product = id;
                inquiry.Email = inq.Email;
                inquiry.City = inq.City;
                inquiry.Phone = inq.Phone;
                db.Entry(inquiry).State = EntityState.Modified;
                db.Inquiries.Add(inquiry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult OrderList(int? userid)
        {
            var list = db.Orders.Where(a => a.Userid==userid).ToList();
            return View(list);
        }

        public async Task<ActionResult> Cart(int id,bool IsDelete=false)
        {
            List<int> cartProduct = new List<int>();
            
        
            if (Session["CartItems"] != null)
            {
                cartProduct = (List<int>)Session["CartItems"];

            }
            if (IsDelete)
            {
                cartProduct = cartProduct.Where(c => c != id).ToList();
                Cart product = db.Carts.Where(a => a.productId == id).FirstOrDefault();
                db.Carts.Remove(product);
            }
            else
            {
                if (cartProduct != null && cartProduct.Any(c => c == id))
                    ViewBag.Message = "Product is already exist!!";

                else
                {
                    cartProduct.Add(id);
                    Cart cart = db.Carts.Create();
                    cart.Username = Session["Username"].ToString();
                    cart.productId = id;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }
            }
            Session["CartItems"] = cartProduct;
            List<product> productList = new List<product>();
            foreach (var PId in cartProduct)
            {
                productList.Add(await db.products.FindAsync(PId));
            }
            return View(productList);
            //return Json(new { status = true, message = "Product added successfully" });
        }

        public ActionResult brand_Display(String brand)
        {
            using (yazleyEntities a = new yazleyEntities())
            {
                var data = a.products.Include(c => c.brand1).Include(c => c.calibre).Where(g => g.brand.Equals(brand)).ToList();
                return View(data);
            }
        }

        public ActionResult material_display(String material)
        {
            using (yazleyEntities a = new yazleyEntities())
            {
                var data = a.products.Include(c => c.brand1).Include(c => c.calibre).Where(g => g.strap_material.Equals(material) || g.glass_material.Equals(material) || g.case_material.Equals(material)).ToList();
                return View(data);
            }
        }

        public ActionResult price_Display(int price1,int price2)
        {
            using (yazleyEntities a = new yazleyEntities())
            {
                var data = a.products.Include(c => c.brand1).Include(c => c.calibre).Where(p => p.price > price1 && p.price < price2).ToList();
                return View(data);
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (yazleyEntities db = new yazleyEntities())
                {
                    var obj = db.Users.Where(a => a.userName.Equals(objUser.userName) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        if (obj.userName == "admin")
                        {
                            return RedirectToAction("Index","product");
                        }
                        else
                        {
                            Session["UserID"] = obj.userId;
                            Session["UserName"] = obj.userName.ToString();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult PayMethodForm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PayMethodForm(User user,string payby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (payby == "cod")
                {
                    return RedirectToAction("ConfirmOrder");
                }
                else
                {
                    return RedirectToAction("PaymentWithPaypal");
                }
            }
            return View(user);
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }
        
        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("ListFeedback");
            }
            return View(feedback);
        }

        public ActionResult ListFeedback()
        {
            var listofdata = db.Feedbacks.ToList();
            return View(listofdata);
        }

        public ActionResult TWG()
        {
            var listofdata = db.thewatchguides.ToList();
            return View(listofdata);
        }

        public ActionResult DetailsGuide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thewatchguide guide = db.thewatchguides.Find(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }


        public ActionResult addUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return View();
        }

        public async Task<ActionResult> Acce_Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }                                                                                                       

        public ActionResult SendMail(string receiver)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("029yasikagohil@gmail.com", "Yasika");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Y3717108";
                    var subject = "Thank you";
                    var body = "";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl=true,
                        DeliveryMethod=SmtpDeliveryMethod.Network,
                        UseDefaultCredentials=false,
                        Credentials=new NetworkCredential(senderEmail.Address,password)
                    };
                    using(var mess=new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject=subject,
                        Body=body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }catch(Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "INR",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = "/Home/ConfirmOrder"
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "INR",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}
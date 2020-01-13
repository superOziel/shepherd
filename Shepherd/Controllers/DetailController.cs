using Shepherd.Models;
using Shepherd.Services;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Controllers
{
    public class DetailController : Controller
    {
        DetailService detailService = new DetailService();
        CommitmentService commitmentService = new CommitmentService();
        RPSAService rpsaService = new RPSAService();
        //
        // GET: /Detail/

        public ActionResult Index(Report item)
        {
            ViewBag.OrderType = item.OrderType;
            ViewBag.BaanCompany = item.BaanCompany;
            ViewBag.ProdOrderNr = item.ProdOrderNr;
            ViewBag.Assembly = item.MaxDelivDate;
            ViewBag.Projplan = item.projplan;
            ViewBag.Location = item.Location;
            ViewBag.MaxDelivDateStr = item.MaxDelivDateStr;
            ViewBag.User = item.username;

            return View();
        }

        public void sendMail(string project, string user) 
        {
            if (user == "oiguzman" || string.IsNullOrEmpty(user))
                return;
            
            user = getUser(user);
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient client = new SmtpClient();

            mail.To.Add(new MailAddress("ecantu@ruhrpumpen.com"));
            mail.To.Add(new MailAddress("oiguzman@ruhrpumpen.com"));

            mail.From = new MailAddress("wfservice@ruhrpumpen.com");
            mail.Subject = "Shepherd Notification: " + project + ", " + user;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            string htmlString = "<br /> User <b>" + user + " </b>has seen the details of the project <b>" + project + "</b>";
            mail.IsBodyHtml = true;
            mail.Body = htmlString;

            mail.Priority = MailPriority.Normal;

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "tulex10.ruhrpumpen.com";

            try
            {
                client.Send(mail);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //
        // GET: /Detail/DetailInformation
        public ActionResult DetailInformation(Report item)
        {
            return Json( detailService.detailInformation(item), JsonRequestBehavior.AllowGet);
        }   

        //
        // GET: /Detail/GetTask/
        public ActionResult GetTasks(int projplan) 
        {
            return Json(detailService.getTasks(projplan), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTasksCategories() {
            return Json(detailService.getTasksCat(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCommitment(Report item)
        {
            return Json(commitmentService.getCommitment(item), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Detail/GetConstraint
        public ActionResult GetConstraint(string order, string location)
        {
            try
            {
                var JsonResult = Json(rpsaService.getConstraint(order, location), JsonRequestBehavior.AllowGet);
                JsonResult.MaxJsonLength = int.MaxValue;
                return JsonResult;
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
            
        }

        public ActionResult GetBuyConstraint(string order, string line, string location) 
        {
            var JsonResult = Json(detailService.getBuyConstraint(order, line, location), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        //
        // GET: /Detail/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Detail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Detail/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Detail/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Detail/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Detail/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Detail/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public string DecodeUser(string user_encode)
        {
            try
            {
                Byte[] b = Convert.FromBase64String(user_encode);
                string decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
                return decrypted;
            }
            catch
            {
                return null;
            }

        }

        public string getUser(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return "";

                string user = string.Empty;
                using (DirectoryEntry _entry = new DirectoryEntry("LDAP://TULAD3", "wfservice", "wfserviceRP"))// createDirectoryEntry())
                {
                    _entry.Username = "wfservice";
                    _entry.Password = "wfserviceRP";

                    DirectorySearcher _searcher = new DirectorySearcher(_entry);
                    //_searcher.SearchScope = SearchScope.OneLevel;

                    _searcher.Filter = "(sAMAccountName=" + username + ")";

                    SearchResult _sr = _searcher.FindOne();
                    user = _sr.Properties["displayname"][0].ToString();

                }

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Active Directory Connection
        static DirectoryEntry createDirectoryEntry()
        {
            try
            {
                DirectoryEntry ldapConnection = new DirectoryEntry();
                ldapConnection.Path = "LDAP://tulAD3.ruhrpumpen.com/DC=ruhrpumpen, DC=com";
                //ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

                return ldapConnection;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Website.Models;

namespace Website.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private CloudBlobContainer blobContainer =
            CloudStorageAccount.Parse(
                "DefaultEndpointsProtocol=https;AccountName=tonii;AccountKey=rNvDjcL2wghkG0lcfhaoV/RMhDd0B+kZPPqIbq2/4HH5WcMLE+n/J0CTdVbrUwduHeverspAjB1CVlaTyCqpnA==")
                .CreateCloudBlobClient().GetContainerReference("pictures");

        // GET: Comments
        public async Task<ActionResult> Index(int sessionId)
        {
            return View(await db.Comments.Where(w => w.SessionId == sessionId).ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create(int sessionId)
        {
            return View(new Comment { SessionId = sessionId });
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SessionId,Note,CommentText")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var picture = Request.Files["Picture"];
                    if (picture != null)
                    {
                        var blob =
                            blobContainer.GetBlockBlobReference(string.Format("Session-{0}/{1}", comment.SessionId,
                                picture.FileName));
                        await blob.UploadFromStreamAsync(picture.InputStream);
                        comment.ImageUrl = blob.Uri.ToString();
                    }
                }
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "BootCampSessions", new { id = comment.SessionId });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SessionId,Note,CommentText,ImageUrl")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

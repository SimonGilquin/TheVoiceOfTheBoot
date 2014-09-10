using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class BootCampSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BootCampSessions
        public async Task<ActionResult> Index()
        {
            return View(await db.Sessions.Select(s=>new SessionSummary
            {
                BootCampSession = s,
                CommentCount = s.Comments.Count,
                AverageSpeakerNote = s.Comments.Average(c => c.SpeakerNote),
                AverageContentNote = s.Comments.Average(c => c.SessionContentNote),
                AverageSupportNote = s.Comments.Average(c => c.SupportNote),
            }).ToListAsync());
        }

        // GET: BootCampSessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootCampSession bootCampSession = await db.Sessions.Include(s => s.Comments).SingleOrDefaultAsync(s => s.Id == id);
            if (bootCampSession == null)
            {
                return HttpNotFound();
            }
            return View(bootCampSession);
        }

        // GET: BootCampSessions/Create
        public async Task<ActionResult> Create(int? bootcampId = null)
        {
            if (bootcampId == null)
            {
                /* Get current Bootcamp */
                var lastBootcamp =
                    await db.Bootcamps.Where(b => b.Current).OrderByDescending(b => b.Date).FirstOrDefaultAsync();
                if (lastBootcamp == null)
                {
                    lastBootcamp = new Bootcamp { Current = true, Date = DateTime.Now.Date, Title = string.Format("{0}'s Bootcamp", DateTime.Now.ToString("YYYY MMMM DD")) };
                    db.Bootcamps.Add(lastBootcamp);
                    await db.SaveChangesAsync();
                }
                bootcampId = lastBootcamp.Id;
            }

            return View(new BootCampSession { BootcampId = bootcampId.Value });
        }


        // POST: BootCampSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BootcampId,StartTime,Title,Speakers,Description")] BootCampSession bootCampSession)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(bootCampSession);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bootCampSession);
        }

        // GET: BootCampSessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootCampSession bootCampSession = await db.Sessions.FindAsync(id);
            if (bootCampSession == null)
            {
                return HttpNotFound();
            }
            return View(bootCampSession);
        }

        // POST: BootCampSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BootcampId,StartTime,Title,Speakers,Description")] BootCampSession bootCampSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bootCampSession).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bootCampSession);
        }

        // GET: BootCampSessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootCampSession bootCampSession = await db.Sessions.FindAsync(id);
            if (bootCampSession == null)
            {
                return HttpNotFound();
            }
            return View(bootCampSession);
        }

        // POST: BootCampSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BootCampSession bootCampSession = await db.Sessions.FindAsync(id);
            db.Sessions.Remove(bootCampSession);
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

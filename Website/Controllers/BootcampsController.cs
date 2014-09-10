using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class BootcampsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var bootcamps = await db.Bootcamps.ToListAsync();
            return View(bootcamps);
        }

        [HttpPost]
        public async Task<ActionResult> Close(int id)
        {
            var bootcamp = await db.Bootcamps.FindAsync(id);
            if (bootcamp == null) return HttpNotFound();
            bootcamp.Current = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Create(Bootcamp bootcamp)
        {
            db.Bootcamps.Add(bootcamp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "BootCampSessions");
        }
    }
}
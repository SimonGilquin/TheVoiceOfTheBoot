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
    }
}
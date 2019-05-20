using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// GET: /Home/
		public async Task<ActionResult> Index()
        {
	        try
	        {
		        Log.Info("Loading home page");

		        return View(await _storeContext.Albums
			        .OrderByDescending(a => a.OrderDetails.Count())
			        .Take(6)
			        .ToListAsync());
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
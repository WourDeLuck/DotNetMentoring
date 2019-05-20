using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// GET: /Store/
		public async Task<ActionResult> Index()
        {
	        try
	        {
		        Log.Info("Getting list of items available");
		        return View(await _storeContext.Genres.ToListAsync());
	        }
	        catch (Exception e)
	        {
		        Log.Error(e.Message);
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
	        }
        }

        // GET: /Store/Browse?genre=Disco
        public async Task<ActionResult> Browse(string genre)
        {
	        try
	        {
		        Log.Info("Getting item info");
		        return View(await _storeContext.Genres.Include("Albums").SingleAsync(g => g.Name == genre));
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        public async Task<ActionResult> Details(int id)
        {
	        try
	        {
				Log.Info($"Getting item details: {id}");
		        var album = await _storeContext.Albums.FindAsync(id);

		        return album != null ? View(album) : (ActionResult)HttpNotFound();
	        }
	        catch (Exception e)
	        {
		        Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
	        try
	        {
		        return PartialView(
			        _storeContext.Genres.OrderByDescending(
				        g => g.Albums.Sum(a => a.OrderDetails.Sum(od => od.Quantity))).Take(9).ToList());
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
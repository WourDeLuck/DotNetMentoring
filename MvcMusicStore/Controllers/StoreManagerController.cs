using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// GET: /StoreManager/
		public async Task<ActionResult> Index()
        {
	        try
	        {
		        return View(await _storeContext.Albums
			        .Include(a => a.Genre)
			        .Include(a => a.Artist)
			        .OrderBy(a => a.Price).ToListAsync());
	        }
	        catch (Exception e)
	        {
		        Log.Error(e.Message);
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
	        }
        }

        // GET: /StoreManager/Details/5
        public async Task<ActionResult> Details(int id = 0)
        {
	        try
	        {
		        var album = await _storeContext.Albums.FindAsync(id);
            
		        if (album == null)
		        {
			        return HttpNotFound();
		        }

		        return View(album);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // GET: /StoreManager/Create
        public async Task<ActionResult> Create()
        {
	        try
	        {
		        return await BuildView(null);
	        }
	        catch (Exception e)
	        {
		        Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // POST: /StoreManager/Create
        [HttpPost]
        public async Task<ActionResult> Create(Album album)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        _storeContext.Albums.Add(album);
                
			        await _storeContext.SaveChangesAsync();
                
			        return RedirectToAction("Index");
		        }

		        return await BuildView(album);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // GET: /StoreManager/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
	        try
	        {
		        var album = await _storeContext.Albums.FindAsync(id);
		        if (album == null)
		        {
			        return HttpNotFound();
		        }

		        return await BuildView(album);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // POST: /StoreManager/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Album album)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        _storeContext.Entry(album).State = EntityState.Modified;

			        await _storeContext.SaveChangesAsync();
                
			        return RedirectToAction("Index");
		        }

		        return await BuildView(album);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // GET: /StoreManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
	        try
	        {
		        var album = await _storeContext.Albums.FindAsync(id);
		        if (album == null)
		        {
			        return HttpNotFound();
		        }

		        return View(album);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
	        try
	        {
		        var album = await _storeContext.Albums.FindAsync(id);
		        if (album == null)
		        {
			        return HttpNotFound();
		        }

		        _storeContext.Albums.Remove(album);

		        await _storeContext.SaveChangesAsync();

		        return RedirectToAction("Index");
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        private async Task<ActionResult> BuildView(Album album)
        {
	        try
	        {
		        ViewBag.GenreId = new SelectList(
			        await _storeContext.Genres.ToListAsync(),
			        "GenreId",
			        "Name",
			        album == null ? null : (object)album.GenreId);

		        ViewBag.ArtistId = new SelectList(
			        await _storeContext.Artists.ToListAsync(),
			        "ArtistId",
			        "Name",
			        album == null ? null : (object)album.ArtistId);

		        return View(album);
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
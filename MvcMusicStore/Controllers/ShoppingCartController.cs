using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// GET: /ShoppingCart/
		public async Task<ActionResult> Index()
        {
	        try
	        {
		        var cart = ShoppingCart.GetCart(_storeContext, this);

		        var viewModel = new ShoppingCartViewModel
		        {
			        CartItems = await cart.GetCartItems().ToListAsync(),
			        CartTotal = await cart.GetTotal()
		        };

		        return View(viewModel);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // GET: /ShoppingCart/AddToCart/5
        public async Task<ActionResult> AddToCart(int id)
        {
	        try
	        {
		        var cart = ShoppingCart.GetCart(_storeContext, this);

		        await cart.AddToCart(await _storeContext.Albums.SingleAsync(a => a.AlbumId == id));

		        await _storeContext.SaveChangesAsync();

		        return RedirectToAction("Index");
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
	        try
	        {
		        var cart = ShoppingCart.GetCart(_storeContext, this);

		        var albumName = await _storeContext.Carts
			        .Where(i => i.RecordId == id)
			        .Select(i => i.Album.Title)
			        .SingleOrDefaultAsync();

		        var itemCount = await cart.RemoveFromCart(id);

		        await _storeContext.SaveChangesAsync();

		        var removed = (itemCount > 0) ? " 1 copy of " : string.Empty;

		        var results = new ShoppingCartRemoveViewModel
		        {
			        Message = removed + albumName + " has been removed from your shopping cart.",
			        CartTotal = await cart.GetTotal(),
			        CartCount = await cart.GetCount(),
			        ItemCount = itemCount,
			        DeleteId = id
		        };

				Log.Info($"Object removed from the cart: {id}");

		        return Json(results);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
	        try
	        {
		        var cart = ShoppingCart.GetCart(_storeContext, this);

		        var cartItems = cart.GetCartItems()
			        .Select(a => a.Album.Title)
			        .OrderBy(x => x)
			        .ToList();

		        ViewBag.CartCount = cartItems.Count();
		        ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

		        return PartialView("CartSummary");
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

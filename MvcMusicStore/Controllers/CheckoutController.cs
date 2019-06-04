using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private const string PromoCode = "FREE";

        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	    private PerformanceCounter _avgDuration;
	    private PerformanceCounter _avgDurationBase;

		// GET: /Checkout/
		public ActionResult AddressAndPayment()
        {
            return View();
        }

	    public CheckoutController()
	    {
			_avgDuration = new PerformanceCounter
			{
				CategoryName = "CustomCounters",
				CounterName = "average time per operation",
				MachineName = ".",
				ReadOnly = false
			};

		    _avgDurationBase = new PerformanceCounter
		    {
			    CategoryName = "CustomCounters",
			    CounterName = "average time per operation base",
			    MachineName = ".",
			    ReadOnly = false
		    };
		}

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
	        try
	        {
				var stopwatch = new Stopwatch();
				stopwatch.Start();

		        var order = new Order();
		        TryUpdateModel(order);

		        if (ModelState.IsValid 
		            && string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase))
		        {
			        order.Username = User.Identity.Name;
			        order.OrderDate = DateTime.Now;

			        _storeContext.Orders.Add(order);

			        await ShoppingCart.GetCart(_storeContext, this).CreateOrder(order);

			        await _storeContext.SaveChangesAsync();

			        return RedirectToAction("Complete", new { id = order.OrderId });
		        }

				stopwatch.Stop();

		        _avgDuration.IncrementBy(stopwatch.ElapsedTicks);
		        _avgDurationBase.Increment();
		        return View(order);
	        }
	        catch (Exception e)
	        {
				Log.Error(e.Message);
		        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
        }

        // GET: /Checkout/Complete
        public async Task<ActionResult> Complete(int id)
        {
	        try
	        {
		        return await _storeContext.Orders.AnyAsync(o => o.OrderId == id && o.Username == User.Identity.Name)
			        ? View(id)
			        : View("Error");
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
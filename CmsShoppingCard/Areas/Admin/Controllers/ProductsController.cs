using CmsShoppingCard.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CmsShoppingCard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        CmsShoppingCardContext context;

        public ProductsController(CmsShoppingCardContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.Products.OrderByDescending(x => x.Id).Include(x => x.Category).ToListAsync());
        }

        //Get /Admin/Product/Create
        public IActionResult Create() {
            ViewBag.CategoryId = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name");
            return View();
        }
    }
}

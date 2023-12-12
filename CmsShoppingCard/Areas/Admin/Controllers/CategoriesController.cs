using CmsShoppingCard.Infrastructure;
using CmsShoppingCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CmsShoppingCardContext context;
        public CategoriesController(CmsShoppingCardContext context) {
            this.context = context;
        }

        //Get Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await context.Categories.OrderBy(x => x.Sorting).ToListAsync());
        }

        //Get Admin/Categories/Create
        public IActionResult Create() => View();

        //Post Admin Categoris/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = await context.Categories.FirstOrDefaultAsync(sl => category.Slug == sl.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("Name", "The Name Already Exist");
                    return View(category);
                }
                category.Sorting = 100;
                context.Add(category);
                await context.SaveChangesAsync();
                TempData["Success"] = "The Category has been Added!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Get Admin/Categories/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Get Admin/Categories/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                var slug = await context.Categories.Where(cat => cat.Id != category.Id).FirstOrDefaultAsync(sl => category.Slug == sl.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("Name", "The Name Already Exist");
                    return View(category);
                }
                category.Sorting = 100;
                context.Update(category);
                await context.SaveChangesAsync();
                TempData["Success"] = "The Category has been Updated!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET /Pages/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                TempData["error"] = "Page Does not Found";
            }
            else
            {
                TempData["Success"] = "Category Deleted Succussfuly!";
                context.Remove(category);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        //Post /Pages/Reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {

            int count = 0;
            foreach (int categoryId in id)
            {
                Category category = await context.Categories.FindAsync(categoryId);
                category.Sorting = count++;
                context.Update(category);
                await context.SaveChangesAsync();
            }
            return Ok();
        }

    }
}

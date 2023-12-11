using CmsShoppingCard.Infrastructure;
using CmsShoppingCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCard.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly CmsShoppingCardContext context;
        public PagesController(CmsShoppingCardContext context)
        {
            this.context = context;
        }
        //Get Admin/Pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }
        
        //Get Admin/Pages/Details/id
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null) 
            {
                return NotFound();
            }
            return View(page);
        }

        //Get Admin/Pages/Create
        public IActionResult Create() => View();

        //Post Admin/Pages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if(ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                var slug = await context.Pages.FirstOrDefaultAsync(sl => page.Slug == sl.Slug);
                if(slug != null) {
                    ModelState.AddModelError("Title", "The Title Already Exist");
                    return View(page);
                }
                page.Sorting = 100;
                context.Add(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The Page has been Added!";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        //Get Admin/Pages/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        //Post Admin/Pages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");
                var slug = await context.Pages.Where(x => x.Id != 1 && x.Id != page.Id).FirstOrDefaultAsync(sl => page.Slug == sl.Slug);
                
                if (slug != null)
                {
                    ModelState.AddModelError("Title", "The Title Already Exist");
                    return View(page);
                }
                page.Sorting = 100;
                context.Update(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The Page has been Updated!";
                return RedirectToAction("Index");
            }
            return View(page);
        }
        // GET /Pages/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                TempData["error"] = "Page Does not Found";
            } else
            {
                TempData["Success"] = "Page Deleted Succussfuly!";
                context.Remove(page);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}


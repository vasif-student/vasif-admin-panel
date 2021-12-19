using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_proj.DAL;
using MVC_proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_proj.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LayoutController : Controller
    {

        private readonly AppDbContext _context;

        public LayoutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var layouts = await _context.Layouts.ToListAsync();
            return View(layouts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var layout = await _context.Layouts.FindAsync(id);
            if(layout == null)
            {
                return NotFound();
            }

            return View(layout);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Layout layout)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            await _context.Layouts.AddAsync(layout);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

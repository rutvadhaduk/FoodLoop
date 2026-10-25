
using FoodLoop.Data;
using FoodLoop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodLoop.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Food list, search, filter and statistics
        public async Task<IActionResult> Index(
            string? searchString,
            string? statusFilter)
        {
            var foods = _context.Foods.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                foods = foods.Where(f =>
                    f.FoodName.Contains(searchString) ||
                    f.Location.Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                foods = foods.Where(f => f.Status == statusFilter);
            }

            ViewBag.TotalDonations = await _context.Foods.CountAsync();

            ViewBag.AvailableDonations = await _context.Foods
                .CountAsync(f => f.Status == "Available");

            ViewBag.TotalMeals = await _context.Foods
                .Where(f => f.Status == "Available")
                .SumAsync(f => (int?)f.Quantity) ?? 0;

            ViewBag.SearchString = searchString;
            ViewBag.StatusFilter = statusFilter;

            var result = await foods
                .OrderByDescending(f => f.Id)
                .ToListAsync();

            return View(result);
        }

        // Create food - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create food - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Foods.Add(food);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(food);
        }
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
        return NotFound();

    var food = await _context.Foods
        .FirstOrDefaultAsync(f => f.Id == id);

    if (food == null)
        return NotFound();

    return View(food);
}
        // Edit food - GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var food = await _context.Foods.FindAsync(id);

            if (food == null)
                return NotFound();

            return View(food);
        }

        // Edit food - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Food food)
        {
            if (id != food.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Foods.Update(food);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(food);
        }

        // Delete confirmation - GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Id == id);

            if (food == null)
                return NotFound();

            return View(food);
        }
        
        // Claim Food
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Claim(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
                return NotFound();

            if (food.Status != "Available")
            {
                TempData["Message"] = "Aa food already claimed chhe!";
                return RedirectToAction(nameof(Index));
            }

            food.Status = "Claimed";

            await _context.SaveChangesAsync();

            TempData["Message"] = "Food successfully claimed!";

            return RedirectToAction(nameof(Index));
        }


        // Delete food - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

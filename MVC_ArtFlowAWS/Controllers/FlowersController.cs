using Microsoft.AspNetCore.Mvc;
using MVC_ArtFlowAWS.Models;
using MVC_ArtFlowAWS.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MVC_ArtFlowAWS.Controllers
{
    public class FlowersController : Controller
    {
        private readonly MVC_ArtFlowAWSContext _context;

        public FlowersController(MVC_ArtFlowAWSContext context)
        {
            _context = context;
        }

        // GET: Flowers
        public async Task<IActionResult> Index()
        {
            var flowers = await _context.ArtTable.ToListAsync();
            return View(flowers);
        }

        // GET: Flowers/AddData
        public IActionResult AddData()
        {
            return View();
        }

        // POST: Flowers/AddData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddData(Flower flower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flower);
        }

        // GET: Flowers/EditData?FlowerId=1
        public async Task<IActionResult> EditData(int? FlowerId)
        {
            if (FlowerId == null)
            {
                return NotFound();
            }

            var flower = await _context.ArtTable.FindAsync(FlowerId.Value);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/UpdateData
        // The edit form posts the fields that bind to the Flower model.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateData(Flower flower)
        {
            if (!ModelState.IsValid)
                return View("EditData", flower);

            try
            {
                _context.Update(flower);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.ArtTable.AnyAsync(e => e.ArtID == flower.ArtID))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // Optional: DELETE action if you plan to use POST delete from the Index view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteData(int FlowerId)
        {
            var flower = await _context.ArtTable.FindAsync(FlowerId);
            if (flower != null)
            {
                _context.ArtTable.Remove(flower);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

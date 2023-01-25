using eBusiness.DAL;
using eBusiness.Models;
using eBusiness.ViewModels.Position;
using Microsoft.AspNetCore.Mvc;

namespace eBusiness.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        AppDbContext _context { get; }
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Positions.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View();
            Position position = new Position
            {
                Name = positionVM.Name
            };
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } 
        public IActionResult Update(int?id)
        {
            if(id is null || id==0) return NotFound();
            var position = _context.Positions.Find(id);
            if(position == null) return NotFound();
            return View(position);
        }
        [HttpPost]
        public IActionResult Update(int id,Position position) 
        {
            Position exist= _context.Positions.Find(id);
            exist.Name=position.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id==0) return NotFound();
            Position position=_context.Positions.Find(id);
            if(position == null) return BadRequest();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

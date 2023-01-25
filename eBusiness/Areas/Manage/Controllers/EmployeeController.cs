using eBusiness.DAL;
using eBusiness.Models;
using eBusiness.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eBusiness.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        AppDbContext _context { get; }
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Employees.Include(e=>e.Position).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Position=new SelectList(_context.Positions,nameof(Position.Id),nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = employeeVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "sekıl yukleyın zehmet olmasa");
                return View();
            }
            if (file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image", "sekılın olcusu 200 kbdan artıq olmaz");
                return View();
            }
            string fileName = Guid.NewGuid() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\ca.r215.16\\Desktop\\eBusiness\\eBusiness\\wwwroot\\assets\\img\\" + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Employee employee = new Employee { FullName = employeeVM.FullName, ImageUrl = fileName, Salary=employeeVM.Salary, 
              FacebookLink=employeeVM.FacebookLink, InstagramUrl=employeeVM.InstagramUrl,TwitterLink=employeeVM.TwitterLink,PositionId=employeeVM.PositionId };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
           if(id is null || id==0) return NotFound();
           Employee employee=_context.Employees.Find(id);
           if (employee is null) return BadRequest();
           return View(employee);
        }
        [HttpPost]
        public IActionResult Update(int? id, Employee employee)
        { 
            Employee exist=_context.Employees.Find(id);
            exist.FullName=employee.FullName;
            exist.Salary=employee.Salary;
            exist.FacebookLink=employee.FacebookLink;
            exist.TwitterLink=employee.TwitterLink;
            exist.InstagramUrl=employee.InstagramUrl;
            exist.ImageUrl=employee.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            Employee employee = _context.Employees.Find(id);
            if (employee is null) return NotFound();
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

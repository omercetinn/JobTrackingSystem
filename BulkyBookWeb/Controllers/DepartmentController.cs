using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Department> Department = _db.Departments;
            return View(Department);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult Create(Department Department)
        {
           
                _db.Departments.Add(Department);
                _db.SaveChanges();
                return RedirectToAction("Index");
            

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var DepartmentFormFb = _db.Departments.Find(id);
            //var DepartmentFormFbFirst=_db.Departments.FirstOrDefault(u=>u.Id==id);

            if (DepartmentFormFb == null)
            {
                return NotFound();
            }

            return View(DepartmentFormFb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department obj)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var DepartmentFormFb = _db.Departments.Find(id);
            //var DepartmentFormFbFirst=_db.Departments.FirstOrDefault(u=>u.Id==id);

            if (DepartmentFormFb == null)
            {
                return NotFound();
            }

            return View(DepartmentFormFb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Departments.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Departments.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

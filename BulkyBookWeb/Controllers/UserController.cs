using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            var user = _db.Users
                         .Include(cat => cat.Departments);
            return View(user.ToList());

            //IEnumerable<User> objUser =  _db.Users;
            //return View(objUser);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> degerler = (from i in _db.Departments.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Name,
                                                 Value = i.Id.ToString()
                                             }).ToList();
            ViewBag.Degerler = degerler;    
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var sr=_db.Departments.Where(i => i.Id == user.Departments.Id).FirstOrDefault();
            user.Departments= sr;
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFormFb = _db.Users.Find(id);
            //var userFormFbFirst=_db.Users.FirstOrDefault(u=>u.Id==id);

            List<SelectListItem> sr = (from i in _db.Departments.ToList()
                                       select new SelectListItem
                                       {
                                           Text = i.Name,
                                           Value = i.Id.ToString()
                                       }).ToList();
            ViewBag.Degerler = sr;
         

            if (userFormFb == null)
            {
                return NotFound();
            }

            return View(userFormFb);
        }

        //Post
        [HttpPost]
        public IActionResult Edit(User obj)
        {                       
            var sr = _db.Departments.Where(i=>i.Id == obj.Departments.Id).FirstOrDefault();
            obj.Departments = sr;
            _db.Users.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFormFb = _db.Users.Find(id);
            //var userFormFbFirst=_db.Users.FirstOrDefault(u=>u.Id==id);           

            if (userFormFb == null)
            {
                return NotFound();
            }

            return View(userFormFb);
        }

        //Post
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

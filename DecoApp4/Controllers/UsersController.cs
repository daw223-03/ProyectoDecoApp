using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecoApp4.Models;
using System.Runtime.Intrinsics.X86;

namespace DecoApp4.Controllers
{
    public class UsersController : Controller
    {
        private readonly DecoappContext _context;

        public UsersController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
              return _context.Users != null ? 
                          View( _context.Users.ToList()) :
                          Problem("Entity set 'DecoappContext.Users'  is null.");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom, string us)
        {
            var facturas = from m in _context.Users select m;
            if (nom != null)
            {
                facturas = facturas.Where(f => f.Nombre == nom);

            }
            if (us != null)
            {
                facturas = facturas.Where(f => f.UserName == us);

            }

            return View(facturas);


        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            var user =  _context.Users
                .FirstOrDefault(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Nombre, string UserName, string Password)
        {
            User us = new User();
            if (Nombre != null) { us.Nombre = Nombre; }
            if (UserName != null) { us.UserName = UserName; }
            if (Password != null) { us.Password = Password; }
            _context.Add(us);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,UserName,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        // POST: Users/Delete/5

        public IActionResult DeleteConfirmed(int id)
        {
            var user =  _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

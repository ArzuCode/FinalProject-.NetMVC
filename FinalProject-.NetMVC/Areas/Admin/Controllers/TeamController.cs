using Rent_a_Car_.Net.DAL;
using Rent_a_Car_.Net.Models;
using Rent_a_Car_.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_a_Car_.Net.Areas.Admin.Controllers
{
    public class TeamController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Teams.ToListAsync();

            return View(members);
        }

        public IActionResult Create()
        {
            Team member = new Team();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team member)
        {
            if (!ModelState.IsValid) return View();

            Team newMember = new Team
            {
                Name = member.Name,
                Surname = member.Surname,
                Position = member.Position,
                CreatedAt = DateTime.Now,
                ImageUrl = member.Image.SaveImage(_env, @"assets\images\member"),
            };

            await _context.Teams.AddAsync(newMember);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Team dbMember = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

            if (dbMember == null) return NotFound();

            return View(dbMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Team member)
        {
            Team dbMember = await _context.Teams.FirstOrDefaultAsync(m => m.Id == member.Id);
            if (dbMember == null) return NotFound();

            if (ModelState["Image"] != null)
            {
                if (!member.Image.IsImage())
                {
                    ModelState.AddModelError("Image", "Image Format Is Wrong");
                    return View();
                }
                if (member.Image.ValidSize(8000))
                {
                    ModelState.AddModelError("Image", "Image Is Oversize");
                    return View();
                }


                string path = Path.Combine(_env.WebRootPath, @"assets\images\member", dbMember.ImageUrl);
                ImageService.DeleteImage(path);
                dbMember.ImageUrl = member.Image.SaveImage(_env, @"assets\images\member");
            }


            var existName = _context.Teams.FirstOrDefault(x => x.Name.ToLower() == member.Name.ToLower());

            if (existName != null)
            {
                if (dbMember.Name.ToLower() != existName.Name.ToLower())
                {
                    ModelState.AddModelError("Name", "Bu brend artıq mövcuddur");
                    return View("Update");
                }
            }

            dbMember.Name = member.Name;
            dbMember.Surname = member.Surname;
            dbMember.Position = member.Position;
            dbMember.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null) return NotFound();

            Team dbMember = await _context.Teams.FindAsync(id);

            if (dbMember == null) return NotFound();

            dbMember.isDeleted = true;
            dbMember.DeletedAt = DateTime.Now;
            dbMember.CreatedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction("index");

        }

        public async Task<IActionResult> RemovedMembers()
        {
            List<Team> members = await _context.Teams
                .Where(b => b.isDeleted == true).ToListAsync();

            return View(members);
        }

        public async Task<IActionResult> BackUp(int id)
        {
            Team removedMembers = await _context.Teams.FindAsync(id);

            if (removedMembers == null) return NotFound();
            removedMembers.DeletedAt = null;
            removedMembers.isDeleted = false;
            removedMembers.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("removedmembers");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Team dbMember = await _context.Teams.FindAsync(id);

            if (dbMember == null) return NotFound();

            if (dbMember.ImageUrl == null) return NotFound();


            string path = Path.Combine(_env.WebRootPath, @"assets\images\member", dbMember.ImageUrl);
            ImageService.DeleteImage(path);

            _context.Teams.Remove(dbMember);
            await _context.SaveChangesAsync();

            return RedirectToAction("removedmembers");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZM_CS296N_TermProject.Models.DataLayer;
using ZM_CS296N_TermProject.Models.DomainModels;
using ZM_CS296N_TermProject.Models.ViewModels;

namespace ZM_CS296N_TermProject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly GameSiteContext _context;
        UserManager<AppUser> userManager;

        public ReviewController(GameSiteContext context, UserManager<AppUser> user)
        {
            _context = context;
            userManager = user;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            return View(await _context.reviews.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.reviews.Include(m => m.Comments)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Title,Message,Rating,Date")] Review review)
        {
            review.User = userManager.GetUserAsync(User).Result;
            review.Date = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,Message,Rating,Date")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.reviews
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.reviews.FindAsync(id);
            _context.reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.reviews.Any(e => e.ReviewId == id);
        }

        public IActionResult WriteReply(int id)
        {
            CommentVM commentVM = new CommentVM { ReviewId = id };
            return View(commentVM);
        }

        [HttpPost]
        public IActionResult WriteReply(CommentVM commentVM)
        {
            Comment comment = new Comment { ReviewId = commentVM.ReviewId, Message = commentVM.Message, Date = DateTime.Now.ToString() };
            var review = (from r in _context.reviews.Include(r => r.Comments)
                          where r.ReviewId == commentVM.ReviewId
                          select r).First<Review>();

            review.AddComment(comment);
            _context.SaveChanges();
            int id = review.ReviewId;
            return RedirectToAction("Index");

        }
    }
}

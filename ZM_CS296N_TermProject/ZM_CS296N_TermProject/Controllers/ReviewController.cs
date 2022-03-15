using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        
        private IReviewRepository repo;
        UserManager<AppUser> userManager;
        private string BannedWordsString = "Seinfield, Hello World!";
        private List<string> BannedWords = new List<string>();
        private RoleManager<IdentityRole> roleManager;
        public ReviewController(IReviewRepository inputRepo, UserManager<AppUser> user, RoleManager<IdentityRole> roleMngr)
        {
            userManager = user;
            repo = inputRepo;
            roleManager = roleMngr;
            BannedWords = BannedWordsString.Split(",").ToList();
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            return View(await repo.SelectAllAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review review = await repo.SelectReviewAndCommentsAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole("Banned")){
                return RedirectToAction("AccessDenied","Account");
            }
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Title,Message,Rating,Date")] Review review)
        {
            if (User.IsInRole("Banned"))
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            review.User = userManager.GetUserAsync(User).Result;
            review.Date = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                repo.Insert(review);
                await repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await repo.SelectByIdAsync(id);
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
                    repo.Update(review);
                    await repo.Save();
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await repo.SelectByIdAsync(id);
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
            var review = await repo.SelectByIdAsync(id);
            repo.Delete(review);
            await repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return repo.Exists(id);
        }

        public IActionResult WriteReply(int id)
        {
            if (User.IsInRole("Banned"))
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            CommentVM commentVM = new CommentVM { ReviewId = id };
            return View(commentVM);
        }

        [HttpPost]
        public async Task<IActionResult> WriteReply(CommentVM commentVM)
        {
            if (User.IsInRole("Banned"))
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            

            Comment comment = new Comment
            {
                ReviewId = commentVM.ReviewId,
                Message = commentVM.Message,
                Date = DateTime.Now.ToString(),
                Commenter = userManager.GetUserAsync(User).Result
            };
            if (ContainsBannedWords(comment.Message))
            {
                return RedirectToAction("Banned");
            }
            Review review = await repo.SelectByIdAsync(commentVM.ReviewId);

            review.AddComment(comment);
            await repo.Save();
            return RedirectToAction("Index");

        }
         
        public async Task<IActionResult> Banned() 
        {
            IdentityRole banRole = await roleManager.FindByNameAsync("Banned");
            if (banRole == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AppUser user = await userManager.GetUserAsync(User);
                await userManager.AddToRoleAsync(user, banRole.Name);
            }
            return View();
        }

        public bool ContainsBannedWords(string inputString)
        {
            foreach(string s in BannedWords)
            {
                if (inputString.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

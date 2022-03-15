using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.DataLayer
{
    public class ReviewRepository : IReviewRepository
    {

        private GameSiteContext ctx { get; set; }
        public ReviewRepository(GameSiteContext inputContext)
        {
            ctx = inputContext;
        }

        public IQueryable<Review> Reviews
        {
            get
            {
               return ctx.reviews
                .Include(m => m.Comments)
                .ThenInclude(m => m.Commenter)
                .Include(m => m.User);

            }
        }

        public void Delete(Review obj)
        {
            ctx.reviews.Remove(obj);
            ctx.SaveChanges();
        }

        public void Insert(Review obj)
        {
            ctx.reviews.Add(obj);
            ctx.SaveChanges();
        }

        public async Task Save()
        {
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> SelectAllAsync()
        {
            List<Review> list = await ctx.reviews.OrderByDescending(m => m.Date).ToListAsync<Review>();
            if (list == null)
            {
                return new List<Review>();
            }
            return list;
        }

        public async Task<Review> SelectByIdAsync(int id)
        {
            return await ctx.reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> SelectWithFilterAsync(string filter)
        {
            IEnumerable<Review> posts;
            if (!String.IsNullOrEmpty(filter))
            {
                posts = await ctx.reviews.Where(s => s.Title.ToLower().Contains(filter.ToLower())).ToListAsync<Review>();
            }
            else
            {
                posts = await ctx.reviews.ToListAsync<Review>();
            }
            return posts;
        }
        public async Task<Review> SelectReviewAndCommentsAsync(int id)
        {
            return await ctx.reviews
                .Include(m => m.Comments)
                .ThenInclude(m => m.Commenter)
                .Include(m => m.User).Where(m=>m.ReviewId == id).FirstOrDefaultAsync<Review>();
        }


        public void Update(Review obj)
        {
            ctx.Update(obj);
        }

        public bool Exists(int id)
        {
            return ctx.reviews.Any(e => e.ReviewId == id);
        }
    }
}

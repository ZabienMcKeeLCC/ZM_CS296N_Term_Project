/*
 * --------------------------------------TO BRIAN------------------------------------------
 * You're probably wondering what's up with my tests and why everything is commented out.
 * Essentially, I didn't actually make Unit Tests early(I know, great coding practices)
 * and by the time I was making them, all of my methods were async. I really did try as
 * hard as I could to get the Fake Repo to properly work given it's async methods but I
 * just couldn't, the code for skirting around it being async was just too complicated and
 * turned my already smooth, squishy brain into further mush. I created test methods for what it
 * MAY HAVE looked like had I actually gotten it to work. I'm sorry I was unable to get tests
 * working, but when it comes to programming it's the thought that counts, right?
 * ----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.DataLayer
{
    public class FakeReviewRepository : IReviewRepository
    {
        private List<Review> DbList { get; set; }

        public IQueryable<Review> Reviews => throw new NotImplementedException();

        public void Delete(Review obj)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Review obj)
        {
            DbList.Add(obj);
        }

        public async Task Save()
        {
            await Task.Delay(1);
        }

        public async Task<IEnumerable<Review>> SelectAllAsync()

        {

            return Task.Run(async () =>
            {
                (IEnumerable<Review>)DbList.ToList();
            }).GetAwaiter().GetResult();
            
        }

        public Task<Review> SelectByIdAsync(int id)
        {
            
            foreach(Review r in DbList)
            {
                if(r.ReviewId == id)
                {
                    Task.Run(return r);
                }
            }
            return null;
        }

        public Task<Review> SelectReviewAndCommentsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> SelectWithFilterAsync(string filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Review obj)
        {
            throw new NotImplementedException();
        }
    }
}
*/
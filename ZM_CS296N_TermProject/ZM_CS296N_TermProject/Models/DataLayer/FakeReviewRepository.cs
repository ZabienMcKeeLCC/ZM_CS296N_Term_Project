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
                (IEnumerable<Review>)DbList;
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

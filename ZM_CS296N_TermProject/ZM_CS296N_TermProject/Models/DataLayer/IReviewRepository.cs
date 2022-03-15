using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.DataLayer
{
    public interface IReviewRepository
    {
        public IQueryable<Review> Reviews { get; }
        void Insert(Review obj);
        public Task<Review> SelectByIdAsync(int id);
        public Task<IEnumerable<Review>> SelectAllAsync();
        public Task<IEnumerable<Review>> SelectWithFilterAsync(string filter);
        public Task<Review>SelectReviewAndCommentsAsync(int id);
        void Delete(Review obj);
        void Update(Review obj);
        bool Exists(int id);
        Task Save();
    }
}

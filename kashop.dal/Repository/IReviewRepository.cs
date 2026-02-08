using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public  interface IReviewRepository
    {
        Task<bool> HasUserReviewProduct(string userId, int productId);
        Task<Review> CreateAsync(Review Request);
    }
}

using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface ICategoryService
    {
        List<CategoryResponse> GetAllCategories();
        CategoryResponse CreateCategory(CategoryRequest Request);
    }
}

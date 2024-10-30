using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dimadev.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface ICategoryHandler 
    {
        Task<Response<Categories?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Categories?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Categories?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Categories?>> GetByIdAsync(GetCategoryByIdRequest request);

        Task<Response<List<Categories>>> GetAllAsync(GetAllCategoriesRequest request);

    }
}

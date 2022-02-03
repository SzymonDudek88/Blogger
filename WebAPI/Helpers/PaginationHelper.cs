using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Wrappers;

namespace WebAPI.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePageResponse<T>
            (IEnumerable<T> pageData, PaginationFilter validPaginationFilter, int totalRecords)
        {
            // to jesdt standardowa implementacja jezeli chodzi o stronicowanie : 
            var response = new PagedResponse<IEnumerable<T>>(pageData, validPaginationFilter.PageNumber, validPaginationFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validPaginationFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32((Math.Ceiling(totalPages)));
            int currentPage = validPaginationFilter.PageNumber;

            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            response.PreviousPage = currentPage > 1 ? true : false;
            response.NextPage = currentPage < roundedTotalPages ? true : false;

            return response;
        }

    }
}

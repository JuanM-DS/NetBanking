using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.CustomEntitys
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> source, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            AddRange(source);
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPageet => CurrentPage < TotalPage;

        public int? NextPage => HasPreviousPage ? CurrentPage + 1: (int?)null;

        public int? PreviousPage => HasPreviousPage ? CurrentPage - 1 : (int?)null;


        public static PagedList<T> Create(IEnumerable<T> source, int currentPage, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((currentPage -1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, currentPage, pageSize, count);
        }
    }
}

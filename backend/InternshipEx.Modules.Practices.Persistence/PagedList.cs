using InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList;
using Microsoft.EntityFrameworkCore;

namespace InternshipEx.Modules.Practices.Persistence
{
    public class PagedList<T> : IPagedList<T>
    {
        public List<T> Items { get; private set; }

        public int TotalCount { get; private set; }

        public int Page { get; private set; }

        public int PageSize { get; private set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        private PagedList(List<T> items, int page, int pageSize, int totalCount) 
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public async static Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new(items, page, pageSize, totalCount);
        }
    }
}

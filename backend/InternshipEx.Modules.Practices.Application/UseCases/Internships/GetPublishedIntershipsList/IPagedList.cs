

namespace InternshipEx.Modules.Practices.Application.UseCases.Internships.GetPublishedIntershipsList
{
    public interface IPagedList<T>
    {
        public List<T> Items { get;}
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;
    }
}

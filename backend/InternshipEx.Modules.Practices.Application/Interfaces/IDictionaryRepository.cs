namespace InternshipEx.Modules.Practices.Application.Interfaces
{
    public interface IDictionaryRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IList<T> GetWithCondition(Func<T, bool> predicate);
    }
}

namespace VendingMachine.DAL.Interfaces
{
    public interface IRepository<TValue, TKey>
    {
        IQueryable<TValue> Entities { get; }

        Task<TValue?> FindAsync(TKey id);

        Task AddAsync(TValue drink);

        Task UpdateAsync(TValue drink);

        Task DeleteAsync(TValue drink);
    }
}

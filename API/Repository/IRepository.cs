namespace GithubSearchApp.API
{
    public interface IRepository<T>
    where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Get(string searchText);

        Task<bool> Create(T entity);

        Task<bool> Delete(T entity);

        //Task<T> Update(T entity);
    }
}

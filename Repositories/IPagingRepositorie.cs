namespace ERP.IntegrationUI.Repositories
{
    public interface IPagingRepositorie<TEntity> where TEntity : class
    {
        List<TEntity> GetPaginatedResult(List<TEntity> data, int currentPage, int pageSize);
        int GetCount(List<TEntity> data);
    }
}

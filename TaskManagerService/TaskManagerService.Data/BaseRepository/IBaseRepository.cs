namespace TaskManagerService.Data.BaseRepository
{
    public interface IBaseRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>, 
        IAddUpdateRepository<TEntity>,
        IDeleteRepository<TKey>
        where TEntity : class, IEntity<TKey>
    {
    }
}
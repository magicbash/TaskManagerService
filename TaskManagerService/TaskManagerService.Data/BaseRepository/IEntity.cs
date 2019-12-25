namespace TaskManagerService.Data.BaseRepository
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
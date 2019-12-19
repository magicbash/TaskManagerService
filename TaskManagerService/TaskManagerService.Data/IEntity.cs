namespace TaskManagerService.Data
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
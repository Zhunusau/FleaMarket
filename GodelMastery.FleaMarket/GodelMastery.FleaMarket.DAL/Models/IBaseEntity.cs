namespace GodelMastery.FleaMarket.DAL.Models
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}

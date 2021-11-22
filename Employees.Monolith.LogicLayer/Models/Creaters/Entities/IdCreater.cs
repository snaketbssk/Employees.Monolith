namespace Employees.Monolith.LogicLayer.Models.Creaters.Entities
{
    public abstract class IdCreater<T> : Creater<T>, IIdCreater
         where T : class
    {
        public int Id { get; set; }
        public IdCreater(int id) :
            base()
        {
            Id = id;
        }
    }
}

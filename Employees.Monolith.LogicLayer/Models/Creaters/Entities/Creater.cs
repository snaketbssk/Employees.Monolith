namespace Employees.Monolith.LogicLayer.Models.Creaters.Entities
{
    public abstract class Creater<T> : ICreater<T>
        where T : class
    {
        public Creater()
        {
        }
        public abstract T Create();
        public abstract T Update(T result);
    }
}

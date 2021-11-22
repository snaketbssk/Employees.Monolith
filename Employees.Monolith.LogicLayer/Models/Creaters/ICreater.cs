namespace Employees.Monolith.LogicLayer.Models.Creaters
{
    public interface ICreater<T>
        where T : class
    {
        T Create();
        T Update(T result);
    }
}

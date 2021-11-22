namespace Employees.Monolith.LogicLayer.Managers.Entities
{
    public abstract class Manager<T> where T : class
    {
        protected T _context { get; private set; }
        public Manager(T context)
        {
            _context = context;
        }
    }
}

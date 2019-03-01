using System.Linq;

namespace TestNinja.Moq
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}
using System.Linq;
using TDD.Demo.Domain;

namespace TDD.Demo.Application
{
    public interface IDbSet<TEntity> : IQueryable<TEntity> where TEntity : EntityBase
    {
        void Save(TEntity entity);

        void Delete(TEntity entity);
    }
}

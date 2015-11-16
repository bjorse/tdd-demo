using TDD.Demo.Domain;

namespace TDD.Demo.Application.Util
{
    public interface IEntitySaver
    {
        TEntity Save<TEntity>(IDbContext context, TEntity entity, RevisionPolicy policy) where TEntity : EntityBase;
    }
}

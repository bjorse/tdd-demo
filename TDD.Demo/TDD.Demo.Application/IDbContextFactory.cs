namespace TDD.Demo.Application
{
    public interface IDbContextFactory
    {
        IDbContext Create();
    }
}

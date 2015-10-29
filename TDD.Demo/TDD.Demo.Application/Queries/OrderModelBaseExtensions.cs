using System.Linq;
using TDD.Demo.Domain;

namespace TDD.Demo.Application.Queries
{
    /// <summary>
    /// Extension methods för att arbeta mot <see cref="OrderModelBase"/>
    /// </summary>
    public static class OrderModelBaseExtensions
    {
        public static TEntity GetLatestByOrderNumber<TEntity>(this IDbSet<TEntity> set, int orderNumber) where TEntity : OrderModelBase
        {
            return set.Where(x => x.OrderInfo.Id == orderNumber).OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}

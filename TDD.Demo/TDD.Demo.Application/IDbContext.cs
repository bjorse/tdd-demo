using System;
using TDD.Demo.Domain;
using TDD.Demo.Domain.Items;
using TDD.Demo.Domain.Shipments;

namespace TDD.Demo.Application
{
    public interface IDbContext : IDisposable
    {
        IDbSet<ItemModel> Items { get; }

        IDbSet<OrderInfoModel> OrderInfo { get; }

        IDbSet<OrderInfoModel> Orders { get; }

        IDbSet<OrderShipmentModel> Shipments { get; } 

        void SaveChanges();
    }
}

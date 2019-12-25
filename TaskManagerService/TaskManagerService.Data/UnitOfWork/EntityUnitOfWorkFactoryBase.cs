using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagerService.Data.UnitOfWork
{
    public abstract class EntityUnitOfWorkFactoryBase : IUnitOfWorkFactory
    {
        private readonly IServiceProvider serviceProvider;

        protected EntityUnitOfWorkFactoryBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IUnitOfWork Create()
        {
            var dbContexts = GetContexts();

            return new EntityUnitOfWork(dbContexts);
        }

        protected TContext GetContext<TContext>() where TContext : DbContext
        {
            return serviceProvider.GetRequiredService<TContext>();
        }

        protected abstract DbContext[] GetContexts();
    }
}
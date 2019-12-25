using System;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerService.Data.UnitOfWork
{
    public class EntityUnitOfWorkFactory : EntityUnitOfWorkFactoryBase
    {
        public EntityUnitOfWorkFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override DbContext[] GetContexts()
        {
            return new DbContext[] { GetContext<Context>() };
        }
    }
}
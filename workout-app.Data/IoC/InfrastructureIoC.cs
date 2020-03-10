using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Data.IoC
{
    public class InfrastructureIoC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterContext<WorkoutAppDbContext>(builder);
        }

        public void RegisterContext<TContext>(ContainerBuilder builder) where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();

                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}

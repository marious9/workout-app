using Autofac;
using Microsoft.EntityFrameworkCore;
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
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<WorkoutAppDbContext>();
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));

                return new WorkoutAppDbContext(opt.Options);
            }).InstancePerLifetimeScope();
        }
    }
}

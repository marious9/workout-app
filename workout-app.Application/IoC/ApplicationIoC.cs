using System;
using System.Collections.Generic;
using AutoMapper;
using Autofac;
using MediatR;
using workout_app.Application.PipelineBehaviours;
using MediatR.Pipeline;
using workout_app.Application.Queries;
using workout_app.Application.Commands;
using FluentValidation;

public class ApplicationIoC : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        builder.RegisterAssemblyTypes(assemblies)
            .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
            .As<Profile>();

        builder.Register(c => new MapperConfiguration(cfg => {
            foreach (var profile in c.Resolve<IEnumerable<Profile>>())
            {
                cfg.AddProfile(profile);
            }
        })).AsSelf().SingleInstance();

        builder.Register(c => c.Resolve<MapperConfiguration>()
            .CreateMapper(c.Resolve))
            .As<IMapper>()
            .InstancePerLifetimeScope();

        builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

        builder.RegisterType<GetAllExercises.GetAllExercisesHandler>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<CreateExercise.CreateExerciseHandler>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<GetExerciseById.GetExerciseByIdHandler>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));


        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.IsClosedTypeOf(typeof(AbstractValidator<>)))
            .AsImplementedInterfaces();

        builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        base.Load(builder);
    }
}
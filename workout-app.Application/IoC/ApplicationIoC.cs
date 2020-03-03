﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Autofac;
using MediatR;
using workout_app.Application.PipelineBehaviours;
using workout_app.Application.Handlers;
using MediatR.Pipeline;

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

        //services.AddValidatorsFromAssembly(typeof(CreateExerciseCommandValidator).Assembly);
        builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

        builder.RegisterType<GetAllExercisesHandler>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterType<CreateExerciseHandler>().AsImplementedInterfaces().InstancePerDependency();
        builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));


        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });

        builder.RegisterTypes(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        base.Load(builder);
    }
}
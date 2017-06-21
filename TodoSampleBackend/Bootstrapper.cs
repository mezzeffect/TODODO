using Autofac;
using AutoMapper;
using TodoSampleBackend.Business;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Models;

namespace TodoSampleBackend
{
    public static class Bootstrapper
    { 
        public static void Run()
        {
            var builder = new ContainerBuilder();
            ConfigureAutofacContainer(builder);
            AutoMapperConfigurator.ConfigureAll();
        }

        private static void ConfigureAutofacContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<TodoSampleBackendAppModule>();
            AppAutoFac.Container = builder.Build();
        }

    }
}
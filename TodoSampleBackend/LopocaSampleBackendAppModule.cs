using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using TodoSampleBackend.Business;
using TodoSampleBackend.Controllers;
using TodoSampleBackend.Data;
using TodoSampleBackend.DataObjects;
using TodoSampleBackend.Interfaces.Business;
using TodoSampleBackend.Interfaces.Data;
using TodoSampleBackend.Interfaces.Repository;
using TodoSampleBackend.Repository;
using Module = Autofac.Module;

namespace TodoSampleBackend
{
    public class TodoSampleBackendAppModule : Module
    {  
        protected override void Load(ContainerBuilder builder)
        {
            RegisterControllers(builder);
            RegisterDatabaseContexts(builder);
            RegisterRepositories(builder);
            RegisterBusinessServices(builder);
            base.Load(builder);
        }

        private static void RegisterBusinessServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserBusinessServiceManager>().As<IUserServiceManager>();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IEntityRepository<User>>();
        }

        private static void RegisterDatabaseContexts(ContainerBuilder builder)
        {
            builder.RegisterType<TodoSampleDataContext>().As<ITodoSampleDataContext>();
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CustomAuthController>();
        }
    }
}
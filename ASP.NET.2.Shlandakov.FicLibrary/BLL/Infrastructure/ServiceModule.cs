using DAL.Concrete;
using DAL.Interface;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class ServiceModule: NinjectModule
    {
        private readonly string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
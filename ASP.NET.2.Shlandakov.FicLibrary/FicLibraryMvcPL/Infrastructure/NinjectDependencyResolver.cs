using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Services;
using Ninject;
using BLL.Interfaces;

namespace FicLibraryMvcPL.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<ITextDescriptionService>().To<TextDescriptionService>();
            kernel.Bind<ITextService>().To<TextService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ICommentRelationService>().To<CommentRelationService>();
            kernel.Bind<IRatingService>().To<RatingService>();
        }
    }
}
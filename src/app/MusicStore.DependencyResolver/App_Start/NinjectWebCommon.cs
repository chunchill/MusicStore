using MusicStore.Core.Interfaces;
using MusicStore.Core.Models;
using MusicStore.Infrastructure.DataAccess;
using MusicStore.Infrastructure.Repositories;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MusicStore.DependencyResolver.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MusicStore.DependencyResolver.App_Start.NinjectWebCommon), "Stop")]

namespace MusicStore.DependencyResolver.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel); 
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            ServiceLocator.SetServiceLocator(() => new NinjectServiceLocator(kernel));
            kernel.Bind<IUnitOfWork>().To<MusicStoreContext>().InRequestScope();
            kernel.Bind<IRepository<Album>>().To<Repository<Album>>();
            kernel.Bind<IRepository<Genre>>().To<Repository<Genre>>();
            kernel.Bind<IRepository<Artist>>().To<Repository<Artist>>();
            kernel.Bind<IRepository<Cart>>().To<Repository<Cart>>();
            kernel.Bind<IRepository<User>>().To<Repository<User>>();
            kernel.Bind<IRepository<Role>>().To<Repository<Role>>();
            kernel.Bind<IRepository<Order>>().To<Repository<Order>>();
            kernel.Bind<IRepository<OrderDetail>>().To<Repository<OrderDetail>>();
            kernel.Bind<IRepository<News>>().To<Repository<News>>();
            kernel.Bind<IRepository<NewsType>>().To<Repository<NewsType>>();
         
        }        
    }
}

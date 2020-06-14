using System;
using Autofac.Core;
using Xamarin.Forms;
using Autofac;

namespace PhantomLib.DendencyInjection
{
    public static class PhantomResolutionHelper
    {
        private readonly static PhantomContainerBuilder _builder = new PhantomContainerBuilder();
        private static IContainer _container; 

        public static void Register(Action<PhantomContainerBuilder> registration)
        {
            registration.Invoke(_builder);

            _container = _builder.Build();
        }

        public static Page ResolvePage<P, M>(Action<M> initialize = null, bool unwrapExceptions = true)
            where P : Page
            where M : InjectablePageModel
        {
            if (_container is null)
                throw new Exception("Unable to resolve pages, no container built. Please use Register to register services");
            
            try
            {
                var page = _container.Resolve<P>();
                var pageModel = _container.Resolve<M>();

                pageModel.Navigation = page.Navigation;
                initialize?.Invoke(pageModel);
                page.BindingContext = pageModel;

                page.Appearing += async (sender, e) => await pageModel.OnAppearing();

                return page;
            }
            catch (DependencyResolutionException dependencyException)
            {
                Exception exception = dependencyException;

                if (unwrapExceptions)
                {
                    while (exception?.GetType() == typeof(DependencyResolutionException))
                    {
                        exception = exception.InnerException;
                    }
                }

                throw exception;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace PhantomLib.DendencyInjection
{
    public class PhantomContainerBuilder
    { 
        private ContainerBuilder _container;

        public PhantomContainerBuilder()
        {
            _container = new ContainerBuilder();
        }

        public PhantomContainerBuilder RegisterNamespace(string ns)
        {

            var types = Assembly
                .GetCallingAssembly()
                .GetTypes();

            List<Type> pageTypes = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Namespace == ns)
                .ToList();

            foreach (Type t in pageTypes)
            {
                _container.RegisterType(t);
            }

            return this;
        }

        public PhantomContainerBuilder RegisterView<T>()
        {
            _container.RegisterType<T>();
            return this;
        }

        public PhantomContainerBuilder RegisterViewModel<T>()
        {
            _container.RegisterType<T>();
            return this;
        }

        /// <summary>
        /// Register an instance type as a template, such as MyService as IMyService. IService can then be injected into any registered Views, ViewModels, or other registered Singletons.
        /// </summary>
        /// <typeparam name="I">Instance</typeparam>
        /// <typeparam name="T">Template</typeparam>
        /// <returns></returns>
        public PhantomContainerBuilder RegisterSingleton<I, T>()
        {
            _container.RegisterType<I>()
                .As<T>()
                .SingleInstance();

            return this;
        }

        internal IContainer Build()
        {
            return _container.Build();
        }
    }
}
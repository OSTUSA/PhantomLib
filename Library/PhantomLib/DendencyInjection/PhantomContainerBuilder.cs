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
        /// 
        /// </summary>
        /// <typeparam name="T">Template</typeparam>
        /// <typeparam name="S">Service</typeparam>
        /// <returns></returns>
        public PhantomContainerBuilder RegisterSingletonService<T, S>()
        {
            _container.RegisterType<T>()
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;

namespace PhantomLib.DendencyInjection
{
    public class PhantomContainerBuilder : ContainerBuilder
    { 
        public PhantomContainerBuilder RegisterAssembly(string assembly)
        {
            List<Type> pageTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Namespace == assembly)
                .ToList();

            foreach (Type t in pageTypes)
            {
                this.RegisterType(t);
            }

            return this;
        }
    }
}
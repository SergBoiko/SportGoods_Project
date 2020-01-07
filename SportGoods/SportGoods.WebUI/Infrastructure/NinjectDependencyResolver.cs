using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportGoods.Domain.Abstract;
using SportGoods.Domain.Entities;

namespace SportGoods.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
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
            Mock<ISportProductRepository> mock = new Mock<ISportProductRepository>();
            mock.Setup(m => m.SportProducts).Returns(new List<SportProduct>
            {
                new SportProduct { Name = "Basketball", Price = 2399},
                new SportProduct { Name = "Knee Pads", Price = 899},
                new SportProduct { Name = "Thermo Sleeve", Price = 999}
            });
            kernel.Bind<ISportProductRepository>().ToConstant(mock.Object);
        }
    }
}
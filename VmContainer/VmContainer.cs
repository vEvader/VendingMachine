using System;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Provider;
using ProviderInterface;
using Repository;
using RepositoryInterface;

namespace Container
{
    public class VmContainer
    {
        private static readonly Lazy<VmContainer> m_lazy = new Lazy<VmContainer>(() => new VmContainer());

        private readonly WindsorContainer m_windsor;

        public static VmContainer Instance
        {
            get
            {
                return m_lazy.Value;
            }
        }

        public TService Resolve<TService>()
        {
            return m_windsor.Resolve<TService>();
        }

        private VmContainer()
        {
            m_windsor = new WindsorContainer();
            m_windsor.Register(Component.For<IVmProvider>().ImplementedBy<VmProvider>());
            m_windsor.Register(Component.For<IVmRepository>().ImplementedBy<VmRepository>());
        }
    }
}

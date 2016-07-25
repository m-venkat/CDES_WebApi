using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PhoneNumberEnrichmentService.Models;
using PhoneNumberEnrichmentService.Services;
using PhoneNumberEnrichmentService.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;

namespace CDES_WebApi.DI.Unity
{
    public class BootStrapDI
    {
        private static  UnityContainer _unityContainer = null;
        private static object _Lock = new object();

       public static IUnityContainer Container
        {
            get {
                if(_unityContainer == null)
                {
                    RegisterDependencies();                   
                }
                return _unityContainer;
            }
        }
            

        private static void InitializeContainer()
        {
            if(_unityContainer == null)
            {
                lock (_Lock)
                {
                    _unityContainer = new UnityContainer();
                }
            }
        }

        public static void RegisterDependencies()
        {
            if (_unityContainer == null)
                InitializeContainer();
            _unityContainer.RegisterType<IPhoneEnrichment, PremiumPhoneEnrichmentService>();
            _unityContainer.RegisterType<IPhoneEnriched, PhonePremiumEnriched>();

        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class UnityDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private IUnityContainer _container = null;
        public UnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException();
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            try { 
            if (serviceType == null || _container == null)
                return null;
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException rex)
            {
                return null;
            }

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                if (serviceType == null || _container == null)
                    return null;
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException rex)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityDependencyResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }

   
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Inspire.Shared.Service
{
    /// <summary>
    /// A generic service container that provides collections and a base service 
    /// </summary>
    public abstract class ServiceContainer
    {

        // Used to implement the GetService function
        private Dictionary<Type, Service> _serviceLookupTable;


        public ServiceContainer()
        {
            _serviceLookupTable = new Dictionary<Type, Service>();
        }

        /// <summary>
        /// Gets a specific type of service from the service container.
        /// This is useful for letting services talk to each other if required.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public T GetService<T>() where T : Service
        {
            return (T)_serviceLookupTable.Values.FirstOrDefault(c => c.GetType() == typeof(T));
        }

        /// <summary>
        /// Registers a service into the container.
        /// </summary>
        /// <param name="service"></param>
        public virtual void RegisterService(Service service)
        {
            service.ServiceContainer = this;
            _serviceLookupTable.Add(service.GetType(), service);

            // Setup now
            service.Setup();

        }

        /// <summary>
        /// Performs updates on all the services
        /// </summary>
        public void PerformUpdates()
        {
            foreach (var service in _serviceLookupTable.Values)
            {
                service.PeformUpdate();
            }
        }




    }
}

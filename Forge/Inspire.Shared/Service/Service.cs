namespace Inspire.Shared.Service
{
    /// <summary>
    /// A service that is performed by a process
    /// </summary>
    public abstract class Service
    {

        /// <summary>
        /// Performs updating on the service object
        /// </summary>
        public abstract void PeformUpdate();

        /// <summary>
        /// The parent service container
        /// </summary>
        public ServiceContainer ServiceContainer { get; set; }

        public abstract void Setup();

    }
}

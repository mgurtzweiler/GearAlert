    using Ninject;
    using NServiceBus;
    using NServiceBus.ObjectBuilder.Common.Config;
namespace GearAlert.Configuration {
    /// <summary>
    /// The static class which holds <see cref="NServiceBus"/> extensions methods.
    /// </summary>
    public static class NinjectObjectBuilderConfig {
        /// <summary>
        /// Instructs <see cref="NServiceBus"/> to use the provided kernel
        /// </summary>
        /// <param name="config">The extended Configure.</param>
        /// <param name="kernel">The kernel.</param>
        /// <returns>The Configure.</returns>
        public static Configure NinjectBuilder(this Configure config, IKernel kernel) {
            ConfigureCommon.With(config, new NinjectObjectBuilder(kernel));
            return config;
        }
    }
}
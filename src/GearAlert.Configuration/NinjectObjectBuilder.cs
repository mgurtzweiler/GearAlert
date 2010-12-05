    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using log4net;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Activation.Strategies;
    using Ninject.Infrastructure;
    using Ninject.Injection;
    using Ninject.Parameters;
    using Ninject.Selection;
    using NServiceBus.ObjectBuilder;
    using NServiceBus.ObjectBuilder.Common;

namespace GearAlert.Configuration {
    /// <summary>
    /// Implementation of IBuilderInternal using the N inject Framework container
    /// </summary>
    public class NinjectObjectBuilder : IContainer {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The kernel hold by this object builder.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// The object builders injection propertyHeuristic for properties.
        /// </summary>
        private readonly IObjectBuilderPropertyHeuristic propertyHeuristic;

        /// <summary>
        /// Maps the supported <see cref="ComponentCallModelEnum"/> to the <see cref="StandardScopeCallbacks"/> of ninject.
        /// </summary>
        private readonly IDictionary<ComponentCallModelEnum, Func<IContext, object>> callModelToScopeMapping =
            new Dictionary<ComponentCallModelEnum, Func<IContext, object>>
                {
                    { ComponentCallModelEnum.Singleton, StandardScopeCallbacks.Singleton }, 
                    { ComponentCallModelEnum.Singlecall, StandardScopeCallbacks.Transient }, 
                };

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectObjectBuilder"/> class.
        /// </summary>
        /// <remarks>
        /// Uses the default object builder property <see cref="propertyHeuristic"/> 
        /// <see cref="ObjectBuilderPropertyHeuristic"/>.
        /// </remarks>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public NinjectObjectBuilder(IKernel kernel) {
            this.kernel = kernel;

            this.RegisterNecessaryBindings();

            this.propertyHeuristic = this.kernel.Get<IObjectBuilderPropertyHeuristic>();

            this.AddCustomPropertyInjectionHeuristic();

            this.ReplacePropertyInjectionStrategyWithCustomPropertyInjectionStrategy();
        }

        /// <summary>
        /// Builds the specified type.
        /// </summary>
        /// <param name="typeToBuild">
        /// The type to build.
        /// </param>
        /// <returns>
        /// An instance of the given type.
        /// </returns>
        public object Build(Type typeToBuild) {
            var output = this.kernel.Get(typeToBuild);

            return output;
        }

        /// <summary>
        /// Returns a list of objects instantiated because their type is compatible with the given type.
        /// </summary>
        /// <param name="typeToBuild">
        /// The type to build.
        /// </param>
        /// <returns>
        /// A list of objects
        /// </returns>
        public IEnumerable<object> BuildAll(Type typeToBuild) {
            var output = this.kernel.GetAll(typeToBuild);

            return output;
        }

        /// <summary>
        /// Configures the specified component.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="callModel">
        /// The call model.
        /// </param>
        public void Configure(Type component, ComponentCallModelEnum callModel) {
            if (this.HasComponent(component)) {
                Log.DebugFormat(
                    CultureInfo.InvariantCulture,
                    "Skipping configuration for {0} and call model {1}",
                    component.FullName,
                    callModel);
                return;
            }

            var instanceScope = this.GetInstanceScopeFrom(callModel);

            this.BindComponentToItself(component, instanceScope);

            this.BindAliasesOfComponentToComponent(component, instanceScope);

            Log.DebugFormat(
                CultureInfo.InvariantCulture,
                "Registering configuration for {0} and call model {1}",
                component.FullName,
                callModel);

            this.propertyHeuristic.RegisteredTypes.Add(component);
        }

        /// <summary>
        /// Configures the property.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void ConfigureProperty(Type component, string property, object value) {
            var bindings = this.kernel.GetBindings(component);

            if (!bindings.Any()) {
                throw new ArgumentException("Component not registered", "component");
            }

            foreach (var binding in bindings) {
                binding.Parameters.Add(new PropertyValue(property, value));
            }
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <param name="lookupType">
        /// Type lookup type.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public void RegisterSingleton(Type lookupType, object instance) {
            this.kernel.Bind(lookupType).ToConstant(instance);
        }

        /// <summary>
        /// Determines whether the specified component type has a component.
        /// </summary>
        /// <param name="componentType">
        /// Type of the component.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified component type has a component; otherwise, <c>false</c>.
        /// </returns>
        public bool HasComponent(Type componentType) {
            var bindings = this.kernel.GetBindings(componentType);
            return bindings.Any();
        }

        /// <summary>
        /// Gets all service types of a given component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>All service types.</returns>
        private static IEnumerable<Type> GetAllServices(Type component) {
            return component.GetInterfaces();
        }

        /// <summary>
        /// Gets the instance scope from call model.
        /// </summary>
        /// <param name="callModel">
        /// The call model.
        /// </param>
        /// <returns>
        /// The instance scope
        /// </returns>
        private Func<IContext, object> GetInstanceScopeFrom(ComponentCallModelEnum callModel) {
            Func<IContext, object> scope;

            if (!this.callModelToScopeMapping.TryGetValue(callModel, out scope)) {
                throw new ArgumentException("The call model is not supported", "callModel");
            }

            return scope;
        }

        /// <summary>
        /// Binds the aliases of component to component with the given <paramref name="instanceScope"/>.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="instanceScope">
        /// The instance scope.
        /// </param>
        private void BindAliasesOfComponentToComponent(Type component, Func<IContext, object> instanceScope) {
            var services = GetAllServices(component).Where(t => t != component);

            foreach (var service in services) {
                this.kernel.Bind(service).ToMethod(ctx => ctx.Kernel.Get(component))
                    .InScope(instanceScope);
            }
        }

        /// <summary>
        /// Binds the component to itself with the given <paramref name="instanceScope"/>.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="instanceScope">
        /// The instance scope.
        /// </param>
        private void BindComponentToItself(Type component, Func<IContext, object> instanceScope) {
            this.kernel.Bind(component).ToSelf()
                .InScope(instanceScope);
        }

        /// <summary>
        /// Adds the custom property injection heuristic.
        /// </summary>
        private void AddCustomPropertyInjectionHeuristic() {
            this.kernel.Components.Get<ISelector>().InjectionHeuristics.Add(
                this.kernel.Get<IObjectBuilderPropertyHeuristic>());
        }

        /// <summary>
        /// Registers the necessary bindings.
        /// </summary>
        private void RegisterNecessaryBindings() {
            this.kernel.Bind<IContainer>().ToConstant(this).InSingletonScope();

            this.kernel.Bind<NewActivationPropertyInjectStrategy>().ToSelf()
                .InSingletonScope()
                .WithPropertyValue("Settings", ctx => ctx.Kernel.Settings);

            this.kernel.Bind<IObjectBuilderPropertyHeuristic>().To<ObjectBuilderPropertyHeuristic>()
                .InSingletonScope()
                .WithPropertyValue("Settings", ctx => ctx.Kernel.Settings);

            this.kernel.Bind<IInjectorFactory>().ToMethod(ctx => ctx.Kernel.Components.Get<IInjectorFactory>());
        }

        /// <summary>
        /// Replaces the default property injection strategy with custom property injection strategy.
        /// </summary>
        private void ReplacePropertyInjectionStrategyWithCustomPropertyInjectionStrategy() {
            IList<IActivationStrategy> activationStrategies = this.kernel.Components.Get<IPipeline>().Strategies;

            IList<IActivationStrategy> copiedStrategies = new List<IActivationStrategy>(
                activationStrategies.Where(strategy => !strategy.GetType().Equals(typeof(PropertyInjectionStrategy)))
                    .Union(new List<IActivationStrategy> { this.kernel.Get<NewActivationPropertyInjectStrategy>() }));

            activationStrategies.Clear();
            copiedStrategies.ToList().ForEach(activationStrategies.Add);
        }
    }
}
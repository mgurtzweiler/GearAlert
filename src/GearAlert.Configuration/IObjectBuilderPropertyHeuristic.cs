    using System;
    using System.Collections.Generic;
    using Ninject.Selection.Heuristics;
namespace GearAlert.Configuration {
    /// <summary>
    /// Implements a heuristic for ninject property injection.
    /// </summary>
    public interface IObjectBuilderPropertyHeuristic : IInjectionHeuristic {
        /// <summary>
        /// Gets the registered types.
        /// </summary>
        /// <value>The registered types.</value>
        IList<Type> RegisteredTypes {
            get;
        }
    }
}
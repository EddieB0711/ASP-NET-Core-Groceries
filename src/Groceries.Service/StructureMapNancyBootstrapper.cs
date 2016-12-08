﻿namespace Groceries.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Configuration;
    using Nancy.Diagnostics;
    using Nancy.ViewEngines;

    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>
    /// Nancy bootstrapper for the StructureMap container.
    /// </summary>
    public abstract class StructureMapNancyBootstrapper : NancyBootstrapperWithRequestContainerBase<IContainer>,
                                                          IDisposable
    {
        private bool isDisposing = false;

        /// <summary>
        /// Get the <see cref="INancyEnvironment" /> instance.
        /// </summary>
        /// <returns>An configured <see cref="INancyEnvironment" /> instance.</returns>
        /// <remarks>The boostrapper must be initialised (<see cref="INancyBootstrapper.Initialise" />) prior to calling this.</remarks>
        public override INancyEnvironment GetEnvironment()
        {
            return this.ApplicationContainer.GetInstance<INancyEnvironment>();
        }

        public new void Dispose()
        {
            if (this.isDisposing)
            {
                return;
            }

            this.isDisposing = true;
            base.Dispose();
        }

        /// <summary>
        /// Gets the diagnostics for initialisation
        /// </summary>
        /// <returns>An <see cref="IDiagnostics"/> implementation</returns>
        protected override IDiagnostics GetDiagnostics()
        {
            return this.ApplicationContainer.GetInstance<IDiagnostics>();
        }

        /// <summary>
        /// Gets all registered application startup tasks
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}"/> instance containing <see cref="IApplicationStartup"/> instances. </returns>
        protected override IEnumerable<IApplicationStartup> GetApplicationStartupTasks()
        {
            return this.ApplicationContainer.GetAllInstances<IApplicationStartup>();
        }

        /// <summary>
        /// Gets all registered request startup tasks
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance containing <see cref="IRequestStartup"/> instances.</returns>
        protected override IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(
            IContainer container,
            Type[] requestStartupTypes)
        {
            return requestStartupTypes.Select(container.GetInstance).Cast<IRequestStartup>().ToArray();
        }

        /// <summary>
        /// Gets all registered application registration tasks
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}"/> instance containing <see cref="IRegistrations"/> instances.</returns>
        protected override IEnumerable<IRegistrations> GetRegistrationTasks()
        {
            return this.ApplicationContainer.GetAllInstances<IRegistrations>();
        }

        /// <summary>
        /// Resolve <see cref="INancyEngine"/>
        /// </summary>
        /// <returns><see cref="INancyEngine"/> implementation</returns>
        protected override INancyEngine GetEngineInternal()
        {
            return this.ApplicationContainer.GetInstance<INancyEngine>();
        }

        /// <summary>
        /// Gets the <see cref="INancyEnvironmentConfigurator"/> used by th.
        /// </summary>
        /// <returns>An <see cref="INancyEnvironmentConfigurator"/> instance.</returns>
        protected override INancyEnvironmentConfigurator GetEnvironmentConfigurator()
        {
            return this.ApplicationContainer.GetInstance<INancyEnvironmentConfigurator>();
        }

        /// <summary>
        /// Registers an <see cref="INancyEnvironment"/> instance in the container.
        /// </summary>
        /// <param name="container">The container to register into.</param>
        /// <param name="environment">The <see cref="INancyEnvironment"/> instance to register.</param>
        protected override void RegisterNancyEnvironment(IContainer container, INancyEnvironment environment)
        {
            container.Configure(registry => registry.For<INancyEnvironment>().Use(environment));
        }

        /// <summary>
        /// Gets the application level container
        /// </summary>
        /// <returns>Container instance</returns>
        protected override IContainer GetApplicationContainer()
        {
            return new Container();
        }

        /// <summary>
        /// Register the bootstrapper's implemented types into the container.
        /// This is necessary so a user can pass in a populated container but not have
        /// to take the responsibility of registering things like <see cref="INancyModuleCatalog"/> manually.
        /// </summary>
        /// <param name="applicationContainer">Application container to register into</param>
        protected override void RegisterBootstrapperTypes(IContainer applicationContainer)
        {
            applicationContainer.Configure(
                registry =>
                    {
                        registry.For<INancyModuleCatalog>().Singleton().Use(this);
                        registry.For<IFileSystemReader>().Singleton().Use<DefaultFileSystemReader>();
                    });
        }

        /// <summary>
        /// Register the default implementations of internally used types into the container as singletons
        /// </summary>
        /// <param name="container">Container to register into</param>
        /// <param name="typeRegistrations">Type registrations to register</param>
        protected override void RegisterTypes(IContainer container, IEnumerable<TypeRegistration> typeRegistrations)
        {
            container.Configure(
                registry =>
                    {
                        foreach (var typeRegistration in typeRegistrations)
                        {
                            RegisterType(
                                typeRegistration.RegistrationType,
                                typeRegistration.ImplementationType,
                                container.Role == ContainerRole.Nested ? Lifetime.PerRequest : typeRegistration.Lifetime,
                                registry);
                        }
                    });
        }

        /// <summary>
        /// Register the various collections into the container as singletons to later be resolved
        /// by IEnumerable{Type} constructor dependencies.
        /// </summary>
        /// <param name="container">Container to register into</param>
        /// <param name="collectionTypeRegistrationsn">Collection type registrations to register</param>
        protected override void RegisterCollectionTypes(
            IContainer container,
            IEnumerable<CollectionTypeRegistration> collectionTypeRegistrationsn)
        {
            container.Configure(
                registry =>
                    {
                        foreach (var collectionTypeRegistration in collectionTypeRegistrationsn)
                        {
                            foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
                            {
                                RegisterType(
                                    collectionTypeRegistration.RegistrationType,
                                    implementationType,
                                    container.Role == ContainerRole.Nested
                                        ? Lifetime.PerRequest
                                        : collectionTypeRegistration.Lifetime,
                                    registry);
                            }
                        }
                    });
        }

        /// <summary>
        /// Register the given instances into the container
        /// </summary>
        /// <param name="container">Container to register into</param>
        /// <param name="instanceRegistrations">Instance registration types</param>
        protected override void RegisterInstances(
            IContainer container,
            IEnumerable<InstanceRegistration> instanceRegistrations)
        {
            container.Configure(
                registry =>
                    {
                        foreach (var instanceRegistration in instanceRegistrations)
                        {
                            registry.For(instanceRegistration.RegistrationType)
                                .LifecycleIs(Lifecycles.Singleton)
                                .Use(instanceRegistration.Implementation);
                        }
                    });
        }

        /// <summary>
        /// Creates a per request child/nested container
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns>Request container instance</returns>
        protected override IContainer CreateRequestContainer(NancyContext context)
        {
            return this.ApplicationContainer.GetNestedContainer();
        }

        /// <summary>
        /// Register the given module types into the request container
        /// </summary>
        /// <param name="container">Container to register into</param>
        /// <param name="moduleRegistrationTypes"><see cref="INancyModule"/> types</param>
        protected override void RegisterRequestContainerModules(
            IContainer container,
            IEnumerable<ModuleRegistration> moduleRegistrationTypes)
        {
            container.Configure(
                registry =>
                    {
                        foreach (var registrationType in moduleRegistrationTypes)
                        {
                            registry.For(typeof(INancyModule))
                                .LifecycleIs(Lifecycles.Unique)
                                .Use(registrationType.ModuleType);
                        }
                    });
        }

        /// <summary>
        /// Retrieve all module instances from the container
        /// </summary>
        /// <param name="container">Container to use</param>
        /// <returns>Collection of <see cref="INancyModule"/> instances</returns>
        protected override IEnumerable<INancyModule> GetAllModules(IContainer container)
        {
            return container.GetAllInstances<INancyModule>();
        }

        /// <summary>
        /// Retreive a specific module instance from the container
        /// </summary>
        /// <param name="container">Container to use</param>
        /// <param name="moduleType">Type of the module</param>
        /// <returns>A <see cref="INancyModule"/> instance</returns>
        protected override INancyModule GetModule(IContainer container, Type moduleType)
        {
            return (INancyModule)container.GetInstance(moduleType);
        }

        private static void RegisterType(
            Type registrationType,
            Type implementationType,
            Lifetime lifetime,
            IProfileRegistry registry)
        {
            switch (lifetime)
            {
                case Lifetime.Transient:
                    registry.For(registrationType).LifecycleIs(Lifecycles.Unique).Use(implementationType);
                    break;
                case Lifetime.Singleton:
                    registry.For(registrationType).LifecycleIs(Lifecycles.Singleton).Use(implementationType);
                    break;
                case Lifetime.PerRequest:
                    registry.For(registrationType).Use(implementationType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                              "lifetime",
                              lifetime,
                              String.Format("Unknown Lifetime: {0}.", lifetime));
            }
        }
    }
}
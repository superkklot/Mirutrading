using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.IOC
{
	public class MirutradingContainer : IContainer
	{
		private IContainer _container;
		private static MirutradingContainer _proxycontainer = new MirutradingContainer();

		public static MirutradingContainer Instance
		{
			get { return _proxycontainer; }
		}

		protected MirutradingContainer()
		{
			_container = ContainerFactory.GetContainer();
		}

		public void AssertConfigurationIsValid()
		{
			throw new NotImplementedException();
		}

		public void BuildUp(object target)
		{
			throw new NotImplementedException();
		}

		public void Configure(Action<ConfigurationExpression> configure)
		{
			throw new NotImplementedException();
		}

		public IContainer CreateChildContainer()
		{
			throw new NotImplementedException();
		}

		public void EjectAllInstancesOf<T>()
		{
			throw new NotImplementedException();
		}

		public Container.OpenGenericTypeExpression ForGenericType(Type templateType)
		{
			throw new NotImplementedException();
		}

		public CloseGenericTypeExpression ForObject(object subject)
		{
			throw new NotImplementedException();
		}

		public System.Collections.IEnumerable GetAllInstances(Type pluginType, StructureMap.Pipeline.ExplicitArguments args)
		{
			return _container.GetAllInstances(pluginType, args);
		}

		public IEnumerable<T> GetAllInstances<T>(StructureMap.Pipeline.ExplicitArguments args)
		{
			return _container.GetAllInstances<T>(args);
		}

		public System.Collections.IEnumerable GetAllInstances(Type pluginType)
		{
			return _container.GetAllInstances(pluginType);
		}

		public IEnumerable<T> GetAllInstances<T>()
		{
			return _container.GetAllInstances<T>();
		}

		public object GetInstance(Type pluginType, StructureMap.Pipeline.ExplicitArguments args, string instanceKey)
		{
			return _container.GetInstance(pluginType, args, instanceKey);
		}

		public object GetInstance(Type pluginType, StructureMap.Pipeline.ExplicitArguments args)
		{
			return _container.GetInstance(pluginType, args);
		}

		public T GetInstance<T>(StructureMap.Pipeline.ExplicitArguments args, string instanceKey)
		{
			return _container.GetInstance<T>(args, instanceKey);
		}

		public T GetInstance<T>(StructureMap.Pipeline.ExplicitArguments args)
		{
			return _container.GetInstance<T>(args);
		}

		public object GetInstance(Type pluginType, StructureMap.Pipeline.Instance instance)
		{
			return _container.GetInstance(pluginType, instance);
		}

		public object GetInstance(Type pluginType, string instanceKey)
		{
			return _container.GetInstance(pluginType, instanceKey);
		}

		public object GetInstance(Type pluginType)
		{
			return _container.GetInstance(pluginType);
		}

		public T GetInstance<T>(StructureMap.Pipeline.Instance instance)
		{
			return _container.GetInstance<T>(instance);
		}

		public T GetInstance<T>(string instanceKey)
		{
			return _container.GetInstance<T>(instanceKey);
		}

		public T GetInstance<T>()
		{
			return _container.GetInstance<T>();
		}

		public IContainer GetNestedContainer(string profileName)
		{
			throw new NotImplementedException();
		}

		public IContainer GetNestedContainer()
		{
			throw new NotImplementedException();
		}

		public IContainer GetProfile(string profileName)
		{
			throw new NotImplementedException();
		}

		public void Inject(Type pluginType, object instance)
		{
			throw new NotImplementedException();
		}

		public void Inject<T>(T instance) where T : class
		{
			throw new NotImplementedException();
		}

		public StructureMap.Query.IModel Model
		{
			get { throw new NotImplementedException(); }
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string ProfileName
		{
			get { throw new NotImplementedException(); }
		}

		public void Release(object @object)
		{
			throw new NotImplementedException();
		}

		public ContainerRole Role
		{
			get { throw new NotImplementedException(); }
		}

		public StructureMap.Pipeline.ITransientTracking TransientTracking
		{
			get { throw new NotImplementedException(); }
		}

		public object TryGetInstance(Type pluginType, StructureMap.Pipeline.ExplicitArguments args, string instanceKey)
		{
			throw new NotImplementedException();
		}

		public object TryGetInstance(Type pluginType, StructureMap.Pipeline.ExplicitArguments args)
		{
			throw new NotImplementedException();
		}

		public T TryGetInstance<T>(StructureMap.Pipeline.ExplicitArguments args, string instanceKey)
		{
			throw new NotImplementedException();
		}

		public T TryGetInstance<T>(StructureMap.Pipeline.ExplicitArguments args)
		{
			throw new NotImplementedException();
		}

		public object TryGetInstance(Type pluginType, string instanceKey)
		{
			throw new NotImplementedException();
		}

		public object TryGetInstance(Type pluginType)
		{
			throw new NotImplementedException();
		}

		public T TryGetInstance<T>(string instanceKey)
		{
			throw new NotImplementedException();
		}

		public T TryGetInstance<T>()
		{
			throw new NotImplementedException();
		}

		public string WhatDidIScan()
		{
			throw new NotImplementedException();
		}

		public string WhatDoIHave(Type pluginType = null, System.Reflection.Assembly assembly = null, string @namespace = null, string typeName = null)
		{
			throw new NotImplementedException();
		}

		public IExplicitProperty With(string argName)
		{
			throw new NotImplementedException();
		}

		public ExplicitArgsExpression With<T>(T arg)
		{
			throw new NotImplementedException();
		}

		public ExplicitArgsExpression With(Action<IExplicitArgsExpression> action)
		{
			throw new NotImplementedException();
		}

		public ExplicitArgsExpression With(Type pluginType, object arg)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}

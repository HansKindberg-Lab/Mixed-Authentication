using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.InversionOfControl
{
	public class DefaultServiceLocator : IServiceLocator, IServiceProvider
	{
		#region Methods

		public virtual T GetService<T>()
		{
			return (T) this.GetService(typeof(T));
		}

		public virtual T GetService<T>(string key)
		{
			return (T) this.GetService(typeof(T), key);
		}

		public virtual object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException("serviceType");

			return Activator.CreateInstance(serviceType);
		}

		public virtual object GetService(Type serviceType, string key)
		{
			if(key != null)
				throw new NotImplementedException();

			return this.GetService(serviceType);
		}

		public virtual IEnumerable<T> GetServices<T>()
		{
			return this.GetServices(typeof(T)).OfType<T>();
		}

		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return new[] {this.GetService(serviceType)};
		}

		#endregion
	}
}
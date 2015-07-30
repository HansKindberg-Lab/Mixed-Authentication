﻿using System;
using Shared.Abstractions.Extensions;

namespace Shared.Abstractions
{
	public abstract class Wrapper<T> : IWrapper<T>
	{
		#region Fields

		private readonly T _wrappedInstance;

		#endregion

		#region Constructors

		protected Wrapper(T wrappedInstance, string wrappedInstanceParameterName)
		{
			if(Equals(wrappedInstance, null))
				throw new ArgumentNullException(wrappedInstanceParameterName);

			this._wrappedInstance = wrappedInstance;
		}

		#endregion

		#region Properties

		public virtual T WrappedInstance
		{
			get { return this._wrappedInstance; }
		}

		#endregion

		#region Methods

		protected internal virtual T GetWrappedInstance(object potentialWrapper)
		{
			return potentialWrapper.GetWrappedInstance<T>();
		}

		#endregion
	}
}
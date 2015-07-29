using System;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace Shared.Web.Security
{
	public class FormsAuthenticationModuleWrapper : IFormsAuthenticationModule
	{
		#region Fields

		private readonly FormsAuthenticationModule _formsAuthenticationModule;
		private static readonly FieldInfo _formsAuthenticationRequiredField = typeof(FormsAuthenticationModule).GetField("_fAuthRequired", BindingFlags.NonPublic | BindingFlags.Static);
		private Action<object, EventArgs> _onEnterAction;
		private static readonly MethodInfo _onEnterMethod = typeof(FormsAuthenticationModule).GetMethod("OnEnter", BindingFlags.Instance | BindingFlags.NonPublic);
		private Action<object, EventArgs> _onLeaveAction;
		private static readonly MethodInfo _onLeaveMethod = typeof(FormsAuthenticationModule).GetMethod("OnLeave", BindingFlags.Instance | BindingFlags.NonPublic);

		#endregion

		#region Constructors

		public FormsAuthenticationModuleWrapper(FormsAuthenticationModule formsAuthenticationModule)
		{
			if(formsAuthenticationModule == null)
				throw new ArgumentNullException("formsAuthenticationModule");

			this._formsAuthenticationModule = formsAuthenticationModule;
			this._formsAuthenticationModule.Authenticate += this.OnFormsAuthenticationModuleAuthenticate;
		}

		#endregion

		#region Events

		public event EventHandler<FormsAuthenticationEventArgs> Authenticate;

		#endregion

		#region Properties

		protected internal virtual FormsAuthenticationModule FormsAuthenticationModule
		{
			get { return this._formsAuthenticationModule; }
		}

		public virtual bool FormsAuthenticationRequired
		{
			get { return (bool) _formsAuthenticationRequiredField.GetValue(this.FormsAuthenticationModule); }
			set { _formsAuthenticationRequiredField.SetValue(this.FormsAuthenticationModule, value); }
		}

		protected internal virtual Action<object, EventArgs> OnEnterAction
		{
			get { return this._onEnterAction ?? (this._onEnterAction = (Action<object, EventArgs>) Delegate.CreateDelegate(typeof(Action<object, EventArgs>), this.FormsAuthenticationModule, _onEnterMethod)); }
		}

		protected internal virtual Action<object, EventArgs> OnLeaveAction
		{
			get { return this._onLeaveAction ?? (this._onLeaveAction = (Action<object, EventArgs>) Delegate.CreateDelegate(typeof(Action<object, EventArgs>), this.FormsAuthenticationModule, _onLeaveMethod)); }
		}

		#endregion

		#region Methods

		public virtual void Dispose()
		{
			this.FormsAuthenticationModule.Dispose();
		}

		public virtual void Init(HttpApplication context)
		{
			this.FormsAuthenticationModule.Init(context);
		}

		public virtual void OnAuthenticateRequest(object sender, EventArgs e)
		{
			this.OnEnterAction(sender, e);
		}

		public virtual void OnEndRequest(object sender, EventArgs e)
		{
			this.OnLeaveAction(sender, e);
		}

		protected internal virtual void OnFormsAuthenticationModuleAuthenticate(object sender, FormsAuthenticationEventArgs e)
		{
			if(this.Authenticate != null)
				this.Authenticate(sender, e);
		}

		#endregion
	}
}
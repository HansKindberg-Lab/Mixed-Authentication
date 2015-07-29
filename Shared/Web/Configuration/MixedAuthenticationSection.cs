using System.Configuration;

namespace Shared.Web.Configuration
{
	public class MixedAuthenticationSection : ConfigurationSection
	{
		#region Fields

		private const string _disableFormsAuthenticationPropertyName = "disableFormsAuthentication";

		#endregion

		#region Properties

		[ConfigurationProperty(_disableFormsAuthenticationPropertyName, DefaultValue = false)]
		public virtual bool DisableFormsAuthentication
		{
			get { return (bool) this[_disableFormsAuthenticationPropertyName]; }
			set { this[_disableFormsAuthenticationPropertyName] = value; }
		}

		#endregion
	}
}
using System.Configuration;

namespace Shared.Web.Configuration
{
	public class MixedAuthenticationSection : ConfigurationSection
	{
		#region Fields

		private const string _formsAuthenticationDisabledPropertyName = "formsAuthenticationDisabled";

		#endregion

		#region Properties

		[ConfigurationProperty(_formsAuthenticationDisabledPropertyName, DefaultValue = false)]
		public virtual bool FormsAuthenticationDisabled
		{
			get { return (bool) this[_formsAuthenticationDisabledPropertyName]; }
			set { this[_formsAuthenticationDisabledPropertyName] = value; }
		}

		#endregion
	}
}
using System.Configuration;

namespace Shared.Configuration
{
	public class ConfigurationManagerWrapper : IConfigurationManager
	{
		#region Methods

		public virtual object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		#endregion
	}
}
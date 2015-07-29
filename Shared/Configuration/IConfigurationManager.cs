namespace Shared.Configuration
{
	public interface IConfigurationManager
	{
		#region Methods

		object GetSection(string sectionName);

		#endregion
	}
}
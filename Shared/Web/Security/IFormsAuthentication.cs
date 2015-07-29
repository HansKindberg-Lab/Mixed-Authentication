namespace Shared.Web.Security
{
	public interface IFormsAuthentication
	{
		#region Properties

		bool Enabled { get; }

		#endregion

		#region Methods

		void Initialize();

		#endregion
	}
}
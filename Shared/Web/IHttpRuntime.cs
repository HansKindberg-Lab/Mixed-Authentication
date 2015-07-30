namespace Shared.Web
{
	public interface IHttpRuntime
	{
		#region Properties

		bool UsingIntegratedPipeline { get; }

		#endregion
	}
}
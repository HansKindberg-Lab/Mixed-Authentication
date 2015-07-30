namespace Shared.Abstractions.Extensions
{
	public interface IWrapperExtension
	{
		#region Methods

		T GetWrappedInstance<T>(object potentialWrapper);

		#endregion
	}
}
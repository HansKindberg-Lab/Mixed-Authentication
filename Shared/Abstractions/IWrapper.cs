namespace Shared.Abstractions
{
	public interface IWrapper<out T>
	{
		#region Properties

		T WrappedInstance { get; }

		#endregion
	}
}
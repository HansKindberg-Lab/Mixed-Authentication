using System;

namespace Shared.Extensions
{
	public static class UriExtension
	{
		#region Methods

		public static string PathAndQueryAndFragment(this Uri uri)
		{
			if(uri == null)
				throw new ArgumentNullException("uri");

			return uri.PathAndQuery + uri.Fragment;
		}

		#endregion
	}
}
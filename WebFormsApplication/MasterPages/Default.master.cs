using System;
using System.Web.UI;

namespace WebFormsApplication.MasterPages
{
	public partial class Default : MasterPage
	{
		#region Methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.HeaderPlaceHolder.DataBind();

			if(this.ModuleRepeater != null)
				this.ModuleRepeater.DataBind();
		}

		#endregion
	}
}
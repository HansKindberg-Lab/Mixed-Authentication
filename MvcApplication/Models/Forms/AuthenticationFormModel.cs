using System.ComponentModel.DataAnnotations;

namespace MvcApplication.Models.Forms
{
	public class AuthenticationFormModel
	{
		#region Properties

		[Display(Name = "Password", Order = 20, Prompt = "Enter your password.")]
		public virtual string Password { get; set; }

		[Display(Name = "Remember me", Order = 30)]
		public virtual bool Persist { get; set; }

		[Display(Name = "User-name", Order = 10, Prompt = "Enter your user-name.")]
		public virtual string UserName { get; set; }

		#endregion
	}
}
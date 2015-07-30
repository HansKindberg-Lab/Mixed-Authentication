<%@ Page AutoEventWireup="false" CodeBehind="SignIn.aspx.cs" Inherits="WebFormsApplication.Util.SignIn" Language="C#" MasterPageFile="~/MasterPages/Default.master" Title="Sign in" %>
<%@ Import Namespace="Shared" %><asp:Content ContentPlaceHolderID="InformationContentPlaceHolder" runat="server" />
<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<asp:PlaceHolder id="UserInformationPlaceHolder" Visible="<%# this.User.Identity.IsAuthenticated %>" runat="server">
		<div class="system-information warning">
			<h2>Already signed in</h2>
			<p>You are already signed in as <%# this.User.Identity.Name %></p>
		</div>
	</asp:PlaceHolder>
	<asp:PlaceHolder id="WindowsSignInPlaceHolder" Visible="<%# !this.User.Identity.IsAuthenticated %>" runat="server">
		<h2>Windows sign in</h2>
		<p><a href="<%# this.WindowsSignInUrl.PathAndQuery %>">Sign in as windows-user.</a></p>
	</asp:PlaceHolder>
	<h2>Forms sign in</h2>
	<p>You do not have to enter any credentials. Just click sign in and you will be signed in as "<%= Global.TheUserName %>". If you check "Remember me" the cookie will be persisted.</p>
	<form runat="server">
		<fieldset>
			<div>
				<asp:Label AssociatedControlID="UserNameTextBox" runat="server">User-name</asp:Label>
				<asp:TextBox id="UserNameTextBox" runat="server" />
			</div>
			<div>
				<asp:Label AssociatedControlID="PasswordTextBox" runat="server">Password</asp:Label>
				<asp:TextBox id="PasswordTextBox" TextMode="Password" runat="server" />
			</div>
			<div>
				<asp:CheckBox id="PersistCheckBox" runat="server" />
				<asp:Label AssociatedControlID="PersistCheckBox" CssClass="display-inline" runat="server">Remember me</asp:Label>
			</div>
			<div>
				<asp:Button OnClick="OnSignInButtonClick" Text="Sign in" runat="server" />
			</div>
		</fieldset>
	</form>
</asp:Content>
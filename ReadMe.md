# Mixed-Authentication
Solution for laborating with forms-authentication together with windows-authentication.
The idea comes from **Mike Volodarsky** - [**IIS 7.0 Two-Level Authentication with Forms Authentication and Windows Authentication**](http://mvolo.com/iis-70-twolevel-authentication-with-forms-authentication-and-windows-authentication/). The links to the source in that article does not work anymore.

## Configuration-example for a MVC-web-application

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
		<configSections>
			<section name="mixedAuthentication" type="Shared.Web.Configuration.MixedAuthenticationSection, Shared" />
		</configSections>
		<location>
			<system.web>
				<authentication mode="Forms">
					<forms name="MvcApplication-SignIn" loginUrl="~/Authentication/" timeout="120" />
				</authentication>
				<compilation debug="true" targetFramework="4.5" />
				<httpRuntime targetFramework="4.5" />
				<roleManager defaultProvider="RoleProvider" enabled="true">
					<providers>
						<add name ="RoleProvider" type="Shared.Web.Security.RoleProvider, Shared" />
					</providers>
				</roleManager>
			</system.web>
			<system.webServer>
				<modules>
					<add name="MixedAuthentication" type="Shared.Web.Security.MixedAuthenticationModule, Shared" />
					<remove name="FormsAuthentication" />
				</modules>
				<!-- IT IS IMPORTANT TO HAVE THIS IN THE ROOT-LOCATION: -->
				<security>
					<authentication>
						<anonymousAuthentication enabled="true" />
						<windowsAuthentication enabled="false" />
					</authentication>
				</security>
			</system.webServer>
		</location>
		<location path="Administration">
			<system.web>
				<authorization>
					<allow roles="Administrators" />
					<deny users="*" />
				</authorization>
			</system.web>
		</location>
		<location path="Authentication/SignInWithWindowsIdentity">
			<mixedAuthentication formsAuthenticationDisabled="true" />
			<system.webServer>
				<security>
					<authentication>
						<anonymousAuthentication enabled="false" />
						<windowsAuthentication enabled="true" />
					</authentication>
				</security>
			</system.webServer>
		</location>
		<runtime>
			<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
				<dependentAssembly>
					<assemblyIdentity name="System.Web.Helpers" publicKeyToken="31bf3856ad364e35"/>
					<bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0"/>
				</dependentAssembly>
				<dependentAssembly>
					<assemblyIdentity name="System.Web.WebPages" publicKeyToken="31bf3856ad364e35"/>
					<bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0"/>
				</dependentAssembly>
				<dependentAssembly>
					<assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35"/>
					<bindingRedirect oldVersion="1.0.0.0-5.2.3.0" newVersion="5.2.3.0"/>
				</dependentAssembly>
			</assemblyBinding>
		</runtime>
	</configuration>

## Configuration-example for a WebForms-web-application

	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
		<configSections>
			<section name="mixedAuthentication" type="Shared.Web.Configuration.MixedAuthenticationSection, Shared" />
		</configSections>
		<location>
			<system.web>
				<authentication mode="Forms">
					<forms name="WebFormsApplication-SignIn" loginUrl="~/Util/SignIn.aspx" timeout="120" />
				</authentication>
				<compilation debug="true" targetFramework="4.5" />
				<httpRuntime targetFramework="4.5" />
				<roleManager defaultProvider="RoleProvider" enabled="true">
					<providers>
						<add name ="RoleProvider" type="Shared.Web.Security.RoleProvider, Shared" />
					</providers>
				</roleManager>
			</system.web>
			<system.webServer>
				<modules>
					<add name="MixedAuthentication" type="Shared.Web.Security.MixedAuthenticationModule, Shared" />
					<remove name="FormsAuthentication" />
				</modules>
				<!-- IT IS IMPORTANT TO HAVE THIS IN THE ROOT-LOCATION: -->
				<security>
					<authentication>
						<anonymousAuthentication enabled="true" />
						<windowsAuthentication enabled="false" />
					</authentication>
				</security>
			</system.webServer>
		</location>
		<location path="Administration">
			<system.web>
				<authorization>
					<allow roles="Administrators" />
					<deny users="*" />
				</authorization>
			</system.web>
		</location>
		<location path="Util/WindowsSignIn.aspx">
			<mixedAuthentication formsAuthenticationDisabled="true" />
			<system.webServer>
				<security>
					<authentication>
						<anonymousAuthentication enabled="false" />
						<windowsAuthentication enabled="true" />
					</authentication>
				</security>
			</system.webServer>
		</location>
	</configuration>
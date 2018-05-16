#define SingleTenantApp
using System;

//The following namespace was added to this sample.
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Microsoft.Owin.Security;

//The following namespace was defined and added to this sample.
using GCCSI_CO2RE.Utils;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace GCCSI_CO2RE
{
    public partial class Startup
    {
        /// <summary>
        /// Configures OpenIDConnect Authentication & Adds Custom Application Authorization Logic on User Login.
        /// </summary>
        /// <param name="app">The application represented by a <see cref="IAppBuilder"/> object.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            try
            {
                Console.WriteLine("SetDefaultSignInAsAuthenticationType -- Cookies");
                app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

                app.UseCookieAuthentication(new CookieAuthenticationOptions());

                Console.WriteLine("Configure OpenIDConnect, register callbacks for OpenIDConnect Notifications");
                //Configure OpenIDConnect, register callbacks for OpenIDConnect Notifications
                app.UseOpenIdConnectAuthentication(
                    new OpenIdConnectAuthenticationOptions
                    {
                        Scope = "openid",
                        ClientId = ConfigHelper.ClientId,
                        ClientSecret = ConfigHelper.AppKey,
                        Resource = "https://analysis.windows.net/powerbi/api",

#if SingleTenantApp
                    Authority = String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, ConfigHelper.Tenant), // For Single-Tenant
                                                                                                                            //Authority = ConfigHelper.AadInstance, // For Single-Tenant
#else
                    Authority = ConfigHelper.CommonAuthority, // For Multi-Tenant
#endif
                    PostLogoutRedirectUri = ConfigHelper.PostLogoutRedirectUri,

                    // Here, we've disabled issuer validation for the multi-tenant sample.  This enables users
                    // from ANY tenant to sign into the application (solely for the purposes of allowing the sample
                    // to be run out-of-the-box.  For a real multi-tenant app, reference the issuer validation in 
                    // WebApp-MultiTenant-OpenIDConnect-DotNet.  If you're running this sample as a single-tenant
                    // app, you can delete the ValidateIssuer property below.
                    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                        {
#if !SingleTenantApp
                        ValidateIssuer = false, // For Multi-Tenant Only
#endif
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                            SaveSigninToken = true,

                        },

                        Notifications = new OpenIdConnectAuthenticationNotifications
                        {

                            AuthorizationCodeReceived = async e => {
                                var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, ConfigHelper.Tenant));
                                var result =
                                    await authContext.AcquireTokenByAuthorizationCodeAsync(
                                         e.ProtocolMessage.Code,
                                         new Uri(ConfigHelper.PostLogoutRedirectUri),
                                         new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey));

                            //Add the token into the current identity.
                            var ci = e.AuthenticationTicket.Identity;
                                ci.AddClaim(new Claim("powerBIToken", result.AccessToken));
                            },

                            AuthenticationFailed = context =>
                            {
                                context.HandleResponse();
                                context.Response.Redirect("/Error/ShowError?signIn=true&errorMessage=" + context.Exception.Message);
                                System.Diagnostics.Trace.WriteLine("ERROR : " + context.Exception.Message);
                                System.Diagnostics.Trace.WriteLine("ERROR : " + context.Exception.StackTrace);
                                return Task.FromResult(0);
                            }
                        }
                    });
            }
            catch(Exception e)
            {
                System.Diagnostics.Trace.WriteLine("ERROR : " + e.Message);
                Console.WriteLine("ERROR : " + e.Message);
            }
            

            //Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext =
            //new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, ConfigHelper.Tenant));
            //var result = authContext.AcquireToken("https://mysecrettenant.onmicrosoft.com/backendAPI", credential, userAssertion);

        }
    }
}
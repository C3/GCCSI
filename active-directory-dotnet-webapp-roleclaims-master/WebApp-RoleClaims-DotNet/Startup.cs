using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using GCCSI_CO2RE.Utils;

[assembly: OwinStartup(typeof(GCCSI_CO2RE.Startup))]

namespace GCCSI_CO2RE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                System.Diagnostics.Trace.WriteLine("Configuring the application......");
                Console.WriteLine("Configuring the application......");
                ConfigureAuth(app);
                System.Diagnostics.Trace.WriteLine("Application configured.");
                Console.WriteLine("Application configured.");
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message);
                Console.WriteLine(e.Message);
            }
            
        }
    }
}

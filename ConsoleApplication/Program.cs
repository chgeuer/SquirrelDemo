using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running");

            using (var updateManager = new UpdateManager(
                urlOrPath: @"C:\Users\chgeuer\github\chgeuer\SquirrelDemo\ConsoleApplication\Releases",
                applicationName: "ConsoleApplication", 
                appFrameworkVersion: FrameworkVersion.Net45))
            {
                var updateInfo = updateManager.CheckForUpdate().Result;
                if (updateInfo == null)
                {
                    Console.WriteLine("No updates found");
                }
                else if (!updateInfo.ReleasesToApply.Any())
                {
                    Console.WriteLine("You're up to date! " + updateInfo.CurrentlyInstalledVersion.EntryAsString + 
                        " " + updateInfo.CurrentlyInstalledVersion.Version.MinorRevision);
                }
                else
                {
                    try
                    {
                        updateManager.UpdateApp(p => Console.WriteLine(p)).Wait();
                    }
                    catch (AggregateException ae)
                    {
                        Console.WriteLine(ae.InnerException.Message);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}

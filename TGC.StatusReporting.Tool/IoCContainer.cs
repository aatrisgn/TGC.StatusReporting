using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.WIQLQueryBuilder;

namespace TGC.StatusReporting.Tool
{
    class IoCContainer
    {
        public static IServiceProvider CreateIoC()
        {
            var collection = new ServiceCollection();

            collection.AddAzureServices();

            return collection.BuildServiceProvider();
        }
    }
}

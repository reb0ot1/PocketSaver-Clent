using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneySaver.Client.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySaver.Client.HostedServices
{
    public class TestHosterService : BackgroundService
    {
        public TestHosterService(IServiceProvider serviceProvider)
        {
            this.Services = serviceProvider;
        }

        IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoTheWork();
        }

        private async Task DoTheWork()
        {
            using (var scope = this.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IResultService>();

                await service.GetResultsAsync();
            }
        }
    }
}

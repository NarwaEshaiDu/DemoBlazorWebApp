﻿using MassTransit;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Blazor2App.ServiceBus
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;
        public Worker(IBus bus)
        {
            _bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Hello
                {
                    Name = "test"
                }, stoppingToken);

                Log.Logger.Error("hi");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}

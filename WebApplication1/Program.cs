//using System.Diagnostics;
//using MassTransit;
//using MassTransit.Metadata;
//using Microsoft.EntityFrameworkCore;


//var host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices((hostContext, services) =>
//    {
//        services.AddDbContext<RegistrationDbContext>(x =>
//        {
//            var connectionString = hostContext.Configuration.GetConnectionString("Default");

//            x.UseSqlServer(connectionString, options =>
//            {
//                options.MinBatchSize(1);
//            });
//        });

      

//        services.AddScoped<IRegistrationValidationService, RegistrationValidationService>();
//        services.AddMassTransit(x =>
//        {
//            x.AddEntityFrameworkOutbox<RegistrationDbContext>(o =>
//            {
//                o.UsePostgres();

//                o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
//            });

//            x.SetKebabCaseEndpointNameFormatter();

//            x.AddConsumer<NotifyRegistrationConsumer>();
//            x.AddConsumer<SendRegistrationEmailConsumer>();
//            x.AddConsumer<AddEventAttendeeConsumer>();
//            x.AddConsumer<ValidateRegistrationConsumer, ValidateRegistrationConsumerDefinition>();
//            x.AddSagaStateMachine<RegistrationStateMachine, RegistrationState, RegistrationStateDefinition>()
//                .EntityFrameworkRepository(r =>
//                {
//                    r.ExistingDbContext<RegistrationDbContext>();
//                    r.UsePostgres();
//                });

//            x.us((context, cfg) =>
//            {
//                cfg.ConfigureEndpoints(context);
//            });
//        });
//    })
//    .UseSerilog()
//    .Build();

//await host.RunAsync();
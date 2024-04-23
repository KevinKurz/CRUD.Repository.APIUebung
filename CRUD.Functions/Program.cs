using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD.Core.ReservationService;
using CRUD.Core.TableService;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IReservationRepository, ReservationRepository>();
        services.AddSingleton<ITableRepository, TableRepository>();
    })
    .Build();

host.Run();

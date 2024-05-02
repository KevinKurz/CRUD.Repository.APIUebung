using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD.Core.ReservationService;
using CRUD.Core.TableService;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataBank;
using CRUD.Core;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IReservationRepository<IReservationDto>, ReservationRepository>();
        services.AddSingleton<ITableRepository<ITableDto>, TableRepository>();
        services.AddSingleton<JsonService>();
        services.AddSingleton<QueryValidator>();
        services.AddSingleton<JsonBank>();
    })
    .Build();

host.Run();

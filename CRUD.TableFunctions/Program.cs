using CRUD.DataStructures.ReservationDTO;
using CRUD.Interface;
using CRUD.Repository;
using CRUD_Reservation_ClassLibrary;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddScoped<ReservationRepository>();
    })
    .Build();

host.Run();

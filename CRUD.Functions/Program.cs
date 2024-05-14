using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.Core.Interfaces;
using CRUD.Core.Repositories;
using CRUD.Core.QueryParams;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IRepository<IReservationDto, QueryParameter, ReservationOptionsParameter>, ReservationRepository>();
        services.AddSingleton<IRepository<ITableDto, QueryParameter, TableOptionsParameter>, TableRepository>();
        services.AddSingleton<IDataService<IModel>, DataService>();
        services.AddSingleton<JsonDataBank>();
    })
    .Build();

host.Run();
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataBank;
using CRUD.Core;
using CRUD.DataStructures.DataModel;
using CRUD.Core.Interfaces;
using CRUD.Core.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IReservationRepository<IReservationDto>, ReservationRepository>(); //Included in Reservationfunctions
        services.AddSingleton<ITableRepository<ITableDto, IQueryParams>, TableRepository>(); //Included in TableFunctions
        services.AddSingleton<IDataService<IModel>, DataService>(); //Included in CORE Project
        services.AddSingleton<PathValidator>(); //Included in Table and Reservationsfunctions
        services.AddSingleton<JsonDataBank>(); //Included in JsonService
    })
    .Build();

host.Run();

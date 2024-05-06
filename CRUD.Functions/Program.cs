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

        services.AddSingleton<IReservationRepository<IReservationDto>, ReservationRepository>(); //Included in Reservationfunctions
        services.AddSingleton<ITableRepository<ITableDto>, TableRepository>(); //Included in TableFunctions
        services.AddSingleton<IDataService<DataService>>(); //Included in CORE Project
        services.AddSingleton<QueryValidator>(); //Included in Table and Reservationsfunctions
        services.AddSingleton<JsonDataBank>(); //Included in JsonService
    })
    .Build();

host.Run();

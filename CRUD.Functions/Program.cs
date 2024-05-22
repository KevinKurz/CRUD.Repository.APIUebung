using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD.Repository.Interfaces;
using CRUD.Repository.Repositories;
using CRUD.Contracts.QueryParams;
using CRUD.Core.FilterService;
using CRUD.Core.DataService;
using CRUD.Core;
using CRUD.Contracts.Queries.ReservationQuery;
using CRUD.Contracts.Queries.TableQuery;
using CRUD.Contracts.DTOs.ReservationDto;
using CRUD.Contracts.DTOs.TableDto;
using CRUD.Core.DataModelService;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<IRepository<IReservationDto, IReservationQuery, QueryParameter, OptionsParameter>, ReservationRepository>();
        services.AddSingleton<IRepository<ITableDto, ITableQuery, QueryParameter, OptionsParameter>, TableRepository>();
        services.AddSingleton<IDataService<IModel>, DataService>();
        services.AddSingleton<PathValidator>();
        services.AddSingleton<TableFilterService>();
        services.AddSingleton<ReservationFilterService>();
    })
    .Build();

host.Run();
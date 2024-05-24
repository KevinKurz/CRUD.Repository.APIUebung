using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using Newtonsoft.Json;
using CRUD.Contracts.AttributeService;
using AuthorizationLevel = Microsoft.Azure.Functions.Worker.AuthorizationLevel;
using CRUD.Contracts.QueryParams;
using CRUD.Contracts.DTOs.TableDto;
using CRUD.Contracts.Queries.TableQuery;
using CRUD.Repository.Interfaces;

namespace CRUD.Functions
{
    public class TableFunctions
    {
        private readonly IRepository<ITableDto, ITableQuery, QueryParameter, OptionsParameter> _tableInterface;
        public TableFunctions(IRepository<ITableDto, ITableQuery, QueryParameter, OptionsParameter> tableInterface)
        {
            _tableInterface = tableInterface;
        }

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("GET_All_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<TableDto>), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult GetAllTables([HttpTrigger(AuthorizationLevel.Function, "get", Route = "tables")] HttpRequest req)
        {
            try
            {
                OptionsParameter optionsParameter = new OptionsParameter(req.Query["filter"], req.Query["sortBy"]);
                QueryParameter queryParameter = new QueryParameter();

                List<TableDto> response = (List<TableDto>)_tableInterface.GetAll(queryParameter, optionsParameter);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Get table by ID
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("GET_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TableDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult GetSingleTable([HttpTrigger(AuthorizationLevel.Function, "get", Route = "tables/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                OptionsParameter optionsParameter = new OptionsParameter(req.Query["filter"], req.Query["sortBy"]);
                QueryParameter queryParameter = new QueryParameter(tableId);

                TableDto response = (TableDto)_tableInterface.GetById(queryParameter, optionsParameter);
                return new OkObjectResult(response);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete all tables except for the first four basetables
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("DELETE_All_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult DeleteAllTables([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "tables")] HttpRequest req)
        {
            try
            {
                _tableInterface.DeleteAll();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete table by ID except the first foer basetables
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("DELETE_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult DeleteSingleTable([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "tables/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                QueryParameter queryParameter = new QueryParameter(tableId);

                _tableInterface.DeleteById(queryParameter);

                return new OkResult();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Create Table by requestBody
        /// </summary>
        /// <returns>
        /// <see cref="StatusCodes.Status201Created"/><br/>
        /// <see cref="StatusCodes.Status400BadRequest"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("POST_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateTableQuery), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> CreateTable([HttpTrigger(AuthorizationLevel.Function, "post", Route = "tables")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if(string.IsNullOrEmpty(requestBody))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                else
                {
                    CreateTableQuery tableQuery = JsonConvert.DeserializeObject<CreateTableQuery>(requestBody);

                    tableQuery.IsValid();
                    _tableInterface.Create(tableQuery);

                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
            }
            catch (NotImplementedException ex)
            { 
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Update table by ID and requestbody except for the first four base tables
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="StatusCodes.Status400BadRequest"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("PUT_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateTableQuery), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> UpdateTable([HttpTrigger(AuthorizationLevel.Function, "put", Route = "tables/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                else
                {
                    QueryParameter queryParameter = new QueryParameter(tableId);
                    UpdateTableQuery tableQuery = JsonConvert.DeserializeObject<UpdateTableQuery>(requestBody);

                    tableQuery.IsValid();

                    _tableInterface.UpdateById(queryParameter, tableQuery); //UpdateWithId, wenn man die Id als Parameter übergibt 

                    return new OkResult();
                }
            }
            catch (NotImplementedException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

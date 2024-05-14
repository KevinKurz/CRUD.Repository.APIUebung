using CRUD.DataStructures.DTOs.TableDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using Newtonsoft.Json;
using CRUD.DataStructures.AttributeService;
using CRUD.Core.QueryParams;
using AuthorizationLevel = Microsoft.Azure.Functions.Worker.AuthorizationLevel;
using CRUD.Core.Interfaces;

namespace CRUD.Functions
{
    public class TableFunctions
    {
        private readonly IRepository<ITableDto, QueryParameter, TableOptionsParameter> _tableInterface;
        public TableFunctions(IRepository<ITableDto, QueryParameter, TableOptionsParameter> tableInterface)
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
                TableOptionsParameter optionsParameter = new TableOptionsParameter(req.Query["capacity"], req.Query["name"], req.Query["availability"]);
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
                TableOptionsParameter optionsParameter = new TableOptionsParameter(req.Query["capacity"], req.Query["name"], req.Query["availability"]);
                QueryParameter queryParameter = new QueryParameter(tableId);

                TableDto response = (TableDto)_tableInterface.GetById(queryParameter, optionsParameter);
                return new OkObjectResult(response);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new BadRequestObjectResult(ex.Message);
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
                QueryParameter pathParameter = new QueryParameter(tableId);

                _tableInterface.DeleteById(pathParameter);

                return new OkResult();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new BadRequestObjectResult(ex.Message);
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
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateTableDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
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
                    CreateTableDto tableDto = JsonConvert.DeserializeObject<CreateTableDto>(requestBody);

                    tableDto.IsValid();
                    _tableInterface.Create(tableDto);

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
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateTableDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
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
                    UpdateTableDto tableDto = JsonConvert.DeserializeObject<UpdateTableDto>(requestBody);

                    tableDto.IsValid();

                    _tableInterface.UpdateById(queryParameter, tableDto);

                    return new OkResult();
                }
            }
            catch (NotImplementedException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

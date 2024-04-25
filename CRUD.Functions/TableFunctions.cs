using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.Core.TableService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using Newtonsoft.Json;
using CRUD.DataStructures.AttributeService;
using System.Linq;

namespace CRUD.Functions
{
    public class TableFunctions
    {
        private readonly ITableRepository<ITableDto> _tableInterface;

        public TableFunctions(ITableRepository<ITableDto> tableRepository)
        {
            _tableInterface = tableRepository;
        }

        // ------------------------------------------------------------------
        // Get All Tables
        // ------------------------------------------------------------------
        [Function("GET_All_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<TableDto>), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> GetAllTables([HttpTrigger(AuthorizationLevel.Function, "get", Route = "tables")] HttpRequest req)
        {
            try
            {
                List<TableDto> response = (List<TableDto>)_tableInterface.GetAll();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // ------------------------------------------------------------------
        // Get Single Table
        // ------------------------------------------------------------------
        [Function("GET_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TableDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> GetSingleTable([HttpTrigger(AuthorizationLevel.Function, "get", Route = "tables/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                _tableInterface.IsRequestQueryValide(tableId);
                TableDto response = (TableDto)_tableInterface.GetById(tableId);
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

        // ------------------------------------------------------------------
        // Delete All Tables
        // ------------------------------------------------------------------
        [Function("DELETE_All_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> DeleteAllTables([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "tables")] HttpRequest req)
        {
            try
            {
                _tableInterface.DeleteAll();
                return new OkResult();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Something unexpected happend");
            }
        }

        // ------------------------------------------------------------------
        // Delete Single Table
        // ------------------------------------------------------------------
        [Function("DELETE_Table")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Table" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> DeleteSingleTable([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "tables/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                _tableInterface.IsRequestQueryValide(tableId);
                _tableInterface.DeleteById(tableId);

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

        // ------------------------------------------------------------------
        // Create Table
        // ------------------------------------------------------------------
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
                CreateTableDto tableDto = JsonConvert.DeserializeObject<CreateTableDto>(requestBody);

                tableDto.IsValid();
                _tableInterface.Create(tableDto);

                return new StatusCodeResult(StatusCodes.Status201Created);
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

        // ------------------------------------------------------------------
        // Update Table
        // ------------------------------------------------------------------
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
                UpdateTableDto tableDto = JsonConvert.DeserializeObject<UpdateTableDto>(requestBody);

                tableDto.IsValid();

                _tableInterface.IsRequestQueryValide(tableId);
                _tableInterface.UpdateById(tableDto, tableId);

                return new OkResult();
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

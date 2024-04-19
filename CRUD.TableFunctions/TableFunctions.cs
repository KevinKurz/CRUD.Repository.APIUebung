using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using HttpTriggerAttribute = Microsoft.Azure.Functions.Worker.HttpTriggerAttribute;
using AuthorizationLevel = Microsoft.Azure.Functions.Worker.AuthorizationLevel;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;
using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.AttributeService;
using CRUD.Core.ReservationService;

namespace CRUD.TableFunctions
{
    public class TableFunctions
    {
        private readonly IReservationRepository _reservationRepository;
        public TableFunctions(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // ------------------------------------------------------------------
        // Post Reservation
        // ------------------------------------------------------------------
        [Function("POST_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateReservationDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.ResetContent, Description = "Paremeters were correct, but table is already occupied")]
        public async Task<IActionResult> PostReservation([HttpTrigger(AuthorizationLevel.Function, "post", Route = "reservations")] HttpRequest req)
        {   
            try
            {
                CreateReservationDto? reservationDto = new CreateReservationDto();
                
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                reservationDto = JsonConvert.DeserializeObject<CreateReservationDto>(requestBody);

                reservationDto.IsValid();

                if (_reservationRepository.Create(reservationDto) == true)
                {
                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Parameter. Your times collides with already existing times.");
                }
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // ------------------------------------------------------------------
        // Get All Reservations
        // ------------------------------------------------------------------
        [Function("GET_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetTableDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> GetAllReservations([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations")] HttpRequest req)
        {
            try
            {
                List<GetTableDto> response = _reservationRepository.GetAll();
                return new OkObjectResult(response);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Something unexpected happend");
            }
        }

        // ------------------------------------------------------------------
        // Get Single Reservation
        // ------------------------------------------------------------------
        [Function("GET_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetReservationDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        public async Task<IActionResult> GetReservation([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                _reservationRepository.IsRequestQueryValide(tableId, reservationId);
                GetReservationDto response = _reservationRepository.GetById(tableId, reservationId);
                return new OkObjectResult(response);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Your IDs are out of range or smaller than 0.");
            }
        }

        // ------------------------------------------------------------------
        // Delete All Reservations
        // ------------------------------------------------------------------
        [Function("DELETE_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> DeleteAllReservations([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "reservations")] HttpRequest req)
        {
            try
            {
                _reservationRepository.DeleteAll();
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Something unexpected happend");
            }
        }

        // ------------------------------------------------------------------
        // Delete Single Reservation
        // ------------------------------------------------------------------
        [Function("DELETE_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        public async Task<IActionResult> DeleteReservation([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                _reservationRepository.IsRequestQueryValide(tableId, reservationId);
                _reservationRepository.DeleteById(tableId, reservationId);
                return new OkResult();
            }
            catch (ArgumentOutOfRangeException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Your IDs are out of range or smaller than 0.");
            }
        }

        // ------------------------------------------------------------------
        // Put Reservation
        // ------------------------------------------------------------------
        [Function("PUT_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateReservationDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.ResetContent, Description = "Paremeters were correct, but table is already occupied")]
        public async Task<IActionResult> PutReservation([HttpTrigger(AuthorizationLevel.Function, "put", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                UpdateReservationDto reservationDto = new UpdateReservationDto();

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                reservationDto = JsonConvert.DeserializeObject<UpdateReservationDto>(requestBody);

                _reservationRepository.IsRequestQueryValide(tableId, reservationId);
                reservationDto.IsValid();

                if (_reservationRepository.UpdateById(tableId, reservationId, reservationDto) == true)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Parameter. Your times collides with already existing times.");
                }
            }
            catch (ValidationException ex)
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

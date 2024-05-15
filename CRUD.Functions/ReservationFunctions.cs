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
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.AttributeService;
using CRUD.Core.Interfaces;
using CRUD.Core.QueryParams;

namespace CRUD.Functions
{
    public class ReservationFunctions
    {
        private readonly IRepository<IReservationDto, QueryParameter, OptionsParameter> _reservationInterface;
        public ReservationFunctions(IRepository<IReservationDto, QueryParameter, OptionsParameter> reservationInterface)
        {
            _reservationInterface = reservationInterface;
        }

        /// <summary>
        /// Post a reservationy by requestBody
        /// </summary>
        /// <param name="req"></param>
        /// <returns>
        /// <see cref="StatusCodes.Status201Created"/><br/>
        /// <see cref="StatusCodes.Status400BadRequest"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
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
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                else
                {
                    CreateReservationDto reservationDto = JsonConvert.DeserializeObject<CreateReservationDto>(requestBody);
                    reservationDto.IsValid();
                    _reservationInterface.Create(reservationDto);

                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
            }
            catch (NotImplementedException ex)
            {
                return new BadRequestObjectResult(ex.Message);
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

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>
        /// <see cref="OkObjectResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("GET_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ReservationDto>), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult GetAllReservations([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations/{tableId}")] HttpRequest req, int tableId)
        {
            try
            {
                QueryParameter queryParameter = new QueryParameter(tableId);
                OptionsParameter optionsParameter = new OptionsParameter(req.Query["filter"], req.Query["sortBy"]);

                List<ReservationDto> response = (List<ReservationDto>)_reservationInterface.GetAll(queryParameter, optionsParameter);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Get reservation by ID
        /// </summary>
        /// <returns>
        /// <see cref="OkObjectResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("GET_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ReservationDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        public IActionResult GetReservation([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                OptionsParameter optionsParameter = new OptionsParameter(req.Query["filter"], req.Query["sortBy"]);
                QueryParameter queryParameter = new QueryParameter(tableId, reservationId);

                ReservationDto response = (ReservationDto)_reservationInterface.GetById(queryParameter, optionsParameter);
                return new OkObjectResult(response);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete all reservations
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("DELETE_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public IActionResult DeleteAllReservations([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "reservations")] HttpRequest req)
        {
            try
            {
                _reservationInterface.DeleteAll();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete reservation by ID
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="BadRequestObjectResult"/>
        /// </returns>
        [Function("DELETE_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        public IActionResult DeleteReservation([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                QueryParameter queryParameter = new QueryParameter(tableId, reservationId);

                _reservationInterface.DeleteById(queryParameter);
                return new OkResult();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Put reservation by requestBody and by ID
        /// </summary>
        /// <returns>
        /// <see cref="OkResult"/><br/>
        /// <see cref="BadRequestObjectResult"/><br/>
        /// <see cref="StatusCodes.Status400BadRequest"/>
        /// </returns>
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
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                if (string.IsNullOrEmpty(requestBody))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                else
                {
                    QueryParameter queryParameter = new QueryParameter(tableId, reservationId);
                    UpdateReservationDto reservationDto = JsonConvert.DeserializeObject<UpdateReservationDto>(requestBody);

                    reservationDto.IsValid();

                    _reservationInterface.UpdateById(queryParameter, reservationDto);

                    return new OkResult();
                }
            }
            catch (NotImplementedException ex)
            {
                return new BadRequestObjectResult(ex.Message);
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

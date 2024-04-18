using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using CRUD_Reservation_ClassLibrary;
using CRUD.Interface;
using CRUD.Repository;
using System.Collections.Generic;


namespace CRUD_Functions
{
    public class TableFunctions //Dependency Injection informieren (Scope, Singleton)
    {
        IReservationRepository rep = new ReservationRepository();

        IValidationRepository val = new ValidationRepository();

        ReservationDto reservationDto = new ReservationDto();

// ------------------------------------------------------------------
// Post Reservation
// ------------------------------------------------------------------
        [FunctionName("POST_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ReservationDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.ResetContent, Description = "Paremeters were correct, but table is already occupied")]
        public async Task<IActionResult> PostReservation([HttpTrigger(AuthorizationLevel.Function, "post", Route = "reservations")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                reservationDto = JsonConvert.DeserializeObject<ReservationDto>(requestBody);
                
                val.IsRequestBodyValide(reservationDto);

                if (rep.Create(reservationDto) == true)
                {
                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Parameter. Your times collides with already existing times.");
                }
            }
            catch (NullReferenceException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Properties must be set and not null.");
            }
            catch (InvalidCastException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Timeproperties must be set and be convertable.");
            }
            catch (ArgumentOutOfRangeException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Your Kapacity is out of range or smaller than 0.");
            }
            catch (JsonException)
            {
                return new BadRequestObjectResult("Invalid Parameter. There is a problem with converting your JSON Data.");
            }
            catch (InvalidDataException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Starttime must not before Endtime.");
            }
        }

// ------------------------------------------------------------------
// Get All Reservations
// ------------------------------------------------------------------
        [FunctionName("GET_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TableDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> GetAllReservations([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations")] HttpRequest req)
        {
            try
            {
                List<TableDto> response = rep.GetAll();
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
        [FunctionName("GET_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiParameter(name: "tableId", Required  = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ReservationDto), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        public async Task<IActionResult> GetReservation([HttpTrigger(AuthorizationLevel.Function, "get", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                val.IsRequestQueryValide(tableId, reservationId);
                ReservationDto response = rep.GetById(tableId, reservationId);
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
        [FunctionName("DELETE_All_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Something unexpected happend")]
        public async Task<IActionResult> DeleteAllReservations([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "reservations")] HttpRequest req)
        {
            try
            {
                rep.DeleteAll();
                return new OkResult();
            }
            catch(Exception)
            {
                return new BadRequestObjectResult("Something unexpected happend");
            }
        }

// ------------------------------------------------------------------
// Delete Single Reservation
// ------------------------------------------------------------------
        [FunctionName("DELETE_Reservation")]
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
                val.IsRequestQueryValide(tableId, reservationId);
                rep.DeleteById(tableId, reservationId);
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
        [FunctionName("PUT_Reservation")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reservation" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "tableId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiParameter(name: "reservationId", Required = true, Type = typeof(int), In = ParameterLocation.Path)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ReservationDto), Required = true, Description = "Date and Time properties must be **DateOnly/TimeOnly** convertable")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Paremeters were given incorrectly")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.ResetContent, Description = "Paremeters were correct, but table is already occupied")]
        public async Task<IActionResult> PutReservation([HttpTrigger(AuthorizationLevel.Function, "put", Route = "reservations/{tableId}/{reservationId}")] HttpRequest req, int tableId, int reservationId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                reservationDto = JsonConvert.DeserializeObject<ReservationDto>(requestBody);

                val.IsRequestQueryValide(tableId, reservationId);
                val.IsRequestBodyValide(reservationDto);

                if (rep.UpdateById(tableId, reservationId, reservationDto) == true)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestObjectResult("Invalid Parameter. Your times collides with already existing times.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Your Parameters are out of range or smaller than 0.");
            }
            catch (NullReferenceException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Properties must be set and not null.");
            }
            catch (InvalidCastException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Timeproperties must be set and be convertable.");
            }
            catch (JsonException)
            {
                return new BadRequestObjectResult("Invalid Parameter. There is a problem with converting your JSON Data.");
            }
            catch (InvalidDataException)
            {
                return new BadRequestObjectResult("Invalid Parameter. Starttime must not before Endtime.");
            }
        }
    }
}


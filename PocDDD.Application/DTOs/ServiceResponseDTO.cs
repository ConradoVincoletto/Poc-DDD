using System.Net;
using PocDDD.Application.Pagination;

namespace PocDDD.Application.DTOs
{
    public class ServiceResponseDTO<T>
    {
        public ServiceResponseDTO() { }

        public ServiceResponseDTO(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponseDTO(Exception ex)
        {
            StatusCode = HttpStatusCode.InternalServerError;
            IsSuccess= false;
            Message = ex.GetBaseException().Message;
        }

        public PaginationDTO? PaginationModel { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}

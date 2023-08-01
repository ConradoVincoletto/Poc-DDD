namespace PocDDD.Application.DTOs
{
    public class ServiceResponseDTO<T>
    {
        public PaginationDTO? PaginationModel { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}

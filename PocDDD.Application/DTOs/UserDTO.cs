namespace PocDDD.Application.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<OrderDTO> OrderDTOs { get; set; }
    }
}

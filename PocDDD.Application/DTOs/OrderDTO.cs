﻿namespace PocDDD.Application.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatAt { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

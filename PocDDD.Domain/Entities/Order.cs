namespace PocDDD.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User {get; private set; }
        public DateTime CreatAt { get; set; }
        public decimal TotalPrice { get; set; }

        public Order(bool isActive, int userId, DateTime creatAt, decimal totalPrice)
        {
            IsActive = isActive;
            UserId = userId;
            CreatAt = creatAt;
            TotalPrice = totalPrice;
        }
    }
}

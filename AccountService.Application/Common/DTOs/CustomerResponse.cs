namespace AccountService.Application.Common.DTOs
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
namespace UserAuth.Api.Data.DTOs.Response
{
    public class UserResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}

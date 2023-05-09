namespace UserAuth.Api.Data.DTOs.Response
{
    public class TokenResponse
    {
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}

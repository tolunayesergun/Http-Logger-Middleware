namespace HttpLogger.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetToken();
    }
}

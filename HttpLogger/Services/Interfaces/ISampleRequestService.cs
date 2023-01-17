namespace HttpLogger.Services.Interfaces
{
    public interface ISampleRequestService
    {
        Task<string> GetDataFromAPI();
    }
}

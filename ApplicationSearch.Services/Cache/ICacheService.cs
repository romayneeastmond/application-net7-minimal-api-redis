namespace ApplicationSearch.Services.Cache
{
    public interface ICacheService
    {
        Task Set(string key, string value);

        Task<string> Get(string key);

        Task Delete(string key);
    }
}

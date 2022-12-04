namespace ApplicationSearch.Services.Cache
{
    public interface ICacheService
    {
        List<string> Scan(string pattern);

        Task Set(string key, string value);

        Task<string> Get(string key);

        Task Delete(string key);
    }
}

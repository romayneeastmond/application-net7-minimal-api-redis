using ApplicationSearch.Services.ViewModels;

namespace ApplicationSearch.Services.Cache
{
    public interface ICacheService
    {
        List<string> Scan(string pattern);

        int ScanCount(string pattern);

        Task<List<ResultSummaryViewModel>> ScanList(string pattern, int startIndex, int pageSize);

        Task Set(string key, string value);

        Task<string> Get(string key);

        Task Delete(string key);
    }
}

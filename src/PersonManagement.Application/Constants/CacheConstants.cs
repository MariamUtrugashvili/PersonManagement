namespace PersonManagement.Application.Constants
{
    public static class CacheConstants
    {
        public const string PersonCacheKeyPrefix = "person";
        public static string GetPersonCacheKey(int id) => $"{PersonCacheKeyPrefix}:{id}";
    }
}

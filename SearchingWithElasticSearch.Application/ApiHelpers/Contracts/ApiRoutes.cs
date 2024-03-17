namespace SearchingWithElasticSearch.Application.ApiHelpers.Contracts;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the searching routes.
    /// </summary>
    public static class Search
    {
        public const string SearchDocuments = "search-documents/{query:string}";
    }
}
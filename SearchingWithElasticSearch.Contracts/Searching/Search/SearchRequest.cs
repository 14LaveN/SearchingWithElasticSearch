namespace SearchingWithElasticSearch.Contracts.Searching.Search;

/// <summary>
/// Represents the search request record.
/// </summary>
/// <param name="DescriptionForSearching">The description for searching.</param>
public sealed record SearchRequest(string DescriptionForSearching)
{
    /// <summary>
    /// Create the new search request from simple description.
    /// </summary>
    /// <param name="description">The description.</param>
    /// <returns>Returns new instance of <see cref="SearchRequest"/>.</returns>
    public static implicit operator SearchRequest(string description) =>
        new(description);
}
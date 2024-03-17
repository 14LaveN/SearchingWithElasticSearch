using SearchingWithElasticSearch.Domain.Entities;

namespace SearchingWithElasticSearch.Contracts.Searching.Search;

/// <summary>
/// Represents the search request record.
/// </summary>
/// <param name="Description">The description.</param>
/// <param name="Name">The name.</param>
/// <param name="CreatedAt">The created at.</param>
public sealed record SearchResponse(
    string Description,
    string Name,
    DateTime CreatedAt)
{
    /// <summary>
    /// Create new instance of search response by <see cref="DocumentEntity"/>.
    /// </summary>
    /// <param name="entity">The  document entity.</param>
    /// <returns>Returns new <see cref="SearchResponse"/>.</returns>
    public static implicit operator SearchResponse(DocumentEntity entity) =>
        new(
            entity.Description,
            entity.Name,
            entity.CreatedAt);
};
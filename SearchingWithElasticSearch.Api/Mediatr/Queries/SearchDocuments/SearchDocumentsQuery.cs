using SearchingWithElasticSearch.Application.Core.Abstractions.Messaging;
using SearchingWithElasticSearch.Contracts.Searching.Search;
using SearchingWithElasticSearch.Domain.Core.Primitives.Maybe;

namespace SearchingWithElasticSearch.Api.Mediatr.Queries.SearchDocuments;

/// <summary>
/// Represents the search documents query record.
/// </summary>
/// <param name="Query">The string of query.</param>
public sealed record SearchDocumentsQuery(string Query)
    : ICachedQuery<Maybe<List<SearchResponse>>>
{
    public string Key { get; } =
        $"search_documents_by-{Query}.";
    public TimeSpan? Expiration { get; } = TimeSpan.FromMinutes(5);
}
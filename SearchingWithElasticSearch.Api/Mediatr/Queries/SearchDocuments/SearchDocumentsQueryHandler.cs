using SearchingWithElasticSearch.Application.Core.Abstractions.Messaging;
using SearchingWithElasticSearch.Contracts.Searching.Search;
using SearchingWithElasticSearch.Database.Data.Interfaces;
using SearchingWithElasticSearch.Domain.Common.Core.Exceptions;
using SearchingWithElasticSearch.Domain.Core.Errors;
using SearchingWithElasticSearch.Domain.Core.Primitives.Maybe;

namespace SearchingWithElasticSearch.Api.Mediatr.Queries.SearchDocuments;

/// <summary>
/// Represents the <see cref="SearchDocumentsQuery"/> handler class. 
/// </summary>
public sealed class SearchDocumentsQueryHandler(
        ISearchRepository searchRepository,
        ILogger<SearchDocumentsQueryHandler> logger)
    : IQueryHandler<SearchDocumentsQuery, Maybe<List<SearchResponse>>>
{
    /// <inheritdoc />
    public async Task<Maybe<List<SearchResponse>>> Handle(
        SearchDocumentsQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Request for search - {request.Query} {DateTime.Now}");

            if (request.Query is null)
            {
                logger.LogWarning("Query with the same description not found");
                throw new NotFoundException(nameof(request.Query), "Query with the same description");
            }

            var response = await searchRepository.SearchDocumentsAsync(request.Query);

            if (response.Value.Count is 0)
            {
                logger.LogWarning($"Documents with the same chars - {request.Query} not found");
                throw new NotFoundException(DomainErrors.Documents.NotFound, nameof(DomainErrors.Documents.NotFound));
            }
            
            logger.LogInformation($"Found by request - {response} {DateTime.Now}");

            return response.Value.Select(index =>
                (SearchResponse)index).ToList();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[SearchDocumentsQueryHandler]: {exception.Message}");
            throw;
        }
    }
}
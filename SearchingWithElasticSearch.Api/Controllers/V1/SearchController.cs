using MediatR;
using SearchingWithElasticSearch.Application.ApiHelpers.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SearchingWithElasticSearch.Api.Mediatr.Queries.SearchDocuments;
using SearchingWithElasticSearch.Application.ApiHelpers.Contracts;
using SearchingWithElasticSearch.Application.ApiHelpers.Policy;
using SearchingWithElasticSearch.Contracts.Searching.Search;
using SearchingWithElasticSearch.Domain.Core.Primitives.Maybe;

namespace SearchingWithElasticSearch.Api.Controllers.V1;

/// <summary>
/// Represents the search controller.
/// </summary>
[Route("api/v1/search")]
public sealed class SearchController(ISender sender)
    : ApiController(sender, nameof(SearchController))
{
    /// <summary>
    /// Search documents by query method.
    /// </summary>
    /// <returns>Base information about Search Documents by query method.</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">OK.</response>
    /// <response code="404">Not found.</response>
    /// <response code="500">Internal server error</response>
    [HttpGet(ApiRoutes.Search.SearchDocuments)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchDocuments([FromRoute] SearchRequest request) =>
        await Maybe<SearchDocumentsQuery>
            .From(new SearchDocumentsQuery(request.DescriptionForSearching))
            .Bind<SearchDocumentsQuery, Maybe<List<SearchResponse>>>(async query =>
                await BaseRetryPolicy.Policy.Execute(async () =>
                await Sender.Send(query)))
            .Match(Ok, NotFound);
}
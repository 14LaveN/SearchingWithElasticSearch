using MediatR;
using SearchingWithElasticSearch.Application.ApiHelpers.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SearchingWithElasticSearch.Api.Controllers.V1;

/// <summary>
/// Represents the search controller.
/// </summary>
[Route("api/v1/search")]
public sealed class SearchController(ISender sender)
    : ApiController(sender, nameof(SearchController))
{
    
}
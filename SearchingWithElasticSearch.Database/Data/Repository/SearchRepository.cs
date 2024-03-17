using Nest;
using SearchingWithElasticSearch.Application.Core.Settings;
using SearchingWithElasticSearch.Database.Data.Interfaces;
using SearchingWithElasticSearch.Domain.Common.Core.Primitives;
using SearchingWithElasticSearch.Domain.Common.Core.Primitives.Maybe;
using SearchingWithElasticSearch.Domain.Entities;
using Result = SearchingWithElasticSearch.Domain.Common.Core.Primitives.Result.Result;

namespace SearchingWithElasticSearch.Database.Data.Repository;

/// <summary>
/// Represents the search repository class.
/// </summary>
public sealed class SearchRepository
    : ISearchRepository
{
    private readonly IElasticClient _client;

    /// <summary>
    /// Create the new instance of SearchRepository.
    /// </summary>
    public SearchRepository()
    {
        var settings = new ConnectionSettings(new Uri(ElasticSettings.ConnectionString))
            .DefaultIndex(ElasticSettings.DefaultIndex);
        
        _client = new ElasticClient(settings); 
        
        _client.Indices.Create(ElasticSettings.DefaultIndex, c => c
            .Map<DocumentEntity>(m => m.AutoMap())
        );
    }
    
    /// <inheritdoc />
    public async void Dispose()
    {
        await _client.ClosePointInTimeAsync();
    }

    /// <inheritdoc />
    public async Task<Maybe<IReadOnlyCollection<DocumentEntity>>> SearchDocumentsAsync(string query)
    {
        var searchResponse = await _client.SearchAsync<DocumentEntity>(s => s
            .Query(q => q
                .MultiMatch(m => m
                    .Fields(f => f
                        .Field(p => p.Name)
                        .Field(p => p.Description)
                    )
                    .Query(query)
                )
            )
        );

        return Maybe<IReadOnlyCollection<DocumentEntity>>.From(searchResponse.Documents);
    }

    /// <inheritdoc />
    public async Task<Result> DeleteDocument(Guid id)
    {
        var result = await _client.DeleteAsync<DocumentEntity>(id);

        return result.IsValid ? await Result.Success() :
            await Result.Failure(new Error("500", result.Result.ToString()));
    }

    /// <inheritdoc />
    public async Task<Result> UpdateDocument(Guid id, DocumentEntity documentEntity)
    {
        var result = await _client.UpdateAsync<DocumentEntity>(id,
            u => u.Doc(documentEntity));
        
        return result.IsValid ? await Result.Success() :
            await Result.Failure(new Error("500", result.Result.ToString()));
    }
}
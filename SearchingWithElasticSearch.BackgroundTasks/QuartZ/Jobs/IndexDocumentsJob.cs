using System.Diagnostics.Metrics;
using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Quartz;
using Quartz.Util;
using SearchingWithElasticSearch.Application.Core.Settings;
using SearchingWithElasticSearch.Domain.Common.Core.Exceptions;
using SearchingWithElasticSearch.Domain.Core.Errors;
using SearchingWithElasticSearch.Domain.Entities;

namespace SearchingWithElasticSearch.BackgroundTasks.QuartZ.Jobs;

/// <summary>
/// Represents the index documents job class.
/// </summary>
public sealed class IndexDocumentsJob
    : IJob
{
    private readonly IElasticClient _elasticClient;
    private readonly ILogger<IndexDocumentsJob> _logger;

    /// <summary>
    /// Initialize a new instance of the <see cref="IndexDocumentsJob"/>
    /// </summary>
    /// <param name="logger">The logger.</param>
    public IndexDocumentsJob(
        ILogger<IndexDocumentsJob> logger)
    {
        _logger = logger;
        
        var settings = new ConnectionSettings(new Uri(ElasticSettings.ConnectionString))
            .DefaultIndex(ElasticSettings.DefaultIndex);
        
        _elasticClient = new ElasticClient(settings); 
        
        _elasticClient.Indices.Create(ElasticSettings.DefaultIndex, c => c
            .Map<DocumentEntity>(m => m.AutoMap())
        );
    }
    
    /// <inheritdoc/>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation($"Request to index documents in {nameof(IndexDocumentsJob)}.");

            await using var connection = new 
                SqlConnection("Server=localhost;Port=5433;Database=DocumentsDb;User Id=postgres;Password=1111;");

            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();
            
            try
            {
                var query = """
                                SELECT * FROM documents
                            """;

                var documents = await connection.QueryAsync<DocumentEntity>(query);
                await transaction.CommitAsync();

                var documentEntities = documents as DocumentEntity[] ?? documents.ToArray();
                if (documentEntities.IsNullOrEmpty())
                {
                    _logger.LogWarning($"Documents not found in database.");
                    throw new NotFoundException(DomainErrors.Documents.NotFound, nameof(DomainErrors.Documents.NotFound));
                }

                var response = await _elasticClient.IndexDocumentAsync(documentEntities);

                if (!response.IsValid)
                {
                    _logger.LogWarning($"Can't index documents - {DateTime.UtcNow}.");
                    throw new AggregateException("Can't index documents.");
                }
                
                _logger.LogInformation($"Index documents at - {DateTime.UtcNow}");
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
            }
        }
        catch (Exception exception)
        {
            _logger.LogError($"[IndexDocumentsJob]: {exception.Message}");
            throw;
        }
    }
}
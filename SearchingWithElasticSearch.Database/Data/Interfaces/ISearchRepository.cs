using System.Reflection.Metadata;
using SearchingWithElasticSearch.Domain.Core.Primitives.Maybe;
using SearchingWithElasticSearch.Domain.Core.Primitives.Result;
using SearchingWithElasticSearch.Domain.Entities;

namespace SearchingWithElasticSearch.Database.Data.Interfaces;

/// <summary>
/// Represents the search repository interface.
/// </summary>
public interface ISearchRepository : IDisposable
{
    /// <summary>
    /// Search documents async.
    /// </summary>
    /// <param name="query">The string query.</param>
    /// <returns>Returns maybe list of <see cref="DocumentEntity"/>.</returns>
    Task<Maybe<IReadOnlyCollection<DocumentEntity>>> SearchDocumentsAsync(string query);
    
    /// <summary>
    /// Delete document.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the result.</returns>
    Task<Result> DeleteDocument(Guid id);
    
    /// <summary>
    /// Update document.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="documentEntity">The document entity.</param>
    /// <returns>Returns the result.</returns>
    Task<Result> UpdateDocument(Guid id, DocumentEntity documentEntity);
}
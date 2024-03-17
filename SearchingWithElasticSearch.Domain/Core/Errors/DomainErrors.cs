using SearchingWithElasticSearch.Domain.Common.Core.Primitives;
using SearchingWithElasticSearch.Domain.Core.Primitives;

namespace SearchingWithElasticSearch.Domain.Core.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the documents errors.
    /// </summary>
    public static class Documents
    {
        public static Error NotFound =>
            new("Documents.NotFound", "The documents with the specified chars was not found.");
    }
    
    /// <summary>
    /// Contains the name errors.
    /// </summary>
    public static class Name
    {
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
    }
    
    ///<summary>
    /// Contains general errors.
    ///</summary>
    public static class General
    {
        public static Error UnProcessableRequest => new(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }
}
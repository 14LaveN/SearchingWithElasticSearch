using System.ComponentModel.DataAnnotations;
using SearchingWithElasticSearch.Domain.Common.Core.Primitives;
using SearchingWithElasticSearch.Domain.Core.Primitives;
using SearchingWithElasticSearch.Domain.Core.Utility;

namespace SearchingWithElasticSearch.Domain.Entities;

/// <summary>
/// Represents document entity class.
/// </summary>
public sealed class DocumentEntity : Entity
{
    /// <summary>
    /// Initialize the <see cref="DocumentEntity"/> class.
    /// </summary>
    /// <param name="name">The metric name.</param>
    /// <param name="description">The metric Description.</param>
    public DocumentEntity(string name, string description)
    {
        Ensure.NotEmpty(name, "The name is required.", nameof(name));
        Ensure.NotEmpty(description, "The description is required.", nameof(description));
        
        Name = name;
        Description = description;
    }
    
    /// <summary>
    /// Gets or sets document name.
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets document description.
    /// </summary>
    [Required]
    public string Description { get; set; }
}
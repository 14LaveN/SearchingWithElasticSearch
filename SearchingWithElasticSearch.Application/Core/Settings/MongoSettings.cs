namespace SearchingWithElasticSearch.Application.Core.Settings;

/// <summary>
/// Represents the elastic settings class.
/// </summary>
public sealed class ElasticSettings
{
    /// <summary>
    /// Gets elastic settings key.
    /// </summary>
    public static readonly string ElasticSettingsKey = "ElasticConnection";

    /// <summary>
    /// Gets or sets connection string.
    /// </summary>
    public static readonly string  ConnectionString = "https://localhost:9200/";
    
    /// <summary>
    /// Gets or sets default index string.
    /// </summary>
    public static readonly string DefaultIndex = "documents";
}
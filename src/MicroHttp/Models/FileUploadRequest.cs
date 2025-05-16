namespace MicroHttp.Models;

/// <summary>
/// Represents a file upload request.
/// </summary>
public class FileUploadRequest
{
    /// <summary>
    /// Gets or sets the file content to upload.
    /// </summary>
    public FileContent File { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets additional form fields to include with the upload.
    /// </summary>
    public Dictionary<string, string>? FormFields { get; set; }
    
    /// <summary>
    /// Gets or sets additional files to upload.
    /// </summary>
    public List<FileContent>? AdditionalFiles { get; set; }
}

/// <summary>
/// Represents file content for upload.
/// </summary>
public class FileContent : IDisposable
{
    /// <summary>
    /// Gets the name of the file.
    /// </summary>
    public string FileName { get; }
    
    /// <summary>
    /// Gets the content stream of the file.
    /// </summary>
    public Stream Content { get; }
    
    /// <summary>
    /// Gets or sets the MIME type of the file.
    /// </summary>
    public string ContentType { get; set; } = "application/octet-stream";
    
    /// <summary>
    /// Gets or sets the form field name for the file.
    /// </summary>
    public string FieldName { get; set; } = "file";
    
    /// <summary>
    /// Creates a new instance of the FileContent class.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="content">The content stream of the file.</param>
    public FileContent(string fileName, Stream content)
    {
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }
    
    /// <summary>
    /// Creates a new instance of the FileContent class from a file path.
    /// </summary>
    /// <param name="filePath">The path to the file.</param>
    /// <returns>A new FileContent instance.</returns>
    public static FileContent FromFile(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var stream = File.OpenRead(filePath);
        var contentType = GetContentTypeFromFileName(fileName);
        
        return new FileContent(fileName, stream) { ContentType = contentType };
    }
    
    private static string GetContentTypeFromFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            _ => "application/octet-stream"
        };
    }
    
    /// <inheritdoc/>
    public void Dispose()
    {
        Content.Dispose();
    }
}
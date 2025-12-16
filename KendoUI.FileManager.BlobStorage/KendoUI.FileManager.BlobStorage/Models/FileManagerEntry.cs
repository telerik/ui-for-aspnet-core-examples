using System.Text.Json.Serialization;

namespace KendoUI.FileManager.BlobStorage.Models
{
    public sealed class FileManagerEntry
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("isDirectory")]
        public bool IsDirectory { get; init; }

        [JsonPropertyName("hasDirectories")]
        public bool HasDirectories { get; init; }

        [JsonPropertyName("path")]
        public string Path { get; init; } = string.Empty;

        [JsonPropertyName("extension")]
        public string Extension { get; init; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; init; }

        [JsonPropertyName("created")]
        public DateTime Created { get; init; }

        [JsonPropertyName("createdUtc")]
        public DateTime CreatedUtc { get; init; }

        [JsonPropertyName("modified")]
        public DateTime Modified { get; init; }

        [JsonPropertyName("modifiedUtc")]
        public DateTime ModifiedUtc { get; init; }

        [JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; init; }

        [JsonPropertyName("dateModified")]
        public DateTime DateModified { get; init; }
    }
}

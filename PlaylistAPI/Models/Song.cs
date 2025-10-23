namespace PlaylistApi.Models;

public class Song
{
    public int Id { get; set; }                  // PK (repo kendi artıracak)
    public string Title { get; set; } = default!;
    public string Artist { get; set; } = default!;
    public int DurationSeconds { get; set; }     // 3:32 -> 212 gibi
    public int? Order { get; set; }              // Playlist sırası (opsiyonel)
    public string? ArtworkUrl { get; set; }      // opsiyonel
}
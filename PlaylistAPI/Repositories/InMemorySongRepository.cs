using System.Collections.Concurrent;
using PlaylistApi.Models;

namespace PlaylistApi.Repositories;

public class InMemorySongRepository : ISongRepository
{
    private readonly object _lock = new();
    private readonly List<Song> _songs = new();
    private int _nextId = 1;

    public InMemorySongRepository()
    {
        Add(new Song { Title = "Blinding Lights", Artist = "Ahe XD", DurationSeconds = 212, Order = 1 });
        Add(new Song { Title = "Levitating", Artist = "Dua Aua", DurationSeconds = 220, Order = 2 });
        Add(new Song { Title = "Watermelon Sugar", Artist = "Ave Ado", DurationSeconds = 214, Order = 3 });
        Add(new Song { Title = "Save Your Tears", Artist = "Ahe XD", DurationSeconds = 246, Order = 4 });
        Add(new Song { Title = "Peaches", Artist = "Justin", DurationSeconds = 198, Order = 5 });
    }

    public IEnumerable<Song> GetAll(string? query = null)
    {
        lock (_lock)
        {
            IEnumerable<Song> result = _songs;

            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.Trim().ToLowerInvariant();
                result = result.Where(s =>
                    s.Title.ToLowerInvariant().Contains(q) ||
                    s.Artist.ToLowerInvariant().Contains(q));
            }

            return result
                .OrderBy(s => s.Order ?? int.MaxValue)
                .ThenBy(s => s.Id)
                .ToList();
        }
    }

    public Song? GetById(int id)
    {
        lock (_lock) { return _songs.FirstOrDefault(s => s.Id == id); }
    }

    public Song Add(Song song)
    {
        lock (_lock)
        {
            song.Id = _nextId++;
            _songs.Add(song);
            return song;
        }
    }

    public bool Update(int id, Song song)
    {
        lock (_lock)
        {
            var existing = _songs.FirstOrDefault(s => s.Id == id);
            if (existing is null) return false;

            existing.Title = song.Title;
            existing.Artist = song.Artist;
            existing.DurationSeconds = song.DurationSeconds;
            existing.ArtworkUrl = song.ArtworkUrl;
            existing.Order = song.Order;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var existing = _songs.FirstOrDefault(s => s.Id == id);
            if (existing is null) return false;
            _songs.Remove(existing);
            return true;
        }
    }

    public bool UpdateOrder(int id, int? order)
    {
        lock (_lock)
        {
            var existing = _songs.FirstOrDefault(s => s.Id == id);
            if (existing is null) return false;
            existing.Order = order;
            return true;
        }
    }
}


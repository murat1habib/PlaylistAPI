using PlaylistApi.Models;

namespace PlaylistApi.Repositories;

public interface ISongRepository
{
    IEnumerable<Song> GetAll(string? query = null);
    Song? GetById(int id);
    Song Add(Song song);
    bool Update(int id, Song song);
    bool Delete(int id);
    bool UpdateOrder(int id, int? order);
}


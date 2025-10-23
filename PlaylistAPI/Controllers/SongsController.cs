using Microsoft.AspNetCore.Mvc;
using PlaylistApi.Models;
using PlaylistApi.Repositories;

namespace PlaylistApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SongsController : ControllerBase
{
    private readonly ISongRepository _repo;

    public SongsController(ISongRepository repo)
    {
        _repo = repo;
    }

    // GET: /api/songs?q=...
    [HttpGet]
    public ActionResult<IEnumerable<Song>> GetAll([FromQuery] string? q)
    {
        var items = _repo.GetAll(q);
        return Ok(items);
    }

    // GET: /api/songs/5
    [HttpGet("{id:int}")]
    public ActionResult<Song> GetById(int id)
    {
        var song = _repo.GetById(id);
        if (song is null) return NotFound();
        return Ok(song);
    }

    // POST: /api/songs
    [HttpPost]
    public ActionResult<Song> Create([FromBody] Song song)
    {
       
        if (string.IsNullOrWhiteSpace(song.Title))
            return BadRequest("Title is required.");
        if (string.IsNullOrWhiteSpace(song.Artist))
            return BadRequest("Artist is required.");
        if (song.DurationSeconds < 0)
            return BadRequest("DurationSeconds must be >= 0.");

       
        var created = _repo.Add(new Song
        {
            Title = song.Title.Trim(),
            Artist = song.Artist.Trim(),
            DurationSeconds = song.DurationSeconds,
            ArtworkUrl = song.ArtworkUrl,
            Order = song.Order
        });

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: /api/songs/5
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Song song)
    {
        if (string.IsNullOrWhiteSpace(song.Title))
            return BadRequest("Title is required.");
        if (string.IsNullOrWhiteSpace(song.Artist))
            return BadRequest("Artist is required.");
        if (song.DurationSeconds < 0)
            return BadRequest("DurationSeconds must be >= 0.");

        var ok = _repo.Update(id, song);
        return ok ? NoContent() : NotFound();
    }

    // DELETE: /api/songs/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var ok = _repo.Delete(id);
        return ok ? NoContent() : NotFound();
    }

    // PATCH: /api/songs/5/order
    // Body: { "order": 3 }  veya  { "order": null }
    public class OrderUpdate { public int? Order { get; set; } }

    [HttpPatch("{id:int}/order")]
    public IActionResult UpdateOrder(int id, [FromBody] OrderUpdate body)
    {
        var ok = _repo.UpdateOrder(id, body?.Order);
        return ok ? NoContent() : NotFound();
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //  /api/github
    public class GithubController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string BookmarksKey = "UserBookmarks";

        public GithubController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // post to bookmark a project
        [HttpPost("bookmark")]
        public IActionResult SaveBookmark([FromBody] object repository)
        {
            // get the list
            var bookmarks = _cache.Get<List<object>>(BookmarksKey) ?? new List<object>();
            
            // add project to the list
            bookmarks.Add(repository);
            
            // save the list
            _cache.Set(BookmarksKey, bookmarks);
            return Ok(new { message = "Repository saved to bookmarks successfully!" });
        }

        
        [HttpGet("bookmarks")]
        public IActionResult GetBookmarks()
        {
            var bookmarks = _cache.Get<List<object>>(BookmarksKey) ?? new List<object>();
            return Ok(bookmarks);
        }
    }
}
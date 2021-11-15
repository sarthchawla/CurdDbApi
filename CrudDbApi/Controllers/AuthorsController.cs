using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly _2DpJw7Z4HeContext _authorContext;

        public AuthorsController(ILogger<AuthorsController> logger, _2DpJw7Z4HeContext authorContext)
        {
            _logger = logger;
            _authorContext = authorContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            _logger.LogInformation("Get all authors");
            return await _authorContext.Authors.ToListAsync();
        }

        [HttpGet]
        [Route("{emailId}")]
        public async Task<Author> GetByEmail(string emailId)
        {
            _logger.LogInformation($"Get author by emailId: {emailId}");
            return await _authorContext.Authors.FirstOrDefaultAsync(t => t.Email == emailId);
        }

        [HttpPost]
        public async Task<ActionResult> InsertAuthor(Author author)
        {
            _logger.LogInformation("Insert author");
            var result = await _authorContext.Authors.AddAsync(author);
            await _authorContext.SaveChangesAsync();

            return Accepted();
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor(Author author)
        {
            _logger.LogInformation("Update author");
            var authorToUpdate = await _authorContext.Authors.FirstOrDefaultAsync(t => t.Email == author.Email);

            authorToUpdate.Birthdate = author.Birthdate;
            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.LastName = author.LastName;

            await _authorContext.SaveChangesAsync();

            return Accepted();
        }
        
        [HttpDelete]
        [Route("{emailId}")]
        public async Task<ActionResult> DeleteAuthor(string emailId)
        {
            _logger.LogInformation($"Delete author by emailId: {emailId}");
            var authorToDelete = await _authorContext.Authors.FirstOrDefaultAsync(t => t.Email == emailId);

            _authorContext.Authors.Remove(authorToDelete);

            await _authorContext.SaveChangesAsync();

            return Accepted();
        }
    }
}

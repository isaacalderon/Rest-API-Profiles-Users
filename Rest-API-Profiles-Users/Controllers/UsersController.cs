#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rest_API_Profiles_Users;
using Rest_API_Profiles_Users.Data;

namespace Rest_API_Profiles_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(BasicUser createdUser)
        {
            Profile profile = await _context.Profiles.FindAsync(createdUser.ProfileId);

            if (profile == null)
                return NotFound($"Profile with id {createdUser.ProfileId} not found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = new User();
            
            user.Id = createdUser.Id;
            user.UserName = createdUser.UserName;
            user.Password = createdUser.Password;
            user.Profile = profile;
            user.IdEmployee = createdUser.IdEmployee;
            user.Status = createdUser.Status;
            user.CreatedDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.Login = createdUser.Login;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var a = await _context.Users.ToListAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(x => x.Profile).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, BasicUser editedUser)
        {
            if (id != editedUser.Id)
                return BadRequest($"Different Ids {id} != {editedUser.Id}");

            User user = await _context.Users.FindAsync(editedUser.Id);
            
            if (user == null)
                return NotFound($"User with Id {editedUser.Id} was not found");
            
            if (editedUser.ProfileId != user.Profile.Id)
            {
                Profile profile = await _context.Profiles.FindAsync(editedUser.ProfileId);
                user.Profile = profile;
            }

            user.UserName = editedUser.UserName;
            user.Password = editedUser.Password;
            user.IdEmployee = editedUser.IdEmployee;
            user.Status = editedUser.Status;
            user.UpdateDate = DateTime.Now;
            user.Login = editedUser.Login;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

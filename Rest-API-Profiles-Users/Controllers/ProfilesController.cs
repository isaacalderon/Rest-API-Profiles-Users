#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rest_API_Profiles_Users;
using Rest_API_Profiles_Users.Data;
using Microsoft.AspNetCore.Http;

namespace Rest_API_Profiles_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : Controller
    {
        private readonly DataContext _context;

        public ProfilesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return Ok(profile);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            return Ok(await _context.Profiles.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int? id)
        {
            Profile profile = await _context.Profiles.SingleOrDefaultAsync(profile => profile.Id == id);
            if (id == null)
                return BadRequest();
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest("Can't change ID");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool profileExists = _context.Profiles.Any(e => e.Id == profile.Id);
                    if (!profileExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(profile);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfile(int? id)
        {
            Profile profile = await _context.Profiles.FindAsync(id);
            if (id == null)
                return BadRequest();
            if (profile == null)
                return NotFound();

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return Ok($"Profile deleted {profile.Id} - {profile.Name}");
        }
    }
}

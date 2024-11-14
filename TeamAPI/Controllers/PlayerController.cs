using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamAPI.Models;

namespace TeamAPI.Controllers
{
    [Route("Players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayerDto)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = createPlayerDto.Name,
                Height = createPlayerDto.Height,
                Weight = createPlayerDto.Weight,
                CreatedTime = DateTime.Now

            };

            if (player != null)
            {
                using (var context = new TeamContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }

            return BadRequest(new {message = "Hiba az objektumképzésa során."});
        }
    }
}

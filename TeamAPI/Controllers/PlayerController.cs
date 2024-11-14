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

            return BadRequest(new { message = "Hiba az objektumképzésa során." });

        }

        [HttpGet]
        public ActionResult Get()
        {
            using (var context = new TeamContext())
            {
                return Ok(context.Players.ToList());
            }
        }


        [HttpGet("/ById")]
        public ActionResult<Player> GetById(Guid id)
        {
            
            using (var context = new TeamContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == id);    

                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound(new { message = "Nincs ilyen játékos."});
            }
        }

        [HttpPut]
        public ActionResult<Player> Put(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            using(var context = new TeamContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == id);
                if (existingPlayer != null)
                {
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Weight = updatePlayerDto.Weight;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();

                    return Ok(existingPlayer);
                }
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult<Player> Delete(Guid id)
        {
            using (var context = new TeamContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == id);

                if(player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();

                    return Ok(new {message = "Játékos törölve"});

                }
                return NotFound(new { message = "Nincs ilyen játékos." });
            }
        }

    }
}

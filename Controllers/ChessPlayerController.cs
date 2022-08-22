using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Controllers
{
    //https://api.chess.com/pub/player/brocaneli/
    [ApiController]
    [Route("player")]
    public class ChessPlayerController : ControllerBase
    {
        private readonly ILogger<ChessPlayerController> _logger;
        private readonly DataContext _dataContext;

        public ChessPlayerController(ILogger<ChessPlayerController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChessPlayer>>> Get([FromQuery] string username, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var filteredData = await _dataContext.ChessPlayers
               .Where(x => x.username == username)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
                return filteredData;
            }

            var result = await _dataContext.ChessPlayers
                .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Success",
                Meta = new
                {
                    CurrentPage = page,
                    PageSize = pageSize
                },
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChessPlayer>> Get (int id)
        {
            var player = await _dataContext.ChessPlayers.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<ChessPlayer>> CreateChessPlayer([FromBody] ChessPlayer player)
        {
            _dataContext.ChessPlayers.Add(player);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ChessPlayer>>> UpdateChessPlayer([FromBody] ChessPlayer request)
        {
            var dbPlayer = await _dataContext.ChessPlayers.FindAsync(request.id);
            if (dbPlayer == null)
                return NotFound();

            dbPlayer.url = request.url;
            dbPlayer.name = request.name;
            dbPlayer.username = request.username;
            dbPlayer.followers = request.followers;
            dbPlayer.country = request.country;
            dbPlayer.last_online = request.last_online;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ChessPlayer>> DeleteChessPlayer([FromRoute] int id)
        {
            var dbPlayer = await _dataContext.ChessPlayers.FindAsync(id);
            if (dbPlayer == null)
                return NotFound();

            _dataContext.ChessPlayers.Remove(dbPlayer);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }
    }
}
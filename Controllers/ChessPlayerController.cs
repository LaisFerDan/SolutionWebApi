using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    [ApiController]
    [Route("player")]
    public class ChessPlayerController : ControllerBase
    {
        private readonly ILogger<ChessPlayerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public ChessPlayerController(ILogger<ChessPlayerController> logger, IConfiguration configuration, DataContext dataContext)
        {
            _logger = logger;
            _configuration = configuration;
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

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChessPlayer>> Get (int id)
        {
            var player = await _dataContext.ChessPlayers.Where(p => p.id == id).FirstOrDefaultAsync();
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }


        [HttpGet("/wins")]
        public async Task<ActionResult<IEnumerable<ChessPlayer>>> NumberOfWinsBiggerThanX(int wins)
        {
            var numberOfWins = await _dataContext.ChessPlayers
               .Where(x => x.wins > wins).ToListAsync();

            if (numberOfWins == null)
            {
                return NotFound();
            }
            return Ok(numberOfWins);
        }

        [HttpGet("/country")]
        public async Task<ActionResult<IEnumerable<ChessPlayer>>> NumberOfPlayersByCountry([FromQuery] string countryName)
        {
            int count = _dataContext.ChessPlayers.Where(x => x.country == countryName).Count();

            if (count == null)
            {
                return NotFound();
            }
            return Ok(count);
        }

        [HttpPost]
        public async Task<ActionResult<ChessPlayer>> CreateChessPlayer([FromHeader] string apiKey, [FromBody] ChessPlayer player)
        {
            if (apiKey != _configuration.GetValue<string>("ApiKey"))
            {
                return Unauthorized();
            }
            _dataContext.ChessPlayers.Add(player);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<ChessPlayer>>> UpdateChessPlayer([FromHeader] string apiKey, [FromBody] ChessPlayer request)
        {
            var dbPlayer = await _dataContext.ChessPlayers.Where(x => x.id == request.id).FirstOrDefaultAsync();
            if (dbPlayer == null)
                return NotFound();

            if (apiKey != _configuration.GetValue<string>("ApiKey"))
            {
                return Unauthorized();
            }

            dbPlayer.url = request.url;
            dbPlayer.name = request.name;
            dbPlayer.username = request.username;
            dbPlayer.best_rating = request.best_rating;
            dbPlayer.wins = request.wins;
            dbPlayer.loses = request.loses;
            dbPlayer.number_of_games = request.number_of_games;
            dbPlayer.followers = request.followers;
            dbPlayer.country = request.country;
            dbPlayer.last_online = request.last_online;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ChessPlayer>> DeleteChessPlayer([FromHeader] string apiKey, [FromRoute] int id)
        {
            var dbPlayer = await _dataContext.ChessPlayers.Where(x => x.id == id).FirstOrDefaultAsync();
            if (dbPlayer == null)
                return NotFound();

            if (apiKey != _configuration.GetValue<string>("ApiKey"))
            {
                return Unauthorized();
            }

            _dataContext.ChessPlayers.Remove(dbPlayer);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.ChessPlayers.ToListAsync());
        }
    }
}
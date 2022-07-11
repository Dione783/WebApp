using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private List<SuperHero> heroes = new List<SuperHero> 
        { 
        new SuperHero { Name = "Spider Man", firstName = "Pyter", lastName = "Parker", Id = 0,locality="New York"},
        new SuperHero {Name="Ironman",firstName="Tony",lastName="Stark",locality="Long Island",Id=1},
        new SuperHero {Name="Super Man",firstName="Caléo",lastName="",locality="New York",Id=2}
        };
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.superHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await context.superHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not finded");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult> AddHero(SuperHero hero)
        {
            context.superHeroes.Add(hero);
            await context.SaveChangesAsync();
            return Ok(await context.superHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await context.superHeroes.FindAsync(request.Id);
            if(dbHero == null)
            {
                return NotFound();
            }
            dbHero.Name = request.Name;
            dbHero.firstName = request.firstName;
            dbHero.lastName = request.lastName;
            dbHero.locality = request.locality;
            await context.SaveChangesAsync();
            return Ok(await context.superHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int heroId)
        {
            var thisHero = await context.superHeroes.FindAsync(heroId);
            if(thisHero == null)
            {
                return NotFound("Hero not Founded.");
            }
            context.superHeroes.Remove(thisHero);
            await context.SaveChangesAsync();
            return Ok(await context.superHeroes.ToListAsync());
        }
    }
}

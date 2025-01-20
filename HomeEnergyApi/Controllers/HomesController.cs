using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomesController : ControllerBase
    {
        private static List<Home> homesList = new List<Home>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(homesList);
        }

        [HttpGet("{id}")]
        public Home FindById(int id)
        {
            foreach (Home home in homesList)
            {
                if (home.Id == id)
                    return home;
            }
            return null;
        }

        [HttpPost]
        public IActionResult CreateHome([FromBody] Home home)
        {
            homesList.Add(home);
            return Created($"/Homes/{home.Id}", home);
        }

        [HttpPut("{id}")]
        public Home UpdateHome([FromBody] Home newHome, [FromRoute] int id)
        {
            for (int i = 0; i < homesList.Count; i++)
            {
                if (homesList[i].Id == id)
                {
                    homesList[i] = newHome;
                    return newHome;
                }
            }
            return null;
        }

        [HttpDelete("{id}")]
        public Home DeleteHome(int id)
        {
            Home homeToDelete = null;

            foreach (Home home in homesList)
            {
                if (home.Id == id)
                    homeToDelete = home;
            }

            if (homeToDelete != null)
            {
                homesList.Remove(homeToDelete);
                return homeToDelete;
            }
            else
            {
                return null;
            }
        }
    }
}

using HotelListing.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel> {
            new Hotel {Id = 1, Name = "Grand Plaza", Address = "123 Main St", Rating = 4.5},
            new Hotel {Id = 2, Name = "Ocean View", Address = "456 Beach Road", Rating = 4.8},
        };

        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            Console.Write(id);

            var hotel = hotels.FirstOrDefault(h => h.Id == id);


            if (hotel == null)
            {
                return NotFound(new { message = "Hotel NOT Found!!" });
            }

            Console.Write(hotel.Id);

            return Ok(hotel);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Hotel newHotel)
        {
            if (hotels.Any(h => h.Id == newHotel.Id))
            {
                return BadRequest("Hotel with this ID already exists.");
            }

            hotels.Add(newHotel);

            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);

            Console.Write(existingHotel.Id);

            if (existingHotel == null) return NotFound(new { message = "Hotel NOT Found!!" });

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Console.Write("OOOO");
        
            var hotel = hotels.FirstOrDefault(h => h.Id == id);

            if (hotel == null) return NotFound(new { message = "Hotel NOT Found!!" });

            hotels.Remove(hotel);

            return NoContent();
        }


    }
}

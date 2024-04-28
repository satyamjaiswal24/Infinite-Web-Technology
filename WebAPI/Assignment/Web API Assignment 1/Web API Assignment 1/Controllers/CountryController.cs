using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API_Assignment_1.Models;

namespace Web_API_Assignment_1.Controllers
{
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {

        static List<Country> countryList = new List<Country>()
        {
            new Country{Id = 1, CountryName = "India", Capital="New Delhi"},
            new Country{Id = 2, CountryName = "Japan", Capital="Tokyo"},
            new Country{Id = 3, CountryName = "America", Capital="New York"},
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> Get()
        {
            return countryList;
        }


        // POST: api/Country (Create)
        [HttpGet]
        [Route("getcountry")]
        public IHttpActionResult Get(int id)
        {
            var country = countryList.FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        // POST: api/Country (Create)
        [HttpPost]
        [Route("postcountry")]

        public IHttpActionResult Post([FromBody] Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            countryList.Add(country);

            // Customize the response message
            string message = $"Country '{country.CountryName}' added successfully.";

            // Return a response with a custom message
            return Content(HttpStatusCode.Created, message);
        }

        // PUT: api/Country/5 (Update)
        [HttpPut]
        [Route("updcountry")]
        public IHttpActionResult Put(int id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                return BadRequest("The ID in the request body does not match the ID in the URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCountry = countryList.FirstOrDefault(c => c.Id == id);
            if (existingCountry == null)
            {
                return NotFound();
            }
            else
            {

                // Check if the provided values are the same as the existing country's values
                if (existingCountry.CountryName == country.CountryName && existingCountry.Capital == country.Capital)
                {
                    // If the values are the same, return a custom response message
                    return Content(HttpStatusCode.NotModified, "No changes were made because provided values match the existing country's values.");
                }
                else
                {
                    existingCountry.CountryName = country.CountryName;
                    existingCountry.Capital = country.Capital;

                    return Ok("Country updated successfully.");
                }
            }
        }

        // DELETE: api/Country/5 (Delete)
        [HttpDelete()]
        [Route("delcountry")]

        public IHttpActionResult Delete(int id)
        {
            var country = countryList.FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            countryList.Remove(country);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

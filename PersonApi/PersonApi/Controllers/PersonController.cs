using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private List<Person> _persons;

        public PersonController(List<Person> persons)
        {
            _persons = persons;
        }

        [HttpGet("GetAsync")]
        public IActionResult GetAsync()
        {
            Thread.Sleep(10000);
            return Ok(_persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Thread.Sleep(3000);
            var person = _persons.Find(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Invalid data");
            }
            Thread.Sleep(3000);
            person.Id = Guid.NewGuid();
            _persons.Add(person);

            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Person updatedPerson)
        {
            Thread.Sleep(3000);
            var existingPerson = _persons.Find(p => p.Id == id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            existingPerson.Name = updatedPerson.Name;
            existingPerson.Age = updatedPerson.Age;

            return Ok(existingPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingPerson = _persons.Find(p => p.Id == id);
            if (existingPerson == null)
            {
                return NotFound();
            }
            Thread.Sleep(3000);
            _persons.Remove(existingPerson);

            return NoContent();
        }
    }

}

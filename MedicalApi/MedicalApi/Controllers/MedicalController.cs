using MedicalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private List<MedicalRecord> _medicalRecords;

        public MedicalController(List<MedicalRecord> medicalRecords)
        {
            _medicalRecords = medicalRecords;
        }

        [HttpGet("GetAsync")]
        public IActionResult GetAsync()
        {
            Thread.Sleep(10000);
            return Ok(_medicalRecords);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Thread.Sleep(2000);
            var medicalRecord = _medicalRecords.Find(record => record.Id == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
            {
                return BadRequest("Invalid data");
            }

            Thread.Sleep(2000);
            medicalRecord.Id = Guid.NewGuid();
            _medicalRecords.Add(medicalRecord);

            return CreatedAtAction(nameof(Get), new { id = medicalRecord.Id }, medicalRecord);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] MedicalRecord updatedRecord)
        {
            Thread.Sleep(2000);
            var existingRecord = _medicalRecords.Find(record => record.Id == id);
            if (existingRecord == null)
            {
                return NotFound();
            }

            existingRecord.PatientName = updatedRecord.PatientName;
            existingRecord.Diagnosis = updatedRecord.Diagnosis;
            existingRecord.Treatment = updatedRecord.Treatment;

            return Ok(existingRecord);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingRecord = _medicalRecords.Find(record => record.Id == id);
            if (existingRecord == null)
            {
                return NotFound();
            }
            Thread.Sleep(2000);
            _medicalRecords.Remove(existingRecord);

            return NoContent();
        }



    }


}

using MedicalApi.Models;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly IPersonDemographicService _personDemographicService;
        private readonly IPersonMedicalRecordService _personMedicalRecordService;

        public PersonController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IPersonDemographicService personDemographicService,
            IPersonMedicalRecordService personMedicalRecordService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;

            _personDemographicService = personDemographicService;
            _personMedicalRecordService = personMedicalRecordService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var personDataTask = GetPersonDemographicData();
                var medicalDataTask = GetPersonMedicalRecord();

                var mappedDataList = PersonDataMapper.BuildPersonModel(await personDataTask, await medicalDataTask);
                return Ok(mappedDataList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPersonDemographicData")]
        private async Task<IEnumerable<PersonDemographicModel>> GetPersonDemographicData()
        {
            try
            {
                var personDemographicRecords = await _personDemographicService.GetPersonDemographicDataAsync();

                return personDemographicRecords;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetPersonMedicalRecord")]
        private async Task<IEnumerable<PersonMedicalRecordModel>> GetPersonMedicalRecord()
        {
            try
            {
                var personMedicalRecords = await _personMedicalRecordService.GetPersonMedicalRecord();

                return personMedicalRecords;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonModel data)
        {
            try
            {
                var personModel = new PersonDemographicModel
                {
                    Id = data.PersonId,
                    Name = data.Name,
                    Age = data.Age
                };

                var createdPersonResult = await CreatePersonDemographicData(personModel);

                var medicalModel = new PersonMedicalRecordModel
                {
                    Id = data.MedicalRecordId,
                    PersonId = createdPersonResult.Id,
                    PatientName = data.PatientName,
                    Diagnosis = data.Diagnosis,
                    Treatment = data.Treatment
                };

                var createdMedicalResult = await CreatePersonMedicalRecord(medicalModel);

                var createdData = new PersonModel
                {
                    Id = Guid.NewGuid(),
                    PersonId = createdPersonResult.Id,
                    Name = createdPersonResult.Name,
                    Age = createdPersonResult.Age,
                    MedicalRecordId = createdMedicalResult.Id,
                    PatientName = createdMedicalResult.PatientName,
                    Diagnosis = createdMedicalResult.Diagnosis,
                    Treatment = createdMedicalResult.Treatment
                };

                return Ok(createdData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreatePersonDemographicData")]
        private async Task<PersonDemographicModel> CreatePersonDemographicData([FromBody] PersonDemographicModel person)
        {
            try
            {
                var newPerson = await _personDemographicService.PostPersonDemographicData(person);
                if (newPerson == null)
                {
                    throw new Exception("unable to create a new person");
                }
                return newPerson;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost("CreatePersonMedicalRecord")]
        private async Task<PersonMedicalRecordModel> CreatePersonMedicalRecord([FromBody] PersonMedicalRecordModel medicalData)
        {
            try
            {
                var newMedicalRecord = await _personMedicalRecordService.CreatePersonMedicalRecord(medicalData);
                if (newMedicalRecord == null)
                {
                    throw new Exception("unable to create a person medical record");
                }
                return newMedicalRecord;
            }
            catch (Exception)
            {
                // delete person data
                throw;
            }
        }
    }
}


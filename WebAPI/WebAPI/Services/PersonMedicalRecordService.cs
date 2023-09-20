using MedicalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonApi.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace WebAPI.Services
{
    public class PersonMedicalRecordService : IPersonMedicalRecordService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IPersonDemographicService _personDemographicService;


        string medicalApiBaseUrl = "";

        public PersonMedicalRecordService(HttpClient httpClient, IConfiguration configuration, IPersonDemographicService personDemographicService)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            medicalApiBaseUrl = _configuration["App:MedicalRecordApiBaseUrl"];
            _personDemographicService = personDemographicService;
        }

        public async Task<IEnumerable<PersonMedicalRecordModel>> GetPersonMedicalRecord()
        {
            try
            {
                string apiUrl = $"{medicalApiBaseUrl}/Medical/GetAsync";
                var medicalApiResponse = await _httpClient.GetAsync(apiUrl);

                if (medicalApiResponse.IsSuccessStatusCode)
                {
                    var medicalDataJson = await medicalApiResponse.Content.ReadAsStringAsync();

                    var medicalRecords = JsonConvert.DeserializeObject<List<PersonMedicalRecordModel>>(medicalDataJson);

                    return medicalRecords;
                }
                else
                {
                    throw new Exception("unable to fetch Person Medical Records");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<PersonMedicalRecordModel> CreatePersonMedicalRecord(PersonMedicalRecordModel medicalData)
        {
            try
            {
                var medicalDataJson = JsonConvert.SerializeObject(medicalData);

                var content = new StringContent(medicalDataJson, Encoding.UTF8, "application/json");

                string apiUrl = $"{medicalApiBaseUrl}/Medical";
                var medicalApiResponse = await _httpClient.PostAsync(apiUrl, content);

                if (medicalApiResponse.IsSuccessStatusCode)
                {
                    var createdMedicalDataJson = await medicalApiResponse.Content.ReadAsStringAsync();
                    var createdMedicalData = JsonConvert.DeserializeObject<PersonMedicalRecordModel>(createdMedicalDataJson);

                    return createdMedicalData;
                }
                else
                {
                    await _personDemographicService.DeletePersonDemographicData(medicalData.PersonId);
                    throw new Exception("unable to create Person Medical Record");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

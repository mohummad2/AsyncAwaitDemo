using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonApi.Models;
using WebAPI.Models; // Replace with the correct namespace for your models

public class PersonDemographicService : IPersonDemographicService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    string personApiBaseUrl = "";

    public PersonDemographicService(HttpClient httpClient,IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        
        personApiBaseUrl = _configuration["App:PersonApiBaseUrl"];
    }

    public async Task<IEnumerable<PersonDemographicModel>> GetPersonDemographicDataAsync()
    {
        try
        {
            string apiUrl = $"{personApiBaseUrl}/Person/GetAsync";
            var personApiResponse = await _httpClient.GetAsync(apiUrl);

            if (personApiResponse.IsSuccessStatusCode)
            {
                var personDataJson = await personApiResponse.Content.ReadAsStringAsync();
                var persons = JsonConvert.DeserializeObject<List<PersonDemographicModel>>(personDataJson);
                return persons;
            }
            else
            {
                throw new Exception("unable to fetch Persons");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PersonDemographicModel> PostPersonDemographicData(PersonDemographicModel person)
    {
        try
        {
            var personJson = JsonConvert.SerializeObject(person);

            var content = new StringContent(personJson, Encoding.UTF8, "application/json");

            string apiUrl = $"{personApiBaseUrl}/Person";
            var personApiResponse = await _httpClient.PostAsync(apiUrl, content);

            if (personApiResponse.IsSuccessStatusCode)
            {
                var createdPersonJson = await personApiResponse.Content.ReadAsStringAsync();
                var createdPerson = JsonConvert.DeserializeObject<PersonDemographicModel>(createdPersonJson);

                return createdPerson;
            }
            else
            {
                throw new Exception("unable to create Person");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeletePersonDemographicData(Guid personId)
    {
        try
        {
            string apiUrl = $"{personApiBaseUrl}/Person";
            var personApiResponse = await _httpClient.DeleteAsync(apiUrl);

            if (!personApiResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to delete Person");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}

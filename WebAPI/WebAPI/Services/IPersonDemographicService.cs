using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

public interface IPersonDemographicService
{
    Task<IEnumerable<PersonDemographicModel>> GetPersonDemographicDataAsync();
    Task<PersonDemographicModel> PostPersonDemographicData(PersonDemographicModel person);
    Task DeletePersonDemographicData(Guid personId);
}
using MedicalApi.Models;

namespace WebAPI.Services
{
    public interface IPersonMedicalRecordService
    {
        Task<IEnumerable<PersonMedicalRecordModel>> GetPersonMedicalRecord();
        Task<PersonMedicalRecordModel> CreatePersonMedicalRecord(PersonMedicalRecordModel medicalData);
    }
}
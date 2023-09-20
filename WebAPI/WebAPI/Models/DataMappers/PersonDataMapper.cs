namespace WebAPI.Models
{
    using MedicalApi.Models;
    using PersonApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
   
    public static class PersonDataMapper
    {
        public static List<PersonModel> BuildPersonModel(IEnumerable<PersonDemographicModel> personData, IEnumerable<PersonMedicalRecordModel> medicalData)
        {
            var mappedDataList = new List<PersonModel>();

            foreach (var person in personData)
            {
                var data = new PersonModel
                {
                    Id = Guid.NewGuid(),
                    PersonId = person.Id,
                    Name = person.Name,
                    Age = person.Age,
                    MedicalRecordId = Guid.Empty,
                    PatientName = "",
                    Diagnosis = "",
                    Treatment = ""
                };

                mappedDataList.Add(data);
            }

            foreach (var medicalRecord in medicalData)
            {
                var matchingData = mappedDataList.FirstOrDefault(d => d.PersonId == medicalRecord.PersonId);

                if (matchingData != null)
                {
                    matchingData.MedicalRecordId = medicalRecord.Id;
                    matchingData.PatientName = medicalRecord.PatientName;
                    matchingData.Diagnosis = medicalRecord.Diagnosis;
                    matchingData.Treatment = medicalRecord.Treatment;
                }
            }

            return mappedDataList;
        }
    }

}

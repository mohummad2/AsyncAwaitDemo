namespace MedicalApi.Models
{
    public class PersonMedicalRecordModel
    {
        public Guid Id { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public Guid PersonId { get; set; }
    }
}

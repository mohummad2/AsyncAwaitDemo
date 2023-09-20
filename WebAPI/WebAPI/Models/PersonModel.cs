namespace WebAPI.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid MedicalRecordId { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}

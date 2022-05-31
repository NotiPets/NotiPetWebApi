using Notipet.Domain;
namespace Notipet.Web.DTO
{
    public class DigitalVaccineDto
    {
        public Guid PetId { get; set; }
        public int BusinessId { get; set; }
        public Guid UserId { get; set; }
        public Guid VaccineId { get; set; }
        public DigitalVaccine ConvertToType()
        {
            DigitalVaccine v = new DigitalVaccine();
            v.Id = new Guid();
            v.UserId = UserId;
            v.PetId = PetId;
            v.BusinessId = BusinessId;
            v.VaccineId = VaccineId;
            v.Date = DateTime.UtcNow;
            return v;
        }
    }
}

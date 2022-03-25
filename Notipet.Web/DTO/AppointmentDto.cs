using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class AppointmentDto : IConvertToType<Appointment>
    {
        public Guid SpecialistId { get; set; }
        public bool IsEmergency { get; set; }
        public DateTime Date { get; set; }
        public Appointment ConvertToType()
        {
            return new Appointment
            {
                SpecialistId = SpecialistId,
                Date = Date,
                IsEmergency = IsEmergency
            };
        }
    }
}

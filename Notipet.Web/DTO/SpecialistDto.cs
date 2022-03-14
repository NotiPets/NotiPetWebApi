namespace Notipet.Web.DTO
{
    public class SpecialistDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Specialty { get; set; }
        public double? Rating { get; set; }
        public string? BusinessId { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public SpecialistDto ConvertToType()
        {
            return new SpecialistDto();
        }
    }

}

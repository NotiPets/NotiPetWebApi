using Notipet.Domain;

namespace Notipet.Web.DTO
{
    public class RatingDto : IConvertToType<Rating>
    {
        public int BusinessId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int RatingNumber { get; set; }
        public Rating ConvertToType()
        {
            return new Rating()
            {
                BusinessId = BusinessId,
                UserId = UserId,
                Comment = Comment,
                RatingNumber = RatingNumber
            };
        }
    }
}

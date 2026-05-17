using Core.Entities;

namespace Entities.Concrete
{
    public class EtutNote : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int FromTeacherUserId { get; set; }
        public int? ToTeacherUserId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

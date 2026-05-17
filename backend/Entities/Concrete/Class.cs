using Core.Entities;

namespace Entities.Concrete
{
    public class Class : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public int? TeacherUserId { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

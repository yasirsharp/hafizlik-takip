using Core.Entities;

namespace Entities.Concrete
{
    public class Attendance : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int TeacherUserId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

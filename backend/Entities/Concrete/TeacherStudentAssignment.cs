using Core.Entities;

namespace Entities.Concrete
{
    public class TeacherStudentAssignment : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int TeacherUserId { get; set; }
        public int StudentId { get; set; }
        public string AssignmentType { get; set; }  // Official, Temporary
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}

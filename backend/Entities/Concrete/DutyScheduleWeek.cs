using Core.Entities;

namespace Entities.Concrete
{
    public class DutyScheduleWeek : IEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int ConstraintId { get; set; }
        public DateTime WeekStartDate { get; set; }
        public string Status { get; set; }  // Draft, Published
        public int CreatedByUserId { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

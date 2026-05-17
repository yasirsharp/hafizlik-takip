using Core.Entities;

namespace Entities.Concrete
{
    public class DutyScheduleConstraint : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string? AssistantWeeklyPattern { get; set; }  // JSON: [2,3,2,3]
        public int TeachersPerDay { get; set; }
        public int AssistantsPerDay { get; set; }
        public int MaxConsecutiveDays { get; set; }
        public string? EveningShiftUserIds { get; set; }  // JSON: [1,2]
        public string? FixedDayAssignments { get; set; }  // JSON
        public string? ExcludedDates { get; set; }  // JSON
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using Core.Entities;

namespace Entities.Concrete
{
    public class DutyScheduleEntry : IEntity
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public int DayOfWeek { get; set; }  // 1=Pazartesi ... 5=Cuma
        public int UserId { get; set; }
        public string UserType { get; set; }  // Teacher, Assistant, Evening
        public int SlotOrder { get; set; }
        public int? ClassId { get; set; }
    }
}

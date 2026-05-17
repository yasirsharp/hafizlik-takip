using Core.Entities;

namespace Entities.Concrete
{
    public class Lesson : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int TeacherUserId { get; set; }
        public string LessonType { get; set; }  // YeniSayfa, Tekrar, DersVerme
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int Performance { get; set; }  // 1-10
        public string? Notes { get; set; }
        public DateTime LessonDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

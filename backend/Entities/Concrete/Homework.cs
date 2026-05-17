using Core.Entities;

namespace Entities.Concrete
{
    public class Homework : IEntity, IHasTenant
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int TeacherUserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Status { get; set; }  // Bekliyor, Tamamlandi, Eksik
        public DateTime DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using Core.Entities;

namespace Entities.Concrete
{
    public class VideoRecord : IEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int TeacherUserId { get; set; }
        public string FilePath { get; set; }
        public string? Title { get; set; }
        public int DurationSeconds { get; set; }
        public long FileSizeBytes { get; set; }
        public DateTime RecordedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

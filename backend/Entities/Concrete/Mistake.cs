using Core.Entities;

namespace Entities.Concrete
{
    public class Mistake : IEntity
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int LessonId { get; set; }
        public string MistakeType { get; set; }  // Telaffuz, Tecvid, Harekeler, Kelime, Ayet, Sayfa, Tekrar, Sira, Ezber, Diger
        public int Page { get; set; }
        public int? AyahNumber { get; set; }
        public string? Word { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

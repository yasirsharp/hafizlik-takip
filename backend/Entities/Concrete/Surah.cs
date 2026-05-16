using Core.Entities;

namespace Entities.Concrete
{
    public class Surah : IEntity
    {
        public int Id { get; set; }
        public int SurahNumber { get; set; }
        public string NameArabic { get; set; }
        public string NameTurkish { get; set; }
        public string NameEnglish { get; set; }
        public int AyahCount { get; set; }
        public int StartPage { get; set; }
        public int JuzNumber { get; set; }
    }
}

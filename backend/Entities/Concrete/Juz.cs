using Core.Entities;

namespace Entities.Concrete
{
    public class Juz : IEntity
    {
        public int Id { get; set; }
        public int JuzNumber { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}

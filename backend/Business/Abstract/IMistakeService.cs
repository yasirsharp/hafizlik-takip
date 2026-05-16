using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMistakeService
    {
        IDataResult<List<Mistake>> GetAll();
        IDataResult<List<Mistake>> GetByLessonId(int lessonId);
        IResult Add(Mistake mistake);
        IResult Delete(Mistake mistake);
    }
}

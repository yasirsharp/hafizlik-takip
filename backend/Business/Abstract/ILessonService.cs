using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ILessonService
    {
        IDataResult<List<Lesson>> GetAll();
        IDataResult<List<Lesson>> GetByStudentId(int studentId);
        IDataResult<Lesson> GetById(int id);
        IResult Add(Lesson lesson);
        IResult Update(Lesson lesson);
        IResult Delete(Lesson lesson);
    }
}

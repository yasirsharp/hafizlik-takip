using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IHomeworkService
    {
        IDataResult<List<Homework>> GetAll();
        IDataResult<List<Homework>> GetByStudentId(int studentId);
        IDataResult<Homework> GetById(int id);
        IResult Add(Homework homework);
        IResult Update(Homework homework);
        IResult Delete(Homework homework);
    }
}

using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IStudentService
    {
        IDataResult<List<Student>> GetAll();
        IDataResult<Student> GetById(int id);
        IResult Add(Student student);
        IResult Update(Student student);
        IResult Delete(Student student);
    }
}

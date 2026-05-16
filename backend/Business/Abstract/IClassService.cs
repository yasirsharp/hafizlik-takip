using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IClassService
    {
        IDataResult<List<Class>> GetAll();
        IDataResult<Class> GetById(int id);
        IResult Add(Class classEntity);
        IResult Update(Class classEntity);
        IResult Delete(Class classEntity);
    }
}

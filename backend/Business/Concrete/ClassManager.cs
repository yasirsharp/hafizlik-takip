using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ClassManager : IClassService
    {
        private IClassDal _classDal;

        public ClassManager(IClassDal classDal)
        {
            _classDal = classDal;
        }

        public IDataResult<List<Class>> GetAll()
        {
            return new SuccessDataResult<List<Class>>(_classDal.GetAll(), Messages.ClassesListed);
        }

        public IDataResult<Class> GetById(int id)
        {
            return new SuccessDataResult<Class>(_classDal.Get(c => c.Id == id));
        }

        public IResult Add(Class classEntity)
        {
            _classDal.Add(classEntity);
            return new SuccessResult(Messages.ClassAdded);
        }

        public IResult Update(Class classEntity)
        {
            _classDal.Update(classEntity);
            return new SuccessResult(Messages.ClassUpdated);
        }

        public IResult Delete(Class classEntity)
        {
            _classDal.Delete(classEntity);
            return new SuccessResult("Sinif silindi");
        }
    }
}

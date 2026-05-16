using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IEtutNoteService
    {
        IDataResult<List<EtutNote>> GetAll();
        IDataResult<List<EtutNote>> GetByStudentId(int studentId);
        IDataResult<List<EtutNote>> GetUnreadByTeacherId(int teacherUserId);
        IResult Add(EtutNote etutNote);
        IResult MarkAsRead(int id);
    }
}

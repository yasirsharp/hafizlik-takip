using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<StudentManager>().As<IStudentService>().SingleInstance();
            builder.RegisterType<ClassManager>().As<IClassService>().SingleInstance();
            builder.RegisterType<LessonManager>().As<ILessonService>().SingleInstance();
            builder.RegisterType<MistakeManager>().As<IMistakeService>().SingleInstance();
            builder.RegisterType<HomeworkManager>().As<IHomeworkService>().SingleInstance();
            builder.RegisterType<AttendanceManager>().As<IAttendanceService>().SingleInstance();
            builder.RegisterType<EtutNoteManager>().As<IEtutNoteService>().SingleInstance();
            builder.RegisterType<DutyScheduleManager>().As<IDutyScheduleService>().SingleInstance();
            builder.RegisterType<TeacherStudentManager>().As<ITeacherStudentService>().SingleInstance();

            // DALs
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<EfStudentDal>().As<IStudentDal>().SingleInstance();
            builder.RegisterType<EfClassDal>().As<IClassDal>().SingleInstance();
            builder.RegisterType<EfLessonDal>().As<ILessonDal>().SingleInstance();
            builder.RegisterType<EfMistakeDal>().As<IMistakeDal>().SingleInstance();
            builder.RegisterType<EfHomeworkDal>().As<IHomeworkDal>().SingleInstance();
            builder.RegisterType<EfEtutNoteDal>().As<IEtutNoteDal>().SingleInstance();
            builder.RegisterType<EfAttendanceDal>().As<IAttendanceDal>().SingleInstance();
            builder.RegisterType<EfDutyScheduleConstraintDal>().As<IDutyScheduleConstraintDal>().SingleInstance();
            builder.RegisterType<EfDutyScheduleWeekDal>().As<IDutyScheduleWeekDal>().SingleInstance();
            builder.RegisterType<EfDutyScheduleEntryDal>().As<IDutyScheduleEntryDal>().SingleInstance();
            builder.RegisterType<EfTeacherStudentAssignmentDal>().As<ITeacherStudentAssignmentDal>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            // JWT
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            // AOP Interceptor
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

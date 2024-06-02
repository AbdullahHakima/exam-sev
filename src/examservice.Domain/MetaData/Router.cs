
namespace examservice.Domain.MetaData;

public static class Router
{
    public const string RouteName = "Api";
    public const string Version = "V0.1";
    public const string GeneralRule = RouteName + "/" + Version + "/";

    public static class StudentRouting
    {
        public const string Prefix = GeneralRule + "Students";
        public const string AddStudent = Prefix + "/AddStudent";
    }

    public static class QuestionRouting
    {
        public const string Prefix = GeneralRule + "courses/{courseId}/questions";
        public const string UploadQuestionsBank = Prefix + "/UploadQuestionsBank";
        public const string AddQuestion = Prefix + "/AddQuestion";
        public const string UpdateQuestion = Prefix + "/{questionId}";
        public const string DeleteQuestion = Prefix + "/{questionId}";
        public const string DeleteBatchQuestions = Prefix + "/DeleteBatchQuestions";
    }


    public static class InstructorRouting
    {
        public const string Prefix = GeneralRule + "instructors";
        public const string AddInstructor = Prefix + "/AddInstructor";
        public const string GetById = Prefix + "/{instructorId}";
        public const string InstructorCourses = Prefix + "/{instructorId}/courses";
    }

    public static class CourseRouting
    {
        public const string Prefix = GeneralRule + "courses";
        public const string AddCourse = Prefix + "/AddCourse";
        public const string GetById = Prefix + "/{courseId}";
    }

    public static class ModuleReouting
    {
        public const string Prefix = GeneralRule + "courses/{courseId}/instructors/{instructorId}/modules";
        public const string GenerateModuels = Prefix + "/GenerateModules";
    }

    public static class QuizRouting
    {
        public const string Prefix = GeneralRule + "courses/{courseId}/instructors/{instructorId}/quizzes";
        public const string CreateQuiz = Prefix + "/createquiz";
        public const string GetQuizModules = Prefix + "/{quizId}/modules/List";
        //for web Get request
        public const string GetById = Prefix + "/{quizId}/Details";
        //for mobile post request
        public const string ViewQuizDetails = Prefix + "/{quizId}/Details";
        public const string GetAllCourseQuizzes = Prefix + "/List";
        public const string UpdateQuiz = Prefix + "/{quizId}";
        public const string IncomingQuizzes = GeneralRule + "courses/{courseId}/quizzes/incommingList";
    }

}

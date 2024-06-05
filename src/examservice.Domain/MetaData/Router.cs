
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
        public const string PublishQuiz = Prefix + "/{quizId}/publish";
        public const string EnrollToQuiz = Prefix + "/EnrollToQuiz";
        public const string SubmitQuiz = Prefix + "/Submit";
        public const string ViewStudentQuizDetails = Prefix + "/ViewQuizDetails";
        public const string ViewInstructorQuizzes = Prefix + "/AllInstructorQuizzes";//For web
        public const string UpdateQuizDetails = Prefix + "/{quizId}/UpdateDetails";
        public const string DeleteQuiz = Prefix + "/{quizId}/DeleteQuiz";
        public const string InstructorQuizDetails = Prefix + "/{quizId}/QuizDeatils";
        public const string ViewStudentQuizzes = Prefix + "/AllStudentQuizzes";// for mobile
    }

}

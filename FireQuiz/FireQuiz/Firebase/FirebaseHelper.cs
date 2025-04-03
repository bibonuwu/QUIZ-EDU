using Firebase.Database;
using FireQuiz.Models;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireQuiz.Models;
using System.Threading.Tasks;

public static class FirebaseHelper
{
    private static readonly FirebaseClient firebase = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");

    public static async Task SaveQuiz(Quiz quiz)
    {
        await firebase
            .Child("Quizzes")
            .Child(quiz.Subject)
            .Child(quiz.Name) // Используем имя теста вместо ID
            .PutAsync(new
            {
                Name = quiz.Name,
                Subject = quiz.Subject,
                Questions = quiz.Questions
            });
    }


    public static async Task<List<Quiz>> GetQuizzes(string subject)
    {
        var quizzes = await firebase
            .Child("Quizzes")
            .Child(subject)
            .OnceAsync<Quiz>();

        return quizzes.Select(q =>
        {
            var quiz = q.Object;
            quiz.Id = q.Key;
            return quiz;
        }).ToList();
    }
}

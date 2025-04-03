using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class FirebaseService
    {
        private FirebaseClient _client;

        public FirebaseService()
        {
            _client = new FirebaseClient("https://test-qstem-default-rtdb.firebaseio.com/");
        }

        public async Task<Dictionary<string, Dictionary<string, QuizData>>> GetAllQuizzesAsync()
        {
            var response = await _client.Child("Quizzes").OnceSingleAsync<object>();
            string json = JsonConvert.SerializeObject(response);
            var quizzes = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, QuizData>>>(json);
            return quizzes;
        }

        // ✅ Новый метод — получение статистики пользователя
        public async Task<JObject> GetUserScoreAsync(User user)
        {
            string userKey = $"{user.firstName} {user.lastName} {user.Id}";
            var scoreData = await _client
                .Child("Score")
                .Child(userKey)
                .OnceSingleAsync<object>();

            if (scoreData == null)
                return null;

            var json = JsonConvert.SerializeObject(scoreData);
            return JObject.Parse(json);
        }

        public async Task<object> GetRawScoreByKeyAsync(string userKey)
        {
            var scoreData = await _client
                .Child("Score")
                .Child(userKey)
                .OnceSingleAsync<object>();

            return scoreData;
        }

    }
}

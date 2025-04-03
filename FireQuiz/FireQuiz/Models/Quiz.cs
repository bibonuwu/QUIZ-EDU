using System.Collections.Generic;

namespace FireQuiz.Models
{
    public class Quiz
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}

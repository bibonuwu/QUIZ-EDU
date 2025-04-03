using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class QuizData
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCorrect { get; set; }
    }

}

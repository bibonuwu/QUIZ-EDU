using System;

namespace FireQuiz.Models
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public string ImageUrl { get; set; } // Добавляем ссылку на изображение
    }
}

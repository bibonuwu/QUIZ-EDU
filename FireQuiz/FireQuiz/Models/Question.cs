using FireQuiz.Models;
using System.Collections.Generic;

public class Question
{
    public string Text { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
    public string ImageUrl { get; set; } // Добавляем ссылку на изображение вопроса
}

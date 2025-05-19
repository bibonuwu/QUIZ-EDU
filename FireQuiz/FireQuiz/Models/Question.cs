using FireQuiz.Models;
using System.Collections.Generic;

public class Question
{
    public string Text { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
    public string ImageUrl { get; set; }

    // Для matching-вопроса:
    public bool IsMatching { get; set; } = false;
    public List<MatchingPairData> MatchingPairs { get; set; }
}
public class MatchingPairData
{
    public string ImageUrl { get; set; } // ссылка на storage
    public string Text { get; set; }
    public List<string> Options { get; set; }
}
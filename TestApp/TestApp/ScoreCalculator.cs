using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    public static class ScoreCalculator
    {
        public static int CalculateQuestionScore(Question question, List<int> selectedIndices)
        {
            int correctAnswersCount = question.Answers.Count(a => a.IsCorrect);

            if (correctAnswersCount == 1)
            {
                // Вопрос с одним правильным ответом
                if (selectedIndices.Count == 1 && question.Answers[selectedIndices[0]].IsCorrect)
                    return 1;

                return 0;
            }
            else if (correctAnswersCount > 1)
            {
                // Многовариантный вопрос
                int correctSelected = selectedIndices.Count(idx => question.Answers[idx].IsCorrect);
                int incorrectSelected = selectedIndices.Count(idx => !question.Answers[idx].IsCorrect);

                if (correctSelected == correctAnswersCount && incorrectSelected == 0)
                    return 2; // Полностью правильный ответ
                else if (correctSelected > 0 && incorrectSelected == 0)
                    return 1; // Частично правильный ответ
                else
                    return 0; // Неверный ответ
            }

            return 0; // Ошибочный вопрос
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class User
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public string school { get; set; }
        public string grade { get; set; }
        public string literal { get; set; }
        public string gender { get; set; }
        public string password { get; set; }
    }


    public class TestResult
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public Dictionary<string, SubjectResult> Subjects { get; set; }
    }

    public class SubjectResult
    {
        public Dictionary<string, string> Answers { get; set; } // Вопрос-ответ True/False
        public int Score { get; set; }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leder_Efter_Server
{
    class TriviaHandler
    {
        public static void SetQuestion(string codeRoom, bool ready, int maxQuestion)
        {
            int questionResult = 0;

            if (ready)
            {
                Random rand = new Random();
                questionResult = rand.Next(0, maxQuestion);
                ServerSend.TriviaQuestionBroadcast(codeRoom, questionResult);
            }
        }

        public static void SetDatabase(string codeRoom, int maxCategory, int maxQuestion)
        {
            List<int> numberTemp = new List<int>();

            for (int i = 0; i < maxQuestion; i++)
            {
                int categoryResult = 0;
                int questionResult = 0;

                Random rand = new Random(DateTime.Now.Millisecond);

                do
                {
                    categoryResult = rand.Next(0, maxCategory);
                    questionResult = rand.Next(0, maxQuestion);
                } while (numberTemp.Contains(questionResult));
                numberTemp.Add(questionResult);                                

                //Console.WriteLine($"Category: {categoryResult} - Question: {questionResult}");
                ServerSend.TriviaDatabaseBroadcast(codeRoom, categoryResult, questionResult);
            }
        }
    }
}

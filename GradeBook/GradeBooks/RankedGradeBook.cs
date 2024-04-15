using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var grades = Students.Select(x => x.AverageGrade).ToList();
            grades = grades.OrderByDescending(x => x).ToList();
            var top20Percent = grades.Count / 5;
            var letterGrade = 5;
            var scoredHigher = 0;

            for (int i = 0; i < grades.Count; i++)
            {
                if (grades[i] <= averageGrade)
                {
                    break;
                }

                scoredHigher++;
                if(scoredHigher >= top20Percent)
                {
                    scoredHigher -= top20Percent;
                    letterGrade--;
                }
            }
            return letterGrade switch
            {
                5 => 'A',
                4 => 'B',
                3 => 'C',
                2 => 'D',
                _ => 'F'
            };
        }
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading required at least 5 students.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }

    }
}

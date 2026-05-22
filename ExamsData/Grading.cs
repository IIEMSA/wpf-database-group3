using System;
using System.Collections.Generic;
using System.Text;

namespace ExamsData
{
    public class Grading
    {
        public static string GetGrade(int mark)
        {
            if (mark >= 80)
                return "A";
            else if (mark >= 70)
                return "B";
            else if (mark >= 60)
                return "C";
            else if (mark >= 50)
                return "D";
            else
                return "F";
        }
    }
}

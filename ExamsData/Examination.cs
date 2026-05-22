using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace ExamsData
{
    public class Examination
    {
        public string connectionString = "Server=localhost;Database=grp3_exam_db;Uid=root;Pwd=King2014;";

        public List<Mark> GetAllMarks()
        {
            List<Mark> marks = new List<Mark>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM marks";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marks.Add(new Mark
                        {
                            Id = reader.GetInt32("Id"),
                            StudentNumber = reader.GetString("StudentNumber"),
                            MarkValue = reader.GetInt32("Mark"),
                            Grade = reader.GetString("Grade")
                        });
                    }
                }

            }
            return marks;
        }
    }
}

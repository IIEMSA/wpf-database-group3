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

        public void AddMark(Mark mark)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var query = "INSERT INTO marks (StudentNumber, Mark, Grade) VALUES (@studentNumber, @mark, @grade)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentNumber", mark.StudentNumber);
                cmd.Parameters.AddWithValue("@mark", mark.MarkValue);
                cmd.Parameters.AddWithValue("@grade", mark.Grade);
                cmd.ExecuteNonQuery();// Execute the query to insert the mark into the database
            }
        }
        public void UpdateMark(Mark mark)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var query = "UPDATE marks SET StudentNumber = @studentNumber,Mark = @mark, Grade = @grade WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", mark.Id);
                cmd.Parameters.AddWithValue("@studentNumber", mark.StudentNumber);
                cmd.Parameters.AddWithValue("@mark", mark.MarkValue);
                cmd.Parameters.AddWithValue("@grade", mark.Grade);
                cmd.ExecuteNonQuery();// Execute the query to update the mark in the database
            }
        }

        public void DeleteMark(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var query = "DELETE FROM marks WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();// delete the mark from the database
            }
        }
    }
}

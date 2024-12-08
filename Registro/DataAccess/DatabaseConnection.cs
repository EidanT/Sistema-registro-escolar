using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SistemaRegistroActividades
{
    public class DatabaseConnection
    {
        private string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Métodos de gestión para estudiantes
        public void AddStudent(string name, string lastname, int age, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO students (name, lastname, age, email) VALUES (@name, @lastname, @age, @email)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditStudent(int id, string name, string lastname, int age, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE students SET name = @name, lastname = @lastname, age = @age, email = @email WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM students WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Métodos de gestión para actividades
        public void AddActivity(string name, DateTime date)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO activities (name, date) VALUES (@name, @date)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditActivity(int id, string name, DateTime date)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE activities SET name = @name, date = @date WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteActivity(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM activities WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        

        public bool UpdateActivity(int activityId, string newName, DateTime? newDate)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                
                // Preparamos la consulta SQL para actualizar el nombre y la fecha
                string query = "UPDATE activities SET name = @name, date = @date WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                
                // Establecemos los parámetros para el nombre y la fecha
                cmd.Parameters.AddWithValue("@id", activityId);
                cmd.Parameters.AddWithValue("@name", string.IsNullOrEmpty(newName) ? (object)DBNull.Value : newName);
                cmd.Parameters.AddWithValue("@date", newDate.HasValue ? (object)newDate.Value : DBNull.Value);

                // Ejecutamos el comando
                int rowsAffected = cmd.ExecuteNonQuery();

                // Si rowsAffected es mayor que 0, significa que la actualización fue exitosa
                return rowsAffected > 0;
            }
        }


        // Métodos de gestión para participantes
        public void AddParticipant(int studentId, int activityId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO participants (student_id, activity_id) VALUES (@studentId, @activityId)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditParticipant(int id, int studentId, int activityId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE participants SET student_id = @studentId, activity_id = @activityId WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@activityId", activityId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteParticipant(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM participants WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Modificar la calificación de un participante
        public void ModifyScore(int id, decimal score)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE participants SET score = @score WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@score", score);
                cmd.ExecuteNonQuery();
            }
        }

        // Métodos de obtención de datos (sin cambios)
        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        lastname = reader.GetString("lastname"),
                        age = reader.GetInt32("age"),
                        email = reader.GetString("email")
                    });
                }
            }
            return students;
        }

        public List<Activity> GetActivities()
        {
            List<Activity> activities = new List<Activity>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM activities", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new Activity
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        date = reader.GetDateTime("date")
                    });
                }
            }
            return activities;
        }

        public List<Participant> GetParticipants()
        {
            List<Participant> participants = new List<Participant>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM participants", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    participants.Add(new Participant
                    {
                        id = reader.GetInt32("id"),
                        student_id = reader.GetInt32("student_id"),
                        activity_id = reader.GetInt32("activity_id"),
                        score = reader.GetDecimal("score")
                    });
                }
            }
            return participants;
        }

        public Student GetStudentById(int studentId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT name, lastname FROM students WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", studentId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Student
                    {
                        name = reader.GetString("name"),
                        lastname = reader.GetString("lastname")
                    };
                }
            }
            return null;
        }

        public Activity GetActivityById(int activityId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                // Consulta que incluye la fecha
                string query = "SELECT name, date FROM activities WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", activityId);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Asegúrate de manejar el valor de la fecha correctamente
                    DateTime activityDate = reader.IsDBNull(reader.GetOrdinal("date")) ? DateTime.MinValue : reader.GetDateTime("date");

                    return new Activity
                    {
                        name = reader.GetString("name"),
                        date = activityDate // Asigna la fecha recuperada
                    };
                }
            }
            return null; // Si no se encuentra la actividad
        }


        public Participant GetParticipantById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM participants WHERE id = @id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Aquí debes mapear los campos de la base de datos al objeto Participant
                            return new Participant
                            {
                                id = reader.GetInt32("id"),
                                student_id = reader.GetInt32("student_id"),
                                activity_id = reader.GetInt32("activity_id"),
                                score = reader.GetDecimal("score")
                            };
                        }
                        else
                        {
                            return null; // Si no se encuentra el participante
                        }
                    }
                }
            }
        }

        public List<Participant> GetParticipantsByStudentId(int studentId)
        {
            List<Participant> participants = new List<Participant>();
            
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM participants WHERE student_id = @studentId";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            participants.Add(new Participant
                            {
                                id = reader.GetInt32("id"),
                                student_id = reader.GetInt32("student_id"),
                                activity_id = reader.GetInt32("activity_id"),
                                score = reader.GetDecimal("score")
                            });
                        }
                    }
                }
            }

            return participants;
        }

    }

    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string email { get; set; }
    }

    public class Activity
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
    }

    public class Participant
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int activity_id { get; set; }
        public decimal score { get; set; }
    }
}

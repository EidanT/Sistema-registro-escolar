using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SistemaRegistroActividades
{
    public class Program
    {
        static string connectionString = "Server=localhost;Database=registroestudiantes;User=root;Password=@Xxnimasterxx123";
        static DatabaseConnection dbConnection = new DatabaseConnection(connectionString);

        public static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Sistema de Registro de Actividades Escolares");
                    Console.WriteLine("1. Gestionar Estudiantes");
                    Console.WriteLine("2. Gestionar Actividades");
                    Console.WriteLine("3. Gestionar Participantes");
                    Console.WriteLine("4. Salir");
                    Console.Write("Elige una opción: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ManageStudents();
                            break;
                        case "2":
                            ManageActivities();
                            break;
                        case "3":
                            ManageParticipants();
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Intenta de nuevo.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
                Console.ReadKey();
            }
        }

        private static void ManageStudents()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestionar Estudiantes");
                Console.WriteLine("1. Mostrar Estudiantes");
                Console.WriteLine("2. Agregar Estudiante");
                Console.WriteLine("3. Editar Estudiante");
                Console.WriteLine("4. Eliminar Estudiante");
                Console.WriteLine("5. Volver");
                Console.Write("Elige una opción: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowStudents();
                            break;
                        case "2":
                            AddStudent();
                            break;
                        case "3":
                            EditStudent();
                            break;
                        case "4":
                            DeleteStudent();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Intenta de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void ShowStudents()
        {
            List<Student> students = dbConnection.GetStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Edad: {student.age}, Email: {student.email}");
                }
            }
        }

        private static void AddStudent()
        {
            string name = GetValidStringInput("Ingrese el nombre del estudiante: ");
            string lastname = GetValidStringInput("Ingrese el apellido del estudiante: ");
            int age = GetValidIntInput("Ingrese la edad del estudiante: ");
            string email = GetValidStringInput("Ingrese el email del estudiante: ");

            dbConnection.AddStudent(name, lastname, age, email);
            Console.WriteLine("Estudiante agregado exitosamente.");
        }

        public static void EditStudent()
        {
            List<SchoolStudent> students = dbConnection.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine("Estudiantes registrados:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Edad: {student.age}, Email: {student.email}");
            }

            Console.WriteLine("Ingrese el ID del estudiante a editar (o deje vacío para volver):");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            int studentId = int.Parse(input);

            var selectedStudent = students.FirstOrDefault(s => s.id == studentId);

            if (selectedStudent == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine($"Nombre actual: {selectedStudent.name}");
            Console.WriteLine($"Apellido actual: {selectedStudent.lastname}");
            Console.WriteLine($"Edad actual: {selectedStudent.age}");
            Console.WriteLine($"Email actual: {selectedStudent.email}");

            Console.WriteLine("Ingrese el nuevo nombre (o deje vacío para conservar el actual):");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) selectedStudent.name = newName;

            Console.WriteLine("Ingrese el nuevo apellido (o deje vacío para conservar el actual):");
            string newLastname = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLastname)) selectedStudent.lastname = newLastname;

            Console.WriteLine("Ingrese la nueva edad (o deje vacío para conservar el actual):");
            string newAgeInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAgeInput) && int.TryParse(newAgeInput, out int newAge))
            {
                selectedStudent.age = newAge;
            }

            Console.WriteLine("Ingrese el nuevo email (o deje vacío para conservar el actual):");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail)) selectedStudent.email = newEmail;

            dbConnection.UpdateStudent(selectedStudent);
            Console.WriteLine("Estudiante actualizado exitosamente.");
        }




        private static void DeleteStudent()
        {
            List<Student> students = dbConnection.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine("Listado de estudiantes:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Edad: {student.age}, Email: {student.email}");
            }

            string input = GetValidStringInput("Ingrese el ID del estudiante a eliminar (deje vacío para volver): ");
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if (int.TryParse(input, out int id))
            {
                dbConnection.DeleteStudent(id);
                Console.WriteLine("Estudiante eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("ID inválido. No se eliminó ningún estudiante.");
            }
        }


        private static void ManageActivities()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestionar Actividades");
                Console.WriteLine("1. Mostrar Actividades");
                Console.WriteLine("2. Agregar Actividad");
                Console.WriteLine("3. Editar Actividad");
                Console.WriteLine("4. Eliminar Actividad");
                Console.WriteLine("5. Volver");
                Console.Write("Elige una opción: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowActivities();
                            break;
                        case "2":
                            AddActivity();
                            break;
                        case "3":
                            EditActivity();
                            break;
                        case "4":
                            DeleteActivity();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Intenta de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void ShowActivities()
        {
            List<Activity> activities = dbConnection.GetActivities();
            if (activities.Count == 0)
            {
                Console.WriteLine("No hay actividades registradas.");
            }
            else
            {
                foreach (var activity in activities)
                {
                    Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}, Fecha: {activity.date.ToString("dd/MM/yyyy")}");
                }
            }
        }

        private static void AddActivity()
        {
            string name = GetValidStringInput("Ingrese el nombre de la actividad: ");
            DateTime date = GetValidDateInput("Ingrese la fecha de la actividad (DD/MM/YYYY): ");

            dbConnection.AddActivity(name, date);
            Console.WriteLine("Actividad agregada exitosamente.");
        }

        private static void EditActivity()
        {
            List<Activity> activities = dbConnection.GetActivities();
            if (activities.Count == 0)
            {
                Console.WriteLine("No hay actividades registradas.");
                return;
            }

            Console.WriteLine("Listado de actividades:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}, Fecha: {activity.date:dd/MM/yyyy}");
            }

            Console.Write("Ingrese el ID de la actividad a editar (deje vacío para volver): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            Activity activityToEdit = activities.FirstOrDefault(a => a.id == id);
            if (activityToEdit == null)
            {
                Console.WriteLine("Actividad no encontrada.");
                return;
            }

            string newName = GetValidStringInput("Ingrese el nuevo nombre de la actividad: ");
            DateTime newDate = GetValidDateInput("Ingrese la nueva fecha (DD/MM/YYYY): ");

            dbConnection.EditActivity(id, newName, newDate);

            Console.WriteLine("Actividad editada exitosamente.");
        }



        private static void DeleteActivity()
        {
            List<Activity> activities = dbConnection.GetActivities();

            if (activities.Count == 0)
            {
                Console.WriteLine("No hay actividades registradas.");
                return;
            }

            Console.WriteLine("Listado de actividades:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}, Fecha: {activity.date:dd/MM/yyyy}");
            }

            string input = GetValidStringInput("Ingrese el ID de la actividad a eliminar (deje vacío para volver): ");
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if (int.TryParse(input, out int id))
            {
                dbConnection.DeleteActivity(id);
                Console.WriteLine("Actividad eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("ID inválido. No se eliminó ninguna actividad.");
            }
        }


        private static void ManageParticipants()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestionar Participantes");
                Console.WriteLine("1. Mostrar Participantes");
                Console.WriteLine("2. Agregar Participante");
                Console.WriteLine("3. Editar Participante");
                Console.WriteLine("4. Eliminar Participante");
                Console.WriteLine("5. Modificar Calificación");
                Console.WriteLine("6. Volver");
                Console.Write("Elige una opción: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowParticipants();
                            break;
                        case "2":
                            AddParticipant();
                            break;
                        case "3":
                            EditParticipant();
                            break;
                        case "4":
                            DeleteParticipant();
                            break;
                        case "5":
                            ModifyScore();
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Intenta de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void ShowParticipants()
        {
            List<Participant> participants = dbConnection.GetParticipants();
            if (participants.Count == 0)
            {
                Console.WriteLine("No hay participantes registrados.");
            }
            else
            {
                foreach (var participant in participants)
                {
                    Student student = dbConnection.GetStudentById(participant.student_id); 
                    Activity activity = dbConnection.GetActivityById(participant.activity_id);  

                    if (student != null && activity != null)
                    {
                        Console.WriteLine($"ID: {participant.id}, Estudiante: {student.name} {student.lastname}, Actividad: {activity.name}, Calificación: {participant.score}");
                    }
                    else
                    {
                        Console.WriteLine($"ID: {participant.id}, Participante no válido (falta estudiante o actividad).");
                    }
                }
            }
        }



        private static void AddParticipant()
        {
            List<Student> students = dbConnection.GetStudents();
            List<Participant> participants = dbConnection.GetParticipants();

            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine("Listado de estudiantes:");
            foreach (var student in students)
            {
                bool isInActivity = participants.Any(p => p.student_id == student.id);
                string status = isInActivity ? "Está en una actividad" : "No está en ninguna actividad";

                Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Estado: {status}");
            }

            Console.Write("Ingrese el ID del estudiante (deje vacío para volver): ");
            string studentInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(studentInput))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            if (!int.TryParse(studentInput, out int studentId))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            Student selectedStudent = students.FirstOrDefault(s => s.id == studentId);
            if (selectedStudent == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            List<Activity> activities = dbConnection.GetActivities();
            if (activities.Count == 0)
            {
                Console.WriteLine("No hay actividades registradas.");
                return;
            }

            Console.WriteLine("Listado de actividades:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}");
            }

            Console.Write("Ingrese el ID de la actividad (deje vacío para volver): ");
            string activityInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(activityInput))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            if (!int.TryParse(activityInput, out int activityId))
            {
                Console.WriteLine("ID de actividad inválido. Por favor, ingrese un número.");
                return;
            }

            Activity selectedActivity = activities.FirstOrDefault(a => a.id == activityId);
            if (selectedActivity == null)
            {
                Console.WriteLine("Actividad no encontrada.");
                return;
            }

            if (participants.Any(p => p.student_id == studentId))
            {
                Console.WriteLine("El estudiante ya está participando en una actividad. No se puede agregar nuevamente.");
                return;
            }

            dbConnection.AddParticipant(studentId, activityId);
            Console.WriteLine("Participante agregado exitosamente.");
        }


        private static void EditParticipant()
        {
            List<Participant> participants = dbConnection.GetParticipants();
            if (participants.Count == 0)
            {
                Console.WriteLine("No hay participantes registrados.");
                return;
            }

            Console.WriteLine("Listado de participantes:");
            foreach (var participant in participants)
            {
                Student student = dbConnection.GetStudentById(participant.student_id);
                Activity activity = dbConnection.GetActivityById(participant.activity_id);
                Console.WriteLine($"ID: {participant.id}, Estudiante: {student.name} {student.lastname}, Actividad: {activity.name}");
            }

            Console.Write("Ingrese el ID del participante a editar (deje vacío para volver): ");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            Participant participantToEdit = participants.FirstOrDefault(p => p.id == id);
            if (participantToEdit == null)
            {
                Console.WriteLine("Participante no encontrado.");
                return;
            }

            List<Activity> activities = dbConnection.GetActivities();
            Console.WriteLine("Listado de actividades:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}");
            }

            Console.Write("Ingrese el nuevo ID de la actividad (deje vacío para volver): ");
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            if (!int.TryParse(input, out int newActivityId))
            {
                Console.WriteLine("ID de actividad inválido. Por favor, ingrese un número.");
                return;
            }

            dbConnection.EditParticipant(participantToEdit.id, participantToEdit.student_id, newActivityId);

            Console.WriteLine("Participante editado exitosamente.");
        }



        private static void DeleteParticipant()
        {
            List<Participant> participants = dbConnection.GetParticipants();

            if (participants.Count == 0)
            {
                Console.WriteLine("No hay participantes registrados.");
                return;
            }

            Console.WriteLine("Listado de participantes:");
            foreach (var participant in participants)
            {
                var student = dbConnection.GetStudentById(participant.student_id);
                var activity = dbConnection.GetActivityById(participant.activity_id);
                Console.WriteLine($"ID: {participant.id}, Estudiante: {student.name} {student.lastname}, Actividad: {activity.name}");
            }

            string input = GetValidStringInput("Ingrese el ID del participante a eliminar (deje vacío para volver): ");
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if (int.TryParse(input, out int id))
            {
                dbConnection.DeleteParticipant(id);
                Console.WriteLine("Participante eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("ID inválido. No se eliminó ningún participante.");
            }
        }

        


        public static void ModifyScore()
        {
            List<StudentActivityScore> studentsInActivitiesWithScores = dbConnection.GetStudentsInActivitiesWithScores();

            if (studentsInActivitiesWithScores.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados en actividades.");
                return;
            }

            Console.WriteLine("Estudiantes en actividades:");
            foreach (var studentActivity in studentsInActivitiesWithScores)
            {
                Console.WriteLine($"ID: {studentActivity.StudentId}, Nombre: {studentActivity.StudentName} {studentActivity.StudentLastName}, Actividad: {studentActivity.ActivityName}, Calificación: {studentActivity.Score}");
            }

            Console.WriteLine("Ingrese el ID del estudiante para modificar su calificación (o deje vacío para volver):");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                return; 
            }

            int studentId = int.Parse(input);

            var selectedStudent = studentsInActivitiesWithScores.FirstOrDefault(s => s.StudentId == studentId);

            if (selectedStudent == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            Console.WriteLine("Ingrese la nueva calificación:");
            decimal newGrade;
            if (!decimal.TryParse(Console.ReadLine(), out newGrade))
            {
                Console.WriteLine("Calificación no válida.");
                return;
            }

            dbConnection.UpdateStudentGrade(selectedStudent.StudentId, newGrade);
            Console.WriteLine("Calificación modificada exitosamente.");
        }






        private static string GetValidStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static int GetValidIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                Console.WriteLine("Por favor, introduce un número entero válido.");
            }
        }

        private static decimal GetValidDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                Console.WriteLine("Por favor, introduce un número decimal válido.");
            }
        }

        private static DateTime GetValidDateInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime result))
                {
                    return result;
                }
                Console.WriteLine("Por favor, introduce una fecha válida en formato DD/MM/YYYY.");
            }
        }
    }
}

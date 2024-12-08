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

        // Gestión de estudiantes
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

        private static void EditStudent()
        {
            // Mostrar un listado de todos los estudiantes
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

            // Solicitar al usuario el ID del estudiante que desea editar
            Console.Write("Ingrese el ID del estudiante a editar (deje vacío para volver): ");
            string input = Console.ReadLine();

            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID ingresado
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            // Verificar si el estudiante existe
            Student studentToEdit = students.FirstOrDefault(s => s.id == id);
            if (studentToEdit == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            // Pedir los nuevos datos para el estudiante
            string newName = GetValidStringInput("Ingrese el nuevo nombre del estudiante: ");
            string newLastname = GetValidStringInput("Ingrese el nuevo apellido del estudiante: ");
            int newAge = GetValidIntInput("Ingrese la nueva edad del estudiante: ");
            string newEmail = GetValidStringInput("Ingrese el nuevo email del estudiante: ");

            // Actualizar el estudiante en la base de datos
            dbConnection.EditStudent(id, newName, newLastname, newAge, newEmail);

            Console.WriteLine("Estudiante editado exitosamente.");
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


        // Gestión de actividades
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
            // Mostrar listado de actividades
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

            // Solicitar ID de la actividad
            Console.Write("Ingrese el ID de la actividad a editar (deje vacío para volver): ");
            string input = Console.ReadLine();

            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID ingresado
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            // Verificar si la actividad existe
            Activity activityToEdit = activities.FirstOrDefault(a => a.id == id);
            if (activityToEdit == null)
            {
                Console.WriteLine("Actividad no encontrada.");
                return;
            }

            // Pedir nuevos datos
            string newName = GetValidStringInput("Ingrese el nuevo nombre de la actividad: ");
            DateTime newDate = GetValidDateInput("Ingrese la nueva fecha (DD/MM/YYYY): ");

            // Actualizar la actividad en la base de datos
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


        // Gestión de participantes
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
                    // Obtener el estudiante y la actividad a partir de los IDs
                    Student student = dbConnection.GetStudentById(participant.student_id);  // Obtener datos del estudiante
                    Activity activity = dbConnection.GetActivityById(participant.activity_id);  // Obtener datos de la actividad

                    // Verificar que tanto el estudiante como la actividad existan
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
            // Mostrar listado de estudiantes
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
                // Verificar si el estudiante ya participa en una actividad
                bool isInActivity = participants.Any(p => p.student_id == student.id);
                string status = isInActivity ? "Está en una actividad" : "No está en ninguna actividad";

                Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Estado: {status}");
            }

            // Solicitar ID del estudiante
            Console.Write("Ingrese el ID del estudiante (deje vacío para volver): ");
            string studentInput = Console.ReadLine();

            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(studentInput))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID ingresado
            if (!int.TryParse(studentInput, out int studentId))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            // Verificar si el estudiante existe
            Student selectedStudent = students.FirstOrDefault(s => s.id == studentId);
            if (selectedStudent == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            // Mostrar listado de actividades
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

            // Solicitar ID de la actividad
            Console.Write("Ingrese el ID de la actividad (deje vacío para volver): ");
            string activityInput = Console.ReadLine();

            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(activityInput))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID de actividad ingresado
            if (!int.TryParse(activityInput, out int activityId))
            {
                Console.WriteLine("ID de actividad inválido. Por favor, ingrese un número.");
                return;
            }

            // Verificar si la actividad existe
            Activity selectedActivity = activities.FirstOrDefault(a => a.id == activityId);
            if (selectedActivity == null)
            {
                Console.WriteLine("Actividad no encontrada.");
                return;
            }

            // Verificar si el estudiante ya está en una actividad
            if (participants.Any(p => p.student_id == studentId))
            {
                Console.WriteLine("El estudiante ya está participando en una actividad. No se puede agregar nuevamente.");
                return;
            }

            // Agregar participante sin calificación
            dbConnection.AddParticipant(studentId, activityId);
            Console.WriteLine("Participante agregado exitosamente.");
        }


        private static void EditParticipant()
        {
            // Mostrar listado de participantes
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

            // Solicitar ID del participante
            Console.Write("Ingrese el ID del participante a editar (deje vacío para volver): ");
            string input = Console.ReadLine();
            
            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID ingresado
            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido. Por favor, ingrese un número.");
                return;
            }

            // Verificar si el participante existe
            Participant participantToEdit = participants.FirstOrDefault(p => p.id == id);
            if (participantToEdit == null)
            {
                Console.WriteLine("Participante no encontrado.");
                return;
            }

            // Mostrar listado de actividades
            List<Activity> activities = dbConnection.GetActivities();
            Console.WriteLine("Listado de actividades:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.id}, Nombre: {activity.name}");
            }

            // Solicitar nuevo ID de actividad
            Console.Write("Ingrese el nuevo ID de la actividad (deje vacío para volver): ");
            input = Console.ReadLine();

            // Verificar si el campo está vacío
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Volviendo al menú anterior...");
                return;
            }

            // Validar el ID de actividad
            if (!int.TryParse(input, out int newActivityId))
            {
                Console.WriteLine("ID de actividad inválido. Por favor, ingrese un número.");
                return;
            }

            // Actualizar el participante en la base de datos
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


        private static void ModifyScore()
        {
            // Mostrar el listado de estudiantes con su ID, nombre y calificación
            List<Student> students = dbConnection.GetStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }

            Console.WriteLine("Listado de Estudiantes:");
            foreach (var student in students)
            {
                // Obtener la calificación promedio del estudiante de todos sus participantes
                List<Participant> studentParticipants = dbConnection.GetParticipantsByStudentId(student.id);
                decimal averageScore = studentParticipants.Count > 0
                    ? studentParticipants.Average(p => p.score) // Promedio de calificación de los participantes
                    : 0;
                
                Console.WriteLine($"ID: {student.id}, Nombre: {student.name} {student.lastname}, Calificación: {averageScore}");
            }

            // Solicitar el ID del estudiante
            int studentId = GetValidIntInput("\nIngrese el ID del estudiante: ");

            // Verificar si el estudiante existe
            Student selectedStudent = dbConnection.GetStudentById(studentId);
            if (selectedStudent == null)
            {
                Console.WriteLine("Estudiante no encontrado.");
                return;
            }

            // Mostrar el listado de participantes para este estudiante
            List<Participant> participants = dbConnection.GetParticipantsByStudentId(studentId);
            if (participants.Count == 0)
            {
                Console.WriteLine("Este estudiante no tiene participa en ninguna actividad.");
                return;
            }

            Console.WriteLine("\nListado de Participantes del Estudiante:");
            foreach (var participant in participants)
            {
                Activity activity = dbConnection.GetActivityById(participant.activity_id);
                Console.WriteLine($"ID: {participant.id}, Actividad: {activity.name}, Calificación: {participant.score}");
            }

            // Solicitar el ID del participante a modificar
            int participantId = GetValidIntInput("\nIngrese el ID del participante cuya calificación desea modificar: ");

            // Obtener el participante por su ID
            Participant participantToEdit = dbConnection.GetParticipantById(participantId);
            if (participantToEdit == null)
            {
                Console.WriteLine("Participante no encontrado.");
                return;
            }

            // Solicitar la nueva calificación
            decimal newScore = GetValidDecimalInput("Ingrese la nueva calificación: ");

            // Modificar la calificación en la base de datos
            dbConnection.ModifyScore(participantId, newScore);
            Console.WriteLine("Calificación modificada exitosamente.");
        }



        // Métodos de validación de entrada
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

// services/databaseService.cs - kompletny kod
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using EventRegistrationApp.Models;

namespace EventRegistrationApp.Services
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService()
        {
            // konfiguracja polaczenia z baza danych mysql
            // zmien haslo na swoje prawdziwe haslo root
            connectionString = "Server=localhost;Port=3306;Database=event_registration;Uid=root;Pwd=admin;";
        }

        public void InitializeDatabase()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // utworzenie tabeli uzytkownikow
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        first_name VARCHAR(50) NOT NULL,
                        last_name VARCHAR(50) NOT NULL,
                        login VARCHAR(50) UNIQUE NOT NULL,
                        password VARCHAR(255) NOT NULL,
                        email VARCHAR(100) UNIQUE NOT NULL,
                        permissions VARCHAR(20) DEFAULT 'user',
                        registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                    )";

                // utworzenie tabeli wydarzen
                string createEventsTable = @"
                    CREATE TABLE IF NOT EXISTS events (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        name VARCHAR(200) NOT NULL,
                        agenda TEXT,
                        event_date DATETIME NOT NULL,
                        is_active BOOLEAN DEFAULT TRUE
                    )";

                // utworzenie tabeli zapisow na wydarzenia
                string createRegistrationsTable = @"
                    CREATE TABLE IF NOT EXISTS event_registrations (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        user_id INT NOT NULL,
                        event_id INT NOT NULL,
                        participation_type VARCHAR(50) NOT NULL,
                        food_preference VARCHAR(50) NOT NULL,
                        is_confirmed BOOLEAN DEFAULT FALSE,
                        status VARCHAR(20) DEFAULT 'Pending',
                        registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
                        FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE
                    )";

                ExecuteNonQuery(connection, createUsersTable);
                ExecuteNonQuery(connection, createEventsTable);
                ExecuteNonQuery(connection, createRegistrationsTable);

                // wstawienie przykladowego administratora
                string insertAdmin = @"
                    INSERT IGNORE INTO users (first_name, last_name, login, password, email, permissions)
                    VALUES ('Admin', 'User', 'admin', 'admin123', 'admin@example.com', 'admin')";
                
                ExecuteNonQuery(connection, insertAdmin);

                // wstawienie przykladowych uzytkownikow
                string insertUsers = @"
                    INSERT IGNORE INTO users (first_name, last_name, login, password, email, permissions)
                    VALUES 
                    ('Jan', 'Kowalski', 'jkowalski', 'haslo123', 'jan.kowalski@example.com', 'user'),
                    ('Anna', 'Nowak', 'anowak', 'haslo123', 'anna.nowak@example.com', 'user')";
                
                ExecuteNonQuery(connection, insertUsers);

                // wstawienie przykladowych wydarzen
                string insertEvents = @"
                    INSERT IGNORE INTO events (name, agenda, event_date)
                    VALUES 
                    ('Konferencja IT 2024', 'najnowsze trendy w it, ai i machine learning oraz nowoczesne technologie chmurowe', '2024-12-15 09:00:00'),
                    ('Workshop C#', 'praktyczne zastosowanie c# w projektach enterprise, linq, async/await, design patterns', '2024-12-20 10:00:00'),
                    ('Hackathon 2024', '48-godzinny maraton programistyczny z nagrodami dla najlepszych zespolow', '2024-12-25 18:00:00'),
                    ('Webinar Cloud Computing', 'wprowadzenie do chmury obliczeniowej, aws, azure, best practices', '2025-01-10 14:00:00')";
                
                ExecuteNonQuery(connection, insertEvents);
            }
        }

        private void ExecuteNonQuery(MySqlConnection connection, string query)
        {
            using (var command = new MySqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // pobranie uzytkownika po loginie
        public User? GetUserByLogin(string login)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users WHERE login = @login";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32("id"),
                                FirstName = reader.GetString("first_name"),
                                LastName = reader.GetString("last_name"),
                                Login = reader.GetString("login"),
                                Password = reader.GetString("password"),
                                Email = reader.GetString("email"),
                                Permissions = reader.GetString("permissions"),
                                RegistrationDate = reader.GetDateTime("registration_date")
                            };
                        }
                    }
                }
            }
            return null;
        }

        // utworzenie nowego uzytkownika
        public bool CreateUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO users (first_name, last_name, login, password, email, permissions)
                        VALUES (@firstName, @lastName, @login, @password, @email, @permissions)";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);
                        command.Parameters.AddWithValue("@login", user.Login);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@permissions", user.Permissions);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // pobranie aktywnych wydarzen
        public List<Event> GetActiveEvents()
        {
            var events = new List<Event>();
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM events WHERE is_active = true ORDER BY event_date";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new Event
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Agenda = reader.GetString("agenda"),
                            EventDate = reader.GetDateTime("event_date"),
                            IsActive = reader.GetBoolean("is_active")
                        });
                    }
                }
            }
            
            return events;
        }

        // sprawdzenie czy uzytkownik jest juz zarejestrowany na wydarzenie
        public bool IsUserRegisteredForEvent(int userId, int eventId)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM event_registrations WHERE user_id = @userId AND event_id = @eventId";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@eventId", eventId);
                        
                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // rejestracja na wydarzenie z sprawdzaniem duplikatow
        public bool RegisterForEvent(EventRegistration registration)
        {
            try
            {
                // sprawdz czy uzytkownik nie jest juz zarejestrowany
                if (IsUserRegisteredForEvent(registration.UserId, registration.EventId))
                {
                    return false; // juz zarejestrowany
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO event_registrations (user_id, event_id, participation_type, food_preference, status)
                        VALUES (@userId, @eventId, @participationType, @foodPreference, @status)";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", registration.UserId);
                        command.Parameters.AddWithValue("@eventId", registration.EventId);
                        command.Parameters.AddWithValue("@participationType", registration.ParticipationType);
                        command.Parameters.AddWithValue("@foodPreference", registration.FoodPreference);
                        command.Parameters.AddWithValue("@status", registration.Status ?? "Pending");
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // pobranie wszystkich zapisow na wydarzenia (dla admina)
        public List<EventRegistration> GetEventRegistrations()
        {
            var registrations = new List<EventRegistration>();
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT er.*, u.first_name, u.last_name, e.name as event_name 
                    FROM event_registrations er
                    JOIN users u ON er.user_id = u.id
                    JOIN events e ON er.event_id = e.id
                    ORDER BY er.registration_date DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        registrations.Add(new EventRegistration
                        {
                            Id = reader.GetInt32("id"),
                            UserId = reader.GetInt32("user_id"),
                            EventId = reader.GetInt32("event_id"),
                            ParticipationType = reader.GetString("participation_type"),
                            FoodPreference = reader.GetString("food_preference"),
                            Status = reader.GetString("status")
                        });
                    }
                }
            }
            
            return registrations;
        }

        // pobranie zgloszen uzytkownika z szczegolami
        public List<UserRegistrationDetail> GetUserRegistrationsWithDetails(int userId)
        {
            var registrations = new List<UserRegistrationDetail>();
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        er.id,
                        er.user_id,
                        er.event_id,
                        er.participation_type,
                        er.food_preference,
                        er.status,
                        er.registration_date,
                        e.name as event_name,
                        e.event_date
                    FROM event_registrations er
                    JOIN events e ON er.event_id = e.id
                    WHERE er.user_id = @userId
                    ORDER BY er.registration_date DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            registrations.Add(new UserRegistrationDetail
                            {
                                Id = reader.GetInt32("id"),
                                UserId = reader.GetInt32("user_id"),
                                EventId = reader.GetInt32("event_id"),
                                EventName = reader.GetString("event_name"),
                                ParticipationType = reader.GetString("participation_type"),
                                FoodPreference = reader.GetString("food_preference"),
                                Status = reader.GetString("status"),
                                RegistrationDate = reader.GetDateTime("registration_date"),
                                EventDate = reader.GetDateTime("event_date")
                            });
                        }
                    }
                }
            }
            
            return registrations;
        }

        // aktualizacja statusu rejestracji
        public bool UpdateRegistrationStatus(int registrationId, string status)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE event_registrations SET status = @status WHERE id = @id";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@id", registrationId);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // pobranie wszystkich uzytkownikow (dla admina)
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users ORDER BY registration_date DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Login = reader.GetString("login"),
                            Email = reader.GetString("email"),
                            Permissions = reader.GetString("permissions"),
                            RegistrationDate = reader.GetDateTime("registration_date")
                        });
                    }
                }
            }
            
            return users;
        }

        // usuniecie uzytkownika (dla admina)
        public bool DeleteUser(int userId)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // najpierw usun zgłoszenia uzytkownika
                    string deleteRegistrations = "DELETE FROM event_registrations WHERE user_id = @id";
                    using (var command1 = new MySqlCommand(deleteRegistrations, connection))
                    {
                        command1.Parameters.AddWithValue("@id", userId);
                        command1.ExecuteNonQuery();
                    }
                    
                    // potem usun uzytkownika
                    string deleteUser = "DELETE FROM users WHERE id = @id";
                    using (var command2 = new MySqlCommand(deleteUser, connection))
                    {
                        command2.Parameters.AddWithValue("@id", userId);
                        return command2.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // reset hasla uzytkownika (dla admina)
        public bool ResetUserPassword(int userId, string newPassword)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE users SET password = @password WHERE id = @id";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@password", newPassword);
                        command.Parameters.AddWithValue("@id", userId);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // dodanie nowego wydarzenia (dla admina)
        public bool AddEvent(Event eventItem)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO events (name, agenda, event_date, is_active)
                        VALUES (@name, @agenda, @eventDate, @isActive)";
                    
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", eventItem.Name);
                        command.Parameters.AddWithValue("@agenda", eventItem.Agenda);
                        command.Parameters.AddWithValue("@eventDate", eventItem.EventDate);
                        command.Parameters.AddWithValue("@isActive", eventItem.IsActive);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // usuniecie wydarzenia (dla admina)
        public bool DeleteEvent(int eventId)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // najpierw usun zgłoszenia na to wydarzenie
                    string deleteRegistrations = "DELETE FROM event_registrations WHERE event_id = @id";
                    using (var command1 = new MySqlCommand(deleteRegistrations, connection))
                    {
                        command1.Parameters.AddWithValue("@id", eventId);
                        command1.ExecuteNonQuery();
                    }
                    
                    // potem usun wydarzenie
                    string deleteEvent = "DELETE FROM events WHERE id = @id";
                    using (var command2 = new MySqlCommand(deleteEvent, connection))
                    {
                        command2.Parameters.AddWithValue("@id", eventId);
                        return command2.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }

    // klasa pomocnicza dla zgloszen z szczegolami
    public class UserRegistrationDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string ParticipationType { get; set; } = string.Empty;
        public string FoodPreference { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public DateTime EventDate { get; set; }
    }
}
using System.Collections.Generic;
using EventRegistrationApp.Models;

namespace EventRegistrationApp.Services
{
    public class AuthenticationService
    {
        private readonly DatabaseService databaseService;
        private readonly Dictionary<string, int> loginAttempts;
        private readonly Dictionary<string, bool> blockedUsers;

        public AuthenticationService(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
            this.loginAttempts = new Dictionary<string, int>();
            this.blockedUsers = new Dictionary<string, bool>();
        }

        public User? CurrentUser { get; private set; } // Zmiana na nullable

        public bool Login(string login, string password)
        {
            if (IsBlocked(login))
            {
                return false;
            }

            var user = databaseService.GetUserByLogin(login);
            
            if (user != null && user.Password == password)
            {
                CurrentUser = user;
                ResetLoginAttempts(login);
                return true;
            }

            IncrementLoginAttempts(login);
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        private void IncrementLoginAttempts(string login)
        {
            if (!loginAttempts.ContainsKey(login))
            {
                loginAttempts[login] = 0;
            }

            loginAttempts[login]++;

            if (loginAttempts[login] >= 3)
            {
                blockedUsers[login] = true;
            }
        }

        private void ResetLoginAttempts(string login)
        {
            if (loginAttempts.ContainsKey(login))
            {
                loginAttempts[login] = 0;
            }
            
            if (blockedUsers.ContainsKey(login))
            {
                blockedUsers[login] = false;
            }
        }

        private bool IsBlocked(string login)
        {
            return blockedUsers.ContainsKey(login) && blockedUsers[login];
        }

        public int GetRemainingAttempts(string login)
        {
            if (!loginAttempts.ContainsKey(login))
            {
                return 3;
            }

            return 3 - loginAttempts[login];
        }
    }
}
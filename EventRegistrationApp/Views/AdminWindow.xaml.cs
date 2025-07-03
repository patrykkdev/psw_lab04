using System.Linq;
using System.Windows;
using EventRegistrationApp.Models;
using EventRegistrationApp.Services;

namespace EventRegistrationApp.Views
{
    public partial class AdminMainWindow : Window
    {
        private readonly AuthenticationService authService;
        private readonly DatabaseService databaseService;

        public AdminMainWindow(AuthenticationService authService, DatabaseService databaseService)
        {
            InitializeComponent();
            this.authService = authService;
            this.databaseService = databaseService;
            
            InitializeWindow();
            LoadAllData();
        }

        private void InitializeWindow()
        {
            var user = authService.CurrentUser;
            WelcomeText.Text = $"Witaj, {user.FirstName} {user.LastName}!";
        }

        private void LoadAllData()
        {
            LoadUsers();
            LoadEvents();
            LoadRegistrations();
        }

        private void LoadUsers()
        {
            var users = databaseService.GetAllUsers();
            UsersDataGrid.ItemsSource = users;
        }

        private void LoadEvents()
        {
            var events = databaseService.GetActiveEvents();
            EventsDataGrid.ItemsSource = events;
        }

        private void LoadRegistrations()
        {
            var registrations = databaseService.GetEventRegistrations();
            RegistrationsDataGrid.ItemsSource = registrations;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            authService.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        // management uzytkownika
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(databaseService);
            if (registerWindow.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show($"Czy na pewno chcesz usunąć użytkownika {selectedUser.FirstName} {selectedUser.LastName}?", 
                                           "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    if (databaseService.DeleteUser(selectedUser.Id))
                    {
                        MessageBox.Show("Użytkownik został usunięty.", "Sukces", 
                                       MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił błąd podczas usuwania użytkownika.", "Błąd", 
                                       MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz użytkownika do usunięcia.", "Informacja", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                string newPassword = "temp123";
                
                if (databaseService.ResetUserPassword(selectedUser.Id, newPassword))
                {
                    MessageBox.Show($"Hasło zostało zresetowane na: {newPassword}", "Sukces", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas resetowania hasła.", "Błąd", 
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz użytkownika do resetowania hasła.", "Informacja", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshUsersButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        // management eventow
        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja dodawania wydarzeń będzie dostępna w przyszłych wersjach.", "Informacja", 
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja edycji wydarzeń będzie dostępna w przyszłych wersjach.", "Informacja", 
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja usuwania wydarzeń będzie dostępna w przyszłych wersjach.", "Informacja", 
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RefreshEventsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadEvents();
        }

        // management rejestracji
        private void ConfirmRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem is EventRegistration selectedRegistration)
            {
                if (databaseService.UpdateRegistrationStatus(selectedRegistration.Id, "Confirmed"))
                {
                    MessageBox.Show("Zapis został potwierdzony.", "Sukces", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRegistrations();
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas potwierdzania zapisu.", "Błąd", 
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz zapis do potwierdzenia.", "Informacja", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RejectRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationsDataGrid.SelectedItem is EventRegistration selectedRegistration)
            {
                if (databaseService.UpdateRegistrationStatus(selectedRegistration.Id, "Rejected"))
                {
                    MessageBox.Show("Zapis został odrzucony.", "Sukces", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRegistrations();
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas odrzucania zapisu.", "Błąd", 
                                   MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz zapis do odrzucenia.", "Informacja", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshRegistrationsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRegistrations();
        }
    }
}
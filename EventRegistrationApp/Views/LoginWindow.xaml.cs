// Views/LoginWindow.xaml.cs - Naprawiona wersja
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventRegistrationApp.Services;
using EventRegistrationApp.Views;

namespace EventRegistrationApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthenticationService authService;
        private readonly DatabaseService databaseService;

        public LoginWindow()
        {
            InitializeComponent();
            databaseService = new DatabaseService();
            authService = new AuthenticationService(databaseService);
            
            // inicjalizacja bazy danych
            try
            {
                databaseService.InitializeDatabase();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Błąd połączenia z bazą danych: {ex.Message}", 
                               "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // ustawianie focusu na login textbox
            this.Loaded += (s, e) => LoginTextBox.Focus();
            
           
            this.KeyDown += LoginWindow_KeyDown;
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";

            string login = LoginTextBox.Text.Trim();
            string password = ShowPasswordCheckBox.IsChecked == true ? 
                             PasswordTextBox.Text : PasswordBox.Password;

            // walidacja
            if (string.IsNullOrEmpty(login))
            {
                ErrorMessage.Text = "Wprowadź login!";
                LoginTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ErrorMessage.Text = "Wprowadź hasło!";
                if (ShowPasswordCheckBox.IsChecked == true)
                    PasswordTextBox.Focus();
                else
                    PasswordBox.Focus();
                return;
            }

            try
            {
                if (authService.Login(login, password))
                {
                    var user = authService.CurrentUser;
                    
                    if (user?.Permissions == "admin")
                    {
                        var adminWindow = new AdminMainWindow(authService, databaseService);
                        adminWindow.Show();
                    }
                    else if (user?.Permissions == "user")
                    {
                        var userWindow = new UserMainWindow(authService, databaseService);
                        userWindow.Show();
                    }
                    else
                    {
                        ErrorMessage.Text = "Nieprawidłowe uprawnienia użytkownika.";
                        return;
                    }
                    
                    this.Close();
                }
                else
                {
                    int remainingAttempts = authService.GetRemainingAttempts(login);
                    
                    if (remainingAttempts <= 0)
                    {
                        ErrorMessage.Text = "Konto zostało zablokowane po 3 nieudanych próbach logowania.";
                        LoginButton.IsEnabled = false;
                    }
                    else
                    {
                        ErrorMessage.Text = $"Nieprawidłowe dane logowania. Pozostało prób: {remainingAttempts}";
                        // czyszczenie pol
                        PasswordBox.Password = "";
                        PasswordTextBox.Text = "";
                        LoginTextBox.Focus();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Text = $"Błąd logowania: {ex.Message}";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registerWindow = new RegisterWindow(databaseService);
                registerWindow.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Błąd otwierania okna rejestracji: {ex.Message}", 
                               "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Focus();
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Focus();
        }
    }
}
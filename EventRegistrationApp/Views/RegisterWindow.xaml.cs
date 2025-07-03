using System.Windows;
using EventRegistrationApp.Models;
using EventRegistrationApp.Services;

namespace EventRegistrationApp.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly DatabaseService databaseService;

        public RegisterWindow(DatabaseService databaseService)
        {
            InitializeComponent();
            this.databaseService = databaseService;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";

            // walidacja
            if (!ValidationService.IsValidName(FirstNameTextBox.Text))
            {
                ErrorMessage.Text = "Imię musi mieć co najmniej 2 znaki.";
                return;
            }

            if (!ValidationService.IsValidName(LastNameTextBox.Text))
            {
                ErrorMessage.Text = "Nazwisko musi mieć co najmniej 2 znaki.";
                return;
            }

            if (!ValidationService.IsValidLogin(LoginTextBox.Text))
            {
                ErrorMessage.Text = "Login musi mieć co najmniej 3 znaki.";
                return;
            }

            if (!ValidationService.IsValidEmail(EmailTextBox.Text))
            {
                ErrorMessage.Text = "Podaj prawidłowy adres e-mail.";
                return;
            }

            if (!ValidationService.IsValidPassword(PasswordBox.Password))
            {
                ErrorMessage.Text = "Hasło musi mieć co najmniej 6 znaków.";
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ErrorMessage.Text = "Hasła muszą być identyczne.";
                return;
            }

            // sprawdzanie czy uzytkownik istnieje
            if (databaseService.GetUserByLogin(LoginTextBox.Text) != null)
            {
                ErrorMessage.Text = "Użytkownik o podanym loginie już istnieje.";
                return;
            }

            // tworzenie nowego uzytkonika
            var newUser = new User
            {
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                Login = LoginTextBox.Text.Trim(),
                Password = PasswordBox.Password,
                Email = EmailTextBox.Text.Trim(),
                Permissions = "user"
            };

            if (databaseService.CreateUser(newUser))
            {
                MessageBox.Show("Rejestracja zakończona pomyślnie! Możesz się teraz zalogować.", 
                               "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                ErrorMessage.Text = "Wystąpił błąd podczas rejestracji. Spróbuj ponownie.";
            }
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
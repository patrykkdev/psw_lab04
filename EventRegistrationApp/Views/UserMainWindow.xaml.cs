using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EventRegistrationApp.Models;
using EventRegistrationApp.Services;

namespace EventRegistrationApp.Views
{
    public partial class UserMainWindow : Window
    {
        private readonly AuthenticationService authService;
        private readonly DatabaseService databaseService;
        private List<Event>? availableEvents; // nullable zeby uniknac ostrzezen

        public UserMainWindow(AuthenticationService authService, DatabaseService databaseService)
        {
            InitializeComponent();
            this.authService = authService;
            this.databaseService = databaseService;
            
            this.Loaded += UserMainWindow_Loaded;
        }

        private void UserMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitializeWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"blad inicjalizacji: {ex.Message}", 
                               "blad", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeWindow()
        {
            // ustawienie tekstu powitalnego
            var user = authService?.CurrentUser;
            if (user != null)
            {
                WelcomeText.Text = $"Witaj, {user.FirstName} {user.LastName}!";
            }

            // zaladowanie wydarzen i zgloszen
            LoadEvents();
            LoadMyRegistrations();
            
            // ustawienie domyslnych wartosci
            ParticipationTypeComboBox.SelectedIndex = 0;
            FoodPreferenceComboBox.SelectedIndex = 0;
            
            // wyczyszczenie komunikatow
            RegistrationMessage.Text = "";
        }

        private void LoadEvents()
        {
            try
            {
                // proba zaladowania z bazy danych
                availableEvents = databaseService?.GetActiveEvents();
                
                if (availableEvents != null && availableEvents.Count > 0)
                {
                    // zaladowanie rzeczywistych wydarzen z bazy
                    SetupEventComboBox(availableEvents);
                    RegistrationMessage.Text = $"zaladowano {availableEvents.Count} wydarzen z bazy danych";
                    RegistrationMessage.Foreground = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    LoadTestEvents();
                }
            }
            catch (Exception ex)
            {
         
                RegistrationMessage.Text = $"blad bazy danych: {ex.Message} - uzywam danych testowych";
                RegistrationMessage.Foreground = System.Windows.Media.Brushes.Orange;
                LoadTestEvents();
            }
        }

        private void LoadTestEvents()
        {
            // dane testowe jesli baza nie dziala
            availableEvents = new List<Event>
            {
                new Event 
                { 
                    Id = 1, 
                    Name = "Konferencja IT 2024", 
                    Agenda = "najnowsze trendy w it, ai i machine learning oraz nowoczesne technologie chmurowe", 
                    EventDate = DateTime.Now.AddDays(30) 
                },
                new Event 
                { 
                    Id = 2, 
                    Name = "Workshop C#", 
                    Agenda = "praktyczne zastosowanie c# w projektach enterprise, linq, async/await, design patterns", 
                    EventDate = DateTime.Now.AddDays(45) 
                },
                new Event 
                { 
                    Id = 3, 
                    Name = "Hackathon 2024", 
                    Agenda = "48-godzinny maraton programistyczny z nagrodami dla najlepszych zespolow", 
                    EventDate = DateTime.Now.AddDays(60) 
                }
            };

            SetupEventComboBox(availableEvents);
            
            RegistrationMessage.Text = "uzywam danych testowych - brak polaczenia z baza";
            RegistrationMessage.Foreground = System.Windows.Media.Brushes.Blue;
        }

        private void SetupEventComboBox(List<Event> events)
        {
            // wyczyszczenie poprzednich danych
            EventComboBox.ItemsSource = null;
            EventComboBox.Items.Clear();
            
            // dodanie wydarzen z ustawieniem wyswietlania
            foreach (var eventItem in events)
            {
                var comboBoxItem = new ComboBoxItem
                {
                    Content = eventItem.Name,
                    Tag = eventItem // przechowujemy caly obiekt w Tag
                };
                EventComboBox.Items.Add(comboBoxItem);
            }
        }

        // ladowanie rzeczywistych zgloszen uzytkownika
        private void LoadMyRegistrations()
        {
            try
            {
                if (authService?.CurrentUser == null)
                {
                    MyRegistrationsDataGrid.ItemsSource = null;
                    return;
                }

                // pobranie zgloszen z bazy danych lub testowych
                var userRegistrations = GetUserRegistrations();
                
                if (userRegistrations.Count > 0)
                {
                    MyRegistrationsDataGrid.ItemsSource = userRegistrations;
                    
                    // aktualizacja tytulu zakladki - usuwamy problematyczne Parent
                    UpdateMyRegistrationsTabTitle(userRegistrations.Count);
                }
                else
                {
                    MyRegistrationsDataGrid.ItemsSource = null;
                    UpdateMyRegistrationsTabTitle(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"blad ladowania zgloszen: {ex.Message}", 
                               "blad", MessageBoxButton.OK, MessageBoxImage.Warning);
                MyRegistrationsDataGrid.ItemsSource = null;
            }
        }

        // pomocnicza metoda do aktualizacji tytulu zakladki
        private void UpdateMyRegistrationsTabTitle(int count)
        {
            try
            {
                // znajdz TabControl i zakladke przez FindName
                if (this.FindName("MyRegistrationsTab") is TabItem tab)
                {
                    tab.Header = count > 0 ? $"📋 Moje Zgłoszenia ({count})" : "📋 Moje Zgłoszenia";
                }
            }
            catch
            {
                // ignoruj bledy aktualizacji tytulu
            }
        }

        // pobranie zgloszen uzytkownika z bazy danych lub testowych
        private List<UserRegistrationView> GetUserRegistrations()
        {
            var userRegistrations = new List<UserRegistrationView>();
            
            try
            {
                if (databaseService == null || authService?.CurrentUser == null)
                    return userRegistrations;

                // proba pobrania z bazy danych
                var userDetails = databaseService.GetUserRegistrationsWithDetails(authService.CurrentUser.Id);
                
                if (userDetails.Count > 0)
                {
                    // mapowanie na widok dla DataGrid
                    foreach (var detail in userDetails)
                    {
                        userRegistrations.Add(new UserRegistrationView
                        {
                            EventName = detail.EventName,
                            ParticipationType = detail.ParticipationType,
                            FoodPreference = detail.FoodPreference,
                            Status = detail.Status,
                            RegistrationDate = detail.RegistrationDate.ToString("dd.MM.yyyy HH:mm")
                        });
                    }
                }
                else
                {
                    if (availableEvents != null && availableEvents.Count > 0)
                    {
                        // mozna dodac przykladowe zgloszenie testowe
                        userRegistrations.Add(new UserRegistrationView
                        {
                            EventName = "Test Event (brak polaczenia z baza)",
                            ParticipationType = "Sluchacz",
                            FoodPreference = "Bez preferencji",
                            Status = "Pending",
                            RegistrationDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // w przypadku bledu zwroc puste zgłoszenia
                System.Diagnostics.Debug.WriteLine($"Blad pobierania zgloszen: {ex.Message}");
            }
            
            return userRegistrations;
        }

        private void EventComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (EventComboBox.SelectedItem is ComboBoxItem selectedItem && 
                    selectedItem.Tag is Event selectedEvent)
                {
                    AgendaTextBlock.Text = selectedEvent.Agenda ?? "brak opisu wydarzenia";
                    EventDateTextBlock.Text = selectedEvent.EventDate.ToString("dd.MM.yyyy HH:mm");
                    
                    // komunikat o wyborze
                    RegistrationMessage.Text = $"wybrano: {selectedEvent.Name}";
                    RegistrationMessage.Foreground = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    AgendaTextBlock.Text = "wybierz wydarzenie aby zobaczyc szczegoly";
                    EventDateTextBlock.Text = "-";
                    RegistrationMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                RegistrationMessage.Text = $"blad wyswietlania szczegolow: {ex.Message}";
                RegistrationMessage.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void RegisterForEventButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // wyczyszczenie poprzednich komunikatow
                RegistrationMessage.Text = "";
                RegistrationMessage.Foreground = System.Windows.Media.Brushes.Red;

                // sprawdzenie czy uzytkownik jest zalogowany
                if (authService?.CurrentUser == null)
                {
                    RegistrationMessage.Text = "blad: nie jestes zalogowany.";
                    return;
                }

                // sprawdzenie czy wybrano wydarzenie
                if (EventComboBox.SelectedItem == null || 
                    !(EventComboBox.SelectedItem is ComboBoxItem selectedItem) ||
                    !(selectedItem.Tag is Event selectedEvent))
                {
                    RegistrationMessage.Text = "wybierz wydarzenie.";
                    return;
                }

                // sprawdzenie czy wybrano typ uczestnictwa
                if (ParticipationTypeComboBox.SelectedItem == null)
                {
                    RegistrationMessage.Text = "wybierz typ uczestnictwa.";
                    return;
                }

                // sprawdzenie czy wybrano preferencje zywieniowe
                if (FoodPreferenceComboBox.SelectedItem == null)
                {
                    RegistrationMessage.Text = "wybierz preferencje zywieniowe.";
                    return;
                }

                // pobranie wybranych wartosci z zabezpieczeniem
                var participationType = ((ComboBoxItem)ParticipationTypeComboBox.SelectedItem).Content?.ToString() ?? "sluchacz";
                var foodPreference = ((ComboBoxItem)FoodPreferenceComboBox.SelectedItem).Content?.ToString() ?? "bez preferencji";

                // proba rejestracji w bazie danych
                if (TryRegisterInDatabase(selectedEvent, participationType, foodPreference))
                {
                    // sukces - rejestracja w bazie
                    RegistrationMessage.Foreground = System.Windows.Media.Brushes.Green;
                    RegistrationMessage.Text = "pomyslnie zarejestrowano na wydarzenie! oczekuj na potwierdzenie.";
                    
                    // pokazanie komunikatu sukcesu
                    MessageBox.Show("rejestracja zostala wyslana!", "sukces", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // sukces testowy - brak polaczenia z baza
                    RegistrationMessage.Foreground = System.Windows.Media.Brushes.Blue;
                    RegistrationMessage.Text = $"rejestracja testowa: '{selectedEvent.Name}' jako '{participationType}' z preferencjami '{foodPreference}'";
                    
                    // pokazanie komunikatu testowego
                    MessageBox.Show("rejestracja testowa zakonczona (brak polaczenia z baza)!", "test", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // resetowanie formularza
                ResetForm();
                LoadMyRegistrations();
                
            }
            catch (Exception ex)
            {
                RegistrationMessage.Text = $"blad rejestracji: {ex.Message}";
                RegistrationMessage.Foreground = System.Windows.Media.Brushes.Red;
                MessageBox.Show($"szczegoly bledu: {ex.ToString()}", "blad", 
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool TryRegisterInDatabase(Event selectedEvent, string participationType, string foodPreference)
        {
            try
            {
                if (databaseService == null || authService?.CurrentUser == null)
                    return false;

                var registration = new EventRegistration
                {
                    UserId = authService.CurrentUser.Id,
                    EventId = selectedEvent.Id,
                    ParticipationType = participationType,
                    FoodPreference = foodPreference,
                    Status = "pending"
                };

                return databaseService.RegisterForEvent(registration);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"blad zapisu do bazy: {ex.Message}", "blad bazy", 
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void ResetForm()
        {
            try
            {
                EventComboBox.SelectedIndex = -1;
                ParticipationTypeComboBox.SelectedIndex = 0;
                FoodPreferenceComboBox.SelectedIndex = 0;
                AgendaTextBlock.Text = "wybierz wydarzenie aby zobaczyc szczegoly";
                EventDateTextBlock.Text = "-";
            }
            catch
            {
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("czy na pewno chcesz sie wylogowac?", 
                                           "potwierdzenie", 
                                           MessageBoxButton.YesNo, 
                                           MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    authService?.Logout();
                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"blad wylogowania: {ex.Message}", 
                               "blad", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // model widoku dla DataGrid
    public class UserRegistrationView
    {
        public string EventName { get; set; } = string.Empty;
        public string ParticipationType { get; set; } = string.Empty;
        public string FoodPreference { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string RegistrationDate { get; set; } = string.Empty;
    }
}

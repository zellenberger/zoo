using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using Microsoft.Win32;
using People;
using Reproducers;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constant autosave file.
        /// </summary>
        private const string AutoSaveNameFile = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            #if DEBUG
            this.Title += "[DEBUG]";
            #endif
        }

        /// <summary>
        /// The admit guest button click.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guest = new Guest("Ethel", 42, 30.0m, WalletColor.Salmon, Gender.Female, new Account());
                Ticket ticket = this.comoZoo.SellTicket(guest);
                this.comoZoo.AddGuest(guest, ticket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.GetType().ToString() + ": " + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// The feed animal button click.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (guest != null && animal != null)
            {
                guest.FeedAnimal(animal);
            }
            else
            {
                MessageBox.Show("Must select both a guest and an animal to feed an animal");
            }

            this.guestListBox.SelectedItem = guest;
            this.animalListBox.SelectedItem = animal;
        }

        /// <summary>
        /// The button to increase the temperature.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The button to decrease the temperature.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The save zoo method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);
            this.SetWindowTitle(fileName);
        }

        /// <summary>
        /// The set window title method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = $"Object-Oriented Programming 2: Zoo [{Path.GetFileName(fileName)}]";
        }

        /// <summary>
        /// The load zoo method.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Returns result.</returns>
        private bool LoadZoo(string fileName)
        {
            bool result = true;
            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.SetWindowTitle(fileName);
                this.AttachDelegates();
            }
            catch
            {
                result = false;
                MessageBox.Show("The file could not be loaded.");
            }

            return result;
        }

        /// <summary>
        /// The clear window method.
        /// </summary>
        private void ClearWindow()
        {
            animalListBox.Items.Clear();
            guestListBox.Items.Clear();
        }

        /// <summary>
        /// The Attach Delegates.
        /// </summary>
        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (double previousTemp, double currentTemp) =>
            {
                temperatureBorder.Height = 2 * this.comoZoo.BirthingRoomTemperature;
                temperatureLabel.Content = string.Format("{0:0.0 °F}", this.comoZoo.BirthingRoomTemperature);
                double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(colorLevel),
                    Convert.ToByte(255 - colorLevel),
                    Convert.ToByte(255 - colorLevel)));
            };

            this.comoZoo.OnAddGuest = (Guest guest) =>
            {
                this.guestListBox.Items.Add(guest);
                guest.OnTextChange += this.UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest = (Guest guest) =>
            {
                this.guestListBox.Items.Remove(guest);
                guest.OnTextChange -= this.UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = (Animal animal) =>
            {
                this.animalListBox.Items.Add(animal);
                animal.OnTextChange += this.UpdateAnimalDisplay;
            };
            
            this.comoZoo.OnRemoveAnimal = (Animal animal) =>
            {
                this.animalListBox.Items.Remove(animal);
                animal.OnTextChange -= this.UpdateAnimalDisplay;
            };
            this.comoZoo.OnDeserialized();
        }

        /// <summary>
        /// The Update Guest Display method.
        /// </summary>
        /// <param name="guest">The guest parameter.</param>
        private void UpdateGuestDisplay(Guest guest)
        {
            int index = this.guestListBox.Items.IndexOf(guest);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() => 
                {
                    //// disconnect the guest
                    this.guestListBox.Items.RemoveAt(index);
                    //// create new guest item in the same spot
                    this.guestListBox.Items.Insert(index, guest);
                    //// re-select the guest
                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                });
            }
        }

        /// <summary>
        /// The Update Animal Display.
        /// </summary>
        /// <param name="animal">The animal.</param>
        private void UpdateAnimalDisplay(Animal animal)
        {
            int index = this.animalListBox.Items.IndexOf(animal);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() => 
                {
                    // disconnect the guest.
                    this.animalListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot.
                    this.animalListBox.Items.Insert(index, animal);

                    // re-select the guest.
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                });
            }
        }

        /// <summary>
        /// This is the window method that loads the zoo.
        /// </summary>
        /// <param name="sender">Send to the next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = this.LoadZoo(AutoSaveNameFile);
            if (result == false)
            {
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
            }

            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
            this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));
        }

        /// <summary>
        /// The button to add a new animal to the zoo.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType type = (AnimalType)this.animalTypeComboBox.SelectedItem;

                Animal animal = AnimalFactory.CreateAnimal(type, "Oggy Boggy", 2, 35, Gender.Male);

                AnimalWindow window = new AnimalWindow(animal);

                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    // Add Joe to the zoo's animal list.
                    this.comoZoo.AddAnimal(animal);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal must be selected before adding an animal to the zoo");
            }
        }

        /// <summary>
        /// The button to remove an animal from the zoo.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal == null)
            {
                MessageBox.Show("Select an animal to remove.");
            }
            else if (MessageBox.Show(string.Format("Are you sure you want to remove an animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.comoZoo.RemoveAnimal(animal);
            }
        }

        /// <summary>
        /// The button to remove an animal from the zoo.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest == null)
            {
                MessageBox.Show("Select a guest to remove.");
            }
            else if (MessageBox.Show(string.Format("Are you sure you want to remove a guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.comoZoo.RemoveGuest(guest);
            }
        }

        /// <summary>
        /// The button to add a guest from the zoo.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            Guest guest = new Guest("John", 50, 50.0m, WalletColor.Black, Gender.Male, account);
            Window guestWindow = new GuestWindow(guest);
            try
            {
                guestWindow.ShowDialog();
                if (guestWindow.DialogResult == true)
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);
                    this.comoZoo.AddGuest(guest, ticket);
                }
                else
                {
                    // Do nothing.
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The animal list box double click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void AnimalListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Animal animal = (Animal)animalListBox.SelectedItem;
            AnimalWindow animalWindow = new AnimalWindow(animal);
            animalWindow.ShowDialog();
            if (animalWindow.DialogResult == true)
            {
                if (animal.IsPregnant == true)
                {
                    this.comoZoo.RemoveAnimal(animal);
                    this.comoZoo.AddAnimal(animal);
                }
            }
        }

        /// <summary>
        /// The guest list box button double click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void GuestListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                Window guestWindow = new GuestWindow(guest);
                guestWindow.ShowDialog();

                if (guestWindow.DialogResult == true)
                {
                }
            }
        }

        /// <summary>
        /// The show cage button click.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Window cageWindow = new CageWindow(this.comoZoo.FindCage(animal.GetType()));
                cageWindow.Show();
            }
            else
            {
                MessageBox.Show("Select an animal to show.");
            }
        }

        /// <summary>
        /// The adopt animal button click.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void AdoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            Guest guest = this.comoZoo.FindGuest(g => g.AdoptedAnimal == null);

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                if (guest != null)
                {
                    guest.AdoptedAnimal = animal;
                    cage.Add(guest);
                }
                else
                {
                    MessageBox.Show("There are no guests available to adopt the animal.");
                }
            }
            else
            {
                MessageBox.Show("Select an animal to adopt.");
            }
        }

        /// <summary>
        /// The un-adopt animal button.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void UnAdoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest != null && animal != null)
            {
                if (animal == guest.AdoptedAnimal)
                {
                    Cage cage = this.comoZoo.FindCage(guest.AdoptedAnimal.GetType());
                    guest.AdoptedAnimal = null;
                    cage.Remove(guest);
                    animal.OnHunger = null;
                }
                else
                {
                    MessageBox.Show($"the animal named {animal.Name} is not their animal.");
                }
            }
            else
            {
                MessageBox.Show("Please select a guest and animal.");
            }
        }

        /// <summary>
        /// The change move behavior button click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void changeMoveBehavior_click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            object behavior = this.changeMoveBehaviorComboBox.SelectedItem;

            if (animal != null && behavior != null)
            {
                animal.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)behavior);
            }
            else
            {
                MessageBox.Show("Please select an animal and a movement type.");
            }
        }

        /// <summary>
        /// The birth animal button click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void BirthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthAnimal();
        }

        /// <summary>
        /// The save button click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.SaveZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// The load button click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.ClearWindow();
                this.LoadZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// The restart button click method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.ClearWindow();
                this.comoZoo = Zoo.NewZoo();
                this.SetWindowTitle("Object-Oriented Programming 2: Zoo");
                this.AttachDelegates();
            }
        }

        /// <summary>
        /// This is the window method closing the zoo.
        /// </summary>
        /// <param name="sender">Send to the next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo(AutoSaveNameFile);
        }
    }
}
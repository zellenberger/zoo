using System;
using System.Windows;
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// Animal window animal.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// /// <param name="guest">Returns guest.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Loads the animal window.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.nameTextBox.Text = this.guest.Name.ToString();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.guest.Gender;
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.Color;
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            moneyAmountComboBox.Items.Add(1);
            moneyAmountComboBox.Items.Add(5);
            moneyAmountComboBox.Items.Add(10);
            moneyAmountComboBox.Items.Add(20);
            moneyAmountComboBox.SelectedItem = moneyAmountComboBox.Items[0];
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            accountComboBox.Items.Add(1);
            accountComboBox.Items.Add(5);
            accountComboBox.Items.Add(10);
            accountComboBox.Items.Add(20);
            accountComboBox.Items.Add(50);
            accountComboBox.Items.Add(100);
            accountComboBox.SelectedItem = moneyAmountComboBox.Items[0];
        }

        /// <summary>
        /// The ok button click.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// The name's lost focus event.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = nameTextBox.Text;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The name's lost focus event.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void AgeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = ageTextBox.Text;
            int age = int.Parse(text);
            try
            {
                this.guest.Age = age;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// The name's lost focus event.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void GenderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// The name's lost focus event.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void WalletColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.guest.Wallet.Color = (WalletColor)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// The add money button click.
        /// </summary>
        /// <param name="sender">Sends to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void AddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            decimal amount = decimal.Parse(moneyAmountComboBox.Text);
            this.guest.Wallet.AddMoney(amount);
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// The subtract money button click.
        /// </summary>
        /// <param name="sender">Sends to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void SubtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            decimal amount = decimal.Parse(moneyAmountComboBox.Text);
            this.guest.Wallet.RemoveMoney(amount);
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// The add account button click.
        /// </summary>
        /// <param name="sender">Sends to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.AddMoney(decimal.Parse(accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// The subtract account button click.
        /// </summary>
        /// <param name="sender">Sends to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void SubtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.RemoveMoney(decimal.Parse(accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }
    }
}

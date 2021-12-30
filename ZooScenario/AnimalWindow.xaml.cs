using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml.
    /// </summary>
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// Animal window animal.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Initializes a new instance of the AnimalWindow class.
        /// </summary>
        /// /// <param name="animal">Returns animal.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        /// <summary>
        /// Loads the animal window.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.animal.Name;
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.animal.Gender;

            if (this.animal.IsPregnant != false)
            {
                this.pregnancyStatusLabel.Content = "Yes";
            }
            else
            {
                this.pregnancyStatusLabel.Content = "No";
            }
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
                this.animal.Name = nameTextBox.Text;
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
                this.animal.Age = age;
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
        private void WeightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = weightTextBox.Text;
            double weight = double.Parse(text);
            try
            {
                this.animal.Weight = weight;
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
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;
            this.makePregnantButton.IsEnabled = this.animal.Gender == Gender.Female ? true : false;
        }

        /// <summary>
        /// The make pregnant in the animal window.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void MakePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Gender)this.genderComboBox.SelectedItem != Gender.Male)
            {
                makePregnantButton.IsEnabled = false;
                this.animal.MakePregnant();
            }

            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
        }
    }
}

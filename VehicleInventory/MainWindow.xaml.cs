// Author: Shibin Shaji
// Date Created: Oct 19, 2025
// Description: This program manages a list of vehicles.
// Date Modifed: Nov 15, 2025
using CarList.Data;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarList
{
    /// <summary>
    /// Contain all car and plane objects added to the inventory
    /// </summary>
    public partial class MainWindow : Window
    {
        // List<Vehicle> listOfCars = new List<Vehicle>();
        /// <summary>
        /// Initialize main window, loads defaul values
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            PopulateYears();
            SetDefaults();
            //displayCars.ItemsSource = Vehicle.List;
            CalculateStatistics();

            UpdateStatus("Welcome!");
        }

        /// <summary>
        /// Populate the year combo with last 50 years.
        /// </summary>
        public void PopulateYears()
        {
            var currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= currentYear - 50; year--)
            {
                ComboYear.Items.Add(year);
            }
        }

        /// <summary>
        /// Setting all the inputs to default state including combo boxes and text fields
        /// </summary>
        public void SetDefaults()
        {
            ComboMake.SelectedIndex = 0;
            textModel.Clear();
            ComboYear.SelectedIndex = 0;
            textPrice.Clear();
            checkIsNew.IsChecked = false;
            displayCars.SelectedIndex = -1;
            ComboEngineCount.SelectedIndex = 0;
            TextCapacity.Clear();
            TextSpan.Clear();
            ComboMake.Focus();
            CommentBox.Content = "Success!";
        }

        /// <summary>
        /// Event hanlder for reset button which clear all fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }
        /// <summary>
        /// Event that remove a vehicle from listview 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = displayCars.SelectedIndex;
            if (selectedIndex == -1)
                    {
                CommentBox.Content = "Please select a vehicle to remove.";
                return;
            }          

            bool remove = JsonManager.RemoveIndex(selectedIndex);
            if (remove)
            {
                displayCars.Items.RemoveAt(selectedIndex);
                CalculateStatistics();
                SetDefaults();
                CommentBox.Content = "Vehicle removed successfully";
            }
            else { CommentBox.Content = "Error: Unsuccessfull"; }
               
        }
     

        /// <summary>
        /// Event that trigger the car and plane entering and updating process including data validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EnterButton_Click(object sender, RoutedEventArgs e)
        {

            string inputMake = ComboMake.Text;
            string inputModel = textModel.Text;
            int inputYear;
            decimal inputPrice;
            bool inputStatus = checkIsNew.IsChecked == true;


            if (radioCar.IsChecked == true)
            {

                if (!int.TryParse(ComboYear.Text, out inputYear))
                {
                    CommentBox.Content = "Please enter a valid year";
                    return;
                }

                else if (!decimal.TryParse(textPrice.Text, out inputPrice))
                {
                    CommentBox.Content = "Please enter a valid price";
                    return;
                }

                try
                {

                    if (displayCars.SelectedIndex == -1)
                    {

                        var addCar = new Car(inputMake, inputModel, inputYear, inputPrice, inputStatus);

                        Vehicle.List.Add(addCar);
                        displayCars.Items.Refresh();
                        CalculateStatistics();
                        SetDefaults();

                    }
                    else
                    {
                        Car clickedCar = (Car)displayCars.SelectedItem;
                        clickedCar.Make = inputMake;
                        clickedCar.Model = inputModel;
                        clickedCar.Year = inputYear;
                        clickedCar.Price = inputPrice;
                        clickedCar.NewStatus = inputStatus;
                        CommentBox.Content = "Car updated!";
                        CalculateStatistics();
                        displayCars.Items.Refresh();
                    }
                }
                catch (InvalidCastException ex)
                {
                    UpdateStatus("Casting error occurred" + ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    UpdateStatus("Model cannot be empty" + ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    UpdateStatus("Price cannot be negative" + ex.Message);
                }
                catch (Exception ex)
                {
                    UpdateStatus("Error occurred:" + ex.Message);
                }
            }
            else if (radioPlane.IsChecked == true)
            {
                int inputEngineCount;
                decimal inputCargoCapacity;
                decimal inputWingsSpan;
                if (!int.TryParse(ComboYear.Text, out inputYear))
                {
                    CommentBox.Content = "Please enter a valid year";
                    return;
                }

                else if (!decimal.TryParse(textPrice.Text, out inputPrice))
                {
                    CommentBox.Content = "Please enter a valid price";
                    return;
                }
                if (!int.TryParse(ComboEngineCount.Text, out inputEngineCount))
                {
                    CommentBox.Content = "Please enter a valid engine count";
                    return;
                }

                else if (!decimal.TryParse(TextCapacity.Text, out inputCargoCapacity))
                {
                    CommentBox.Content = "Please enter a valid cargo capacity value";
                    return;
                }
                else if (!decimal.TryParse(TextSpan.Text, out inputWingsSpan))
                {
                    CommentBox.Content = "Please enter a valid wings span value";
                    return;
                }
                try
                {

                    if (displayCars.SelectedIndex == -1)
                    {
                        var addPlane = new Plane(inputMake, inputModel, inputYear, inputPrice, inputStatus, inputEngineCount, inputCargoCapacity, inputWingsSpan);

                        Vehicle.List.Add(addPlane);
                        CalculateStatistics();
                        displayCars.Items.Refresh();

                        SetDefaults();
                    }
                    else
                    {
                        Plane clickedPlane = (Plane)displayCars.SelectedItem;
                        clickedPlane.Make = inputMake;
                        clickedPlane.Model = inputModel;
                        clickedPlane.Year = inputYear;
                        clickedPlane.Price = inputPrice;
                        clickedPlane.NewStatus = inputStatus;
                        clickedPlane.EngineCount = inputEngineCount;
                        clickedPlane.CargoCapacity = inputCargoCapacity;
                        clickedPlane.WingsSpan = inputWingsSpan;
                        CommentBox.Content = "Plane updated!";
                        CalculateStatistics();
                        displayCars.Items.Refresh();
                    }
                }
                catch (InvalidCastException ex)
                {
                    UpdateStatus("Casting error occurred" + ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    UpdateStatus("Model/Cargo capacity/Wings span cannot be empty" + ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    UpdateStatus("Price cannot be negative" + ex.Message);
                }
                catch (Exception ex)
                {
                    UpdateStatus("Error occurred:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Event that trigger when user selects a car or a plane from the list for modification or viewing purpose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void displayCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (displayCars.SelectedItem == null)
                return;

            Vehicle selectedVehicle = (Vehicle)displayCars.SelectedItem;

            if (selectedVehicle.Type == "Plane")
            {
                Plane planeClicked = (Plane)selectedVehicle;

                radioPlane.IsChecked = true;
                ComboMake.Text = planeClicked.Make;
                textModel.Text = planeClicked.Model;
                ComboYear.Text = planeClicked.Year.ToString();
                textPrice.Text = planeClicked.Price.ToString();
                checkIsNew.IsChecked = planeClicked.NewStatus;
                ComboEngineCount.Text = planeClicked.EngineCount.ToString();
                TextCapacity.Text = planeClicked.CargoCapacity.ToString();
                TextSpan.Text = planeClicked.WingsSpan.ToString();

                LabelCount.Visibility = Visibility.Visible;
                ComboEngineCount.Visibility = Visibility.Visible;
                LabelCargo.Visibility = Visibility.Visible;
                TextCapacity.Visibility = Visibility.Visible;
                LabelSpan.Visibility = Visibility.Visible;
                TextSpan.Visibility = Visibility.Visible;
            }
            else if (selectedVehicle.Type == "Car")
            {
                Car carClicked = (Car)selectedVehicle;

                radioCar.IsChecked = true;
                ComboMake.Text = carClicked.Make;
                textModel.Text = carClicked.Model;
                ComboYear.Text = carClicked.Year.ToString();
                textPrice.Text = carClicked.Price.ToString();
                checkIsNew.IsChecked = carClicked.NewStatus;

                LabelCount.Visibility = Visibility.Hidden;
                ComboEngineCount.Visibility = Visibility.Hidden;
                LabelCargo.Visibility = Visibility.Hidden;
                TextCapacity.Visibility = Visibility.Hidden;
                LabelSpan.Visibility = Visibility.Hidden;
                TextSpan.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Update status messages when a change happens in the program and time.
        /// </summary>
        /// <param name="message">The new message</param>
        private void UpdateStatus(string message)
        {
            CommentBox.Content = DateTime.Now.ToString("HH:mm:ss") + "   " + message;
        }

        /// <summary>
        /// Updates status messages switching between tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = (TabControl)sender;
            if (tabControl.SelectedItem == tabViewInventory && displayCars.SelectedIndex == -1)
            {
                RefreshList();
                UpdateStatus("Inventory list viewed");
            }
            else if (tabControl.SelectedItem == tabStatistics)
            {
                UpdateStatus("Statistics Viewed");
                
            }
            else if (tabControl.SelectedItem == tabAddVehicle)
            {
                UpdateStatus("Welcome! Add a vehicle");
            }
        }
        /// <summary>
        /// When a radio button is selected, it show or hide the related properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaneChanged(object sender, RoutedEventArgs e)
        {
            if (radioPlane.IsChecked == true)
            {
                LabelCount.Visibility = Visibility.Visible;
                ComboEngineCount.Visibility = Visibility.Visible;
                LabelCargo.Visibility = Visibility.Visible;
                TextCapacity.Visibility = Visibility.Visible;
                LabelSpan.Visibility = Visibility.Visible;
                TextSpan.Visibility = Visibility.Visible;
            }
            else
            {
                LabelCount.Visibility = Visibility.Hidden;
                ComboEngineCount.Visibility = Visibility.Hidden;
                LabelCargo.Visibility = Visibility.Hidden;
                TextCapacity.Visibility = Visibility.Hidden;
                LabelSpan.Visibility = Visibility.Hidden;
                TextSpan.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Method calculates number of vehicles in the list and adds total price of all vehicles.
        /// </summary>
        public void CalculateStatistics()
        {
            int totalVehicles = 0;
            decimal totalCost = 0;
            decimal averageCost = 0;

            for (int i = 0; i < Vehicle.List.Count; i++)
            {
                Vehicle item = Vehicle.List[i];

                if (item != null)
                {
                    totalVehicles = totalVehicles + 1;


                    if (item.Price >= 0)
                    {
                        totalCost = totalCost + item.Price;
                        averageCost = totalCost / totalVehicles;
                    }
                }
            }


            OutputTotalVehicle.Text = totalVehicles.ToString();
            OutputCostTotal.Text = totalCost.ToString("C");
            OutputAverage.Text = averageCost.ToString("C");

        }
        /// <summary>
        /// Clear the current items and refreshes the ListView that displays vehicles
        /// </summary>
        private void RefreshList()
        {
            displayCars.Items.Clear();
            var currentVehicles = Vehicle.List;
            foreach (var vehicle in currentVehicles)
            {
                displayCars.Items.Add(vehicle);
            }

        }
        /// <summary>
        /// Displays help information for this software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpAboutMenu_Click(object sender, RoutedEventArgs e)
        {
            string message =
                "Thank you for using this software\n" +
                "Version: 1.0\n" +
                "Developed as part of learning WPF";
                

            MessageBox.Show(message, "About", MessageBoxButton.OK);
        }
        /// <summary>
        /// Removes all vehicle from the listview 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAll_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove all vehicles?", "Confirm Remove All",MessageBoxButton.YesNo );

            if (result == MessageBoxResult.Yes)
            {
                if (JsonManager.RemoveAllVehicles())
                {
                    displayCars.Items.Clear();
                    CalculateStatistics();
                    SetDefaults();
                    CommentBox.Content = "All vehicles have been removed successfully";
                }
                else
                {
                    CommentBox.Content = "Failed to remove all vehicle";
                }
            }
        }
        /// <summary>
        /// Button to exit the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm Close", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

    }
}
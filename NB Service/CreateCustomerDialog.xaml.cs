using System.Runtime.InteropServices;
using Controller;
using Interfaces;
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

namespace NB_Service
{
    /// <summary>
    /// Interaction logic for CreateCustomerDialog.xaml
    /// </summary>
    public partial class CreateCustomerDialog : Window
    {
        MainWindow mainWindow;
        ICustomer icustomer;
        CustomerFacade cf;
        AppointmentFacade af;
        List<IAppointment> appointments;
        List<ICustomer> customersList;
        bool EditInsteadOfSave; // true = opdater kunde .. false = opret ny kunde

        public CreateCustomerDialog(MainWindow mainWindow, CustomerFacade cfInput, AppointmentFacade afInput, List<ICustomer> customers)
        {
            InitializeComponent();
            cf = cfInput;
            af = afInput;
            customersList = customers;
            icustomer = cf.CreateCustomer();
            this.mainWindow = mainWindow;

            EditInsteadOfSave = false;

            appointments = new List<IAppointment>();
            AppointmentListView.ItemsSource = appointments;
        }

        // Constructor til "Ændre Kunde":
        public CreateCustomerDialog(MainWindow mainWindow, CustomerFacade cfInput, AppointmentFacade afInput, List<ICustomer> customers, ICustomer cust)
        {
            InitializeComponent();
            cf = cfInput;
            af = afInput;
            customersList = customers;
            icustomer = cust;
            this.mainWindow = mainWindow;

            appointments = cust.Appointments;
            AppointmentListView.ItemsSource = appointments;

            EditInsteadOfSave = true;

            nameTextBox.Text = cust.Name;
            contactPersonTextBox.Text = cust.ContactPerson;
            addressTextBox.Text = cust.Address;
            cityTextBox.Text = cust.City;
            zipCodeTextBox.Text = cust.ZipCode;
            phoneNumberTextBox.Text = cust.PhoneNumber;
        }

        private void SaveCustomerClicked(object sender, RoutedEventArgs e)
        {
            icustomer.Name = nameTextBox.Text;
            icustomer.ContactPerson = contactPersonTextBox.Text;
            icustomer.Address = addressTextBox.Text;
            icustomer.ZipCode = zipCodeTextBox.Text;
            icustomer.City = cityTextBox.Text;
            icustomer.PhoneNumber = phoneNumberTextBox.Text;
            icustomer.Appointments = appointments;

            if (EditInsteadOfSave == false)
            {
                icustomer.Id = GetNextId();
                cf.SaveCustomer(icustomer);
            }
            else
            {
                cf.Update(icustomer);
            }

            mainWindow.customersDataGrid.Items.Refresh();
            mainWindow.GetAppointments();
            mainWindow.appointmentsDataGrid.Items.Refresh();
            this.Close();
        }

        private int GetNextId()
        {
           
                if (customersList != null && customersList.Count > 0)
                {
                    int currentID = customersList.Select(customer => customer.Id).Concat(new[] { 0 }).Max();

                    return currentID + 1;
                }
                else
                {
                    return 0;
                }
        }
            
        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addAppointmentClicked(object sender, RoutedEventArgs e)
        {
            AddAppointmentDialog aap = new AddAppointmentDialog(icustomer, this, appointments);
            aap.ShowDialog();

            appointments.Add(aap.appointment);

            AppointmentListView.Items.Refresh();
        }

        private void deleteAppointmentClicked(object sender, RoutedEventArgs e)
        {
            appointments.Remove(AppointmentListView.SelectedItem as IAppointment);
            AppointmentListView.Items.Refresh();
        }
    }
}

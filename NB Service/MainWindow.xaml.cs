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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NB_Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<ICustomer> customersList;
        private List<IAppointment> appointments;
        CustomerFacade cf;
        AppointmentFacade af;

        public MainWindow()
        {
            cf = new CustomerFacade();
            af = new AppointmentFacade(cf);
            InitializeComponent();
            customersList = cf.LoadAllCustomers();

            customersDataGrid.ItemsSource = customersList;
            GetAppointments();
        }

        public void GetAppointments()
        {
            if(customersList != null)
            {
                appointments = new List<IAppointment>();
                foreach (ICustomer customer in customersList)
                {
                    foreach (IAppointment appointment in customer.Appointments)
                    {
                        appointments.Add(appointment);
                    }
                }
                appointmentsDataGrid.ItemsSource = appointments;
            }      
        }

        private void CreateCustomerClicked(object sender, RoutedEventArgs e)
        {
            CreateCustomerDialog ccd = new CreateCustomerDialog(this, cf, af, customersList);
            ccd.ShowDialog();
        }

        private void editCustomerButtonClicked(object sender, RoutedEventArgs e)
        {
            if (customersDataGrid.SelectedItem != null)
            {
                ICustomer selectedCustomer = customersDataGrid.SelectedItem as ICustomer;
                CreateCustomerDialog ccd = new CreateCustomerDialog(this, cf, af, customersList, selectedCustomer);
                ccd.ShowDialog();
            }
            else
            {
                CreateCustomerDialog ccd = new CreateCustomerDialog(this, cf, af, customersList);
                ccd.ShowDialog();
            }
        }

        private void searchCustomersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchCustomersTextBox.Text != "")
            {
                List<ICustomer> searchCustomersList = new List<ICustomer>();
                customersDataGrid.ItemsSource = searchCustomersList;

                foreach (ICustomer cust in customersList)
                {
                    string lastname = cust.Name.ToString().Split(' ').Last(); // find efternavn

                    // søgekriterier:
                    if (cust.Name.StartsWith(searchCustomersTextBox.Text, StringComparison.OrdinalIgnoreCase) || lastname.StartsWith(searchCustomersTextBox.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        searchCustomersList.Add(cust);
                    }
                } 
            }
            else // hvis søgefeltet er tomt:
            {
                customersDataGrid.ItemsSource = customersList;
            }

            customersDataGrid.Items.Refresh();
        }

        private void searchAppointmentsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchAppointmentsTextBox.Text != "")
            {
                List<IAppointment> searchAppointmentsList = new List<IAppointment>();
                appointmentsDataGrid.ItemsSource = searchAppointmentsList;

                foreach (ICustomer cust in customersList)
                {
                    foreach (IAppointment app in cust.Appointments)
                    {
                        string searchData = app.Name + " " + app.Description + " " + app.StartDate + " " + app.Address + " " + app.ZipCode + " " + app.City;
                        if (searchData.IndexOf(searchAppointmentsTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            searchAppointmentsList.Add(app);
                        }
                    }
                }
            }
            else
            {
                appointmentsDataGrid.ItemsSource = appointments;
            }

            appointmentsDataGrid.Items.Refresh();
        }

        private void HighlightCustomerOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IAppointment SelectedAppointment = appointmentsDataGrid.SelectedItem as IAppointment;

            foreach (ICustomer customer in customersDataGrid.Items)
            {
                foreach (IAppointment appointment in customer.Appointments)
                {
                    if (appointment == SelectedAppointment)
                    {
                        customersDataGrid.SelectedItem = customer;
                    }
                }
            }
        }

        private void deleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ICustomer selectedCustomer = customersDataGrid.SelectedItem as ICustomer;
            cf.Delete(selectedCustomer);
            customersList.Remove(selectedCustomer);
            customersDataGrid.Items.Refresh();
            GetAppointments();
            appointmentsDataGrid.Items.Refresh();
        }
    }
}

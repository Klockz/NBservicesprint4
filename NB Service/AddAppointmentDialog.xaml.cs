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
using Controller;
using Interfaces;

namespace NB_Service
{
    /// <summary>
    /// Interaction logic for AddAppointmentDialog.xaml
    /// </summary>
    public partial class AddAppointmentDialog : Window
    {
        private ICustomer iCustomer;
        public IAppointment appointment;
        private CreateCustomerDialog ccd;
        private List<IAppointment> appointments;

        public AddAppointmentDialog()
        {
            InitializeComponent();
        }

        public AddAppointmentDialog(ICustomer icustomer, CreateCustomerDialog createCustomerDialog, List<IAppointment> appointments)
        {
            
            this.iCustomer = icustomer;
            this.ccd = createCustomerDialog;
            this.appointments = appointments;
            InitializeComponent();
        }

        private void onClickAddAppointment(object sender, RoutedEventArgs e)
        {
            appointment = AppointmentFacade.CreateAppointment();

            try
            {
                appointment.Name = txtboxNavn.Text;
                appointment.Description = txtboxBeskrivelse.Text;

                TimeSpan tidspunkt;
                TimeSpan.TryParse(txtboxTidspunkt.Text, out tidspunkt);

                if (dpStartDate.SelectedDate != null)
                {
                    appointment.StartDate = dpStartDate.SelectedDate.Value.Add(tidspunkt);
                }
                else
                {
                    appointment.StartDate = DateTime.Now;
                }

                int freq;
                int.TryParse(txtboHyppighed.Text, out freq);
                appointment.Frequency = freq;

                double price;
                double.TryParse(txtboxPris.Text, out price);
                appointment.Price = price;

                appointment.City = txtboxBy.Text;
                appointment.ZipCode = txtboxPostnr.Text;
                appointment.Address = txtboxAddresse.Text;

                //appointments.Add(appointment);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Fejl! " + e.ToString());
            }

            Close();
        }
    }
}

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

using Pra.MonitoraatJH2.Core.Entities;
using Pra.MonitoraatJH2.Core.Services;
using Pra.MonitoraatJH2.Core.Enums;
using Pra.MonitoraatJH2.Core.Interfaces;

namespace Pra.MonitoraatJH2.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool isNewAddress;
        PersonService personService;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            personService = new PersonService();
            PopulateAddressTypes();
            PopulateTypes();
            PopulatePersons();
            DefaultView();
        }
        private void DefaultView()
        {
            grpAddresses.IsEnabled = false;
            grpPersons.IsEnabled = true;
            grpDetails.IsEnabled = true;
            btnSaveAddress.Visibility = Visibility.Hidden;
            btnCancelAddress.Visibility = Visibility.Hidden;
        }
        private void AddressView()
        {
            grpAddresses.IsEnabled = true;
            grpPersons.IsEnabled = false;
            grpDetails.IsEnabled = false;
            btnSaveAddress.Visibility = Visibility.Visible;
            btnCancelAddress.Visibility = Visibility.Visible;
        }
        private void PopulatePersons(Type type = null)
        {
            // VOORBEELD HOE JE EEN COMBO OF LISTBOX KUNT VULLEN MET OBJECTEN
            lstPersons.ItemsSource = null;
            if(type == null)
                lstPersons.ItemsSource = personService.Persons;
            else
                lstPersons.ItemsSource = personService.GetPersonsPerType(type);
        }
        private void PopulateTypes()
        {
            // VOORBEELD HOE JE EEN COMBO OF LISTBOX KAN VULLEN MET TYPES
            cmbFilter.Items.Clear();
            ComboBoxItem itm;
            foreach(Type type in personService.GetPersonTypes())
            {
                itm = new ComboBoxItem();
                itm.Content = type.Name;
                itm.Tag = type;
                cmbFilter.Items.Add(itm);
            }
            cmbFilter.SelectedIndex = -1;
        }
        private void PopulateAddressTypes()
        {
            // VOORBEELD HOE JE EEN COMBO OF LISTBOX KAN VULLEN MET ENUM WAARDEN
            ComboBoxItem itm;
            foreach (var addressTypeName in Enum.GetValues(typeof(AddressType)))
            {
                itm = new ComboBoxItem();
                itm.Tag = addressTypeName;
                itm.Content = addressTypeName.ToString();
                cmbAddressTypes.Items.Add(itm);
            }

        }
        private void ClearControls()
        {
            tblInfo.Text = "";
            lblName.Content = "";
            lstAddresses.ItemsSource = null;
            ClearAddressControls();
        }
        private void ClearAddressControls()
        {
            cmbAddressTypes.SelectedIndex = -1;
            txtStreet.Text = "";
            txtHouseNumber.Text = "";
            txtPostalCode.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
        }
        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFilter.SelectedItem != null)
            {
                ComboBoxItem itm = (ComboBoxItem)cmbFilter.SelectedItem;
                PopulatePersons((Type)itm.Tag);
            }
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbFilter.SelectedIndex = -1;
            PopulatePersons();
        }

        private void lstPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstPersons.SelectedItem != null)
            {
                Person person = (Person)lstPersons.SelectedItem;
                tblInfo.Text = person.ShowDetails();
                lblName.Content = person.Name;
                lstAddresses.ItemsSource = person.Addresses;
            }
        }



        private void lstAddresses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAddressControls();
            if(lstAddresses.SelectedItem != null)
            {

                Address address = (Address)lstAddresses.SelectedItem;
                foreach (ComboBoxItem itm in cmbAddressTypes.Items)
                {
                    if ((AddressType)itm.Tag == address.AddressType)
                    {
                        itm.IsSelected = true;
                        break;
                    }
                }
                txtStreet.Text = address.Street;
                txtHouseNumber.Text = address.HouseNumber;
                txtPostalCode.Text = address.PostalCode;
                txtCity.Text = address.City;
                txtCountry.Text = address.Country;
            }
        }

        private void btnNewAddress_Click(object sender, RoutedEventArgs e)
        {
            isNewAddress = true;
            ClearAddressControls();
            AddressView();
            cmbAddressTypes.Focus();
        }

        private void btnEditAddress_Click(object sender, RoutedEventArgs e)
        {
            if(lstAddresses.SelectedItem != null)
            {
                isNewAddress = false;
                AddressView();
                cmbAddressTypes.Focus();
            }
        }

        private void btnDeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            if(lstAddresses.SelectedItem != null)
            {
                ClearAddressControls();
                Address address = (Address)lstAddresses.SelectedItem;
                Person person = (Person)lstPersons.SelectedItem;
                person.Addresses.Remove(address);
                lstPersons_SelectionChanged(null, null);
            }
        }

        private void btnSaveAddress_Click(object sender, RoutedEventArgs e)
        {
            string street = txtStreet.Text.Trim();
            string houseNumber = txtHouseNumber.Text.Trim();
            string postalCode = txtPostalCode.Text.Trim();
            string city = txtCity.Text.Trim();
            string country = txtCountry.Text.Trim();
            ComboBoxItem itm = (ComboBoxItem)cmbAddressTypes.SelectedItem;
            AddressType addressType = (AddressType)itm.Tag;

            Person person = (Person)lstPersons.SelectedItem;
            Address address;
            if(isNewAddress)
            {
                address = new Address(street, houseNumber, postalCode, city, country, addressType);
                person.Addresses.Add(address);
            }
            else
            {
                address = (Address)lstAddresses.SelectedItem;
                address.Street = street;
                address.HouseNumber = houseNumber;
                address.PostalCode = postalCode;
                address.City = city;
                address.Country = country;
                address.AddressType = addressType;

            }
            lstPersons_SelectionChanged(null, null);
            DefaultView();

        }

        private void btnCancelAddress_Click(object sender, RoutedEventArgs e)
        {
            DefaultView();
            ClearAddressControls();
            lstAddresses_SelectionChanged(null, null);
        }
    }
}

using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace munkalap
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private Enums.WindowMode windowMode;
        private Employee employee;
        private ObservableCollection<Employee> employees;

        public Enums.WindowMode WindowMode
        {
            get { return windowMode; }
            set 
            { 
                windowMode = value;
                lbEmployee.IsEnabled = windowMode == Enums.WindowMode.Reader;
                btnNew.IsEnabled = windowMode == Enums.WindowMode.Reader;
                grpDetails.IsEnabled = windowMode == Enums.WindowMode.Edit;
            }
        }

        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowMode = Enums.WindowMode.Reader;
            employees = new ObservableCollection<Employee>(employeeRepository.GetAll());
            lbEmployee.ItemsSource = employees;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            WindowMode = Enums.WindowMode.Edit;
            employee = new Employee() { Id = 0 };
            grpDetails.DataContext = employee;
            txtName.Focus();
        }

        private void miModify_Click(object sender, RoutedEventArgs e)
        {
            WindowMode = Enums.WindowMode.Edit;
            employee = (Employee)lbEmployee.SelectedItem;
            grpDetails.DataContext = employee;
            txtName.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            WindowMode = Enums.WindowMode.Reader;
            if (employee.Id != 0)   
            {
                var index = employees.IndexOf(employee);
                employees[index] = employeeRepository.GetById(employee.Id);
            }
            employee = null;
            grpDetails.DataContext = employee;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (employee.Id == 0)
            {
                employees.Add(employeeRepository.Create(employee));                
            }
            else
            {
                employeeRepository.Update(employee);
            }
            WindowMode = Enums.WindowMode.Reader;
            employee = null;
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var empl = (Employee)lbEmployee.SelectedItem;
            var message = $"Biztosan törölni szeretné a {empl.Name} nevű dolgozót";
            if (MessageBox.Show(message, "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                employeeRepository.Delete(empl);
                employees.Remove(empl);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using munkalap.service;
using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace munkalap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ApplicationDbContext.AppDbContext = new ApplicationDbContext();
            ApplicationDbContext.AppDbContext.ConnectionString = ConfigurationManager.ConnectionStrings["worksheet"].ConnectionString;
#if DEBUG
            ApplicationDbContext.AppDbContext.Database.EnsureCreated();
#endif               
        }

        private void miEmployees_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = new EmployeeWindow();
            employeeWindow.ShowDialog();
           
        }

        private void miFailures_Click(object sender, RoutedEventArgs e)
        {
            new FailureWindow().ShowDialog();
        }
    }
}

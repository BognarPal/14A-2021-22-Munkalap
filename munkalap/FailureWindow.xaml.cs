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
    /// Interaction logic for FailureWindow.xaml
    /// </summary>
    public partial class FailureWindow : Window
    {
        private FailureRepository failureRepository = new FailureRepository();
        private ObservableCollection<Failure> failures = new ObservableCollection<Failure>();

        public FailureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgFailures.ItemsSource = failures;
            var employeeRepository = new EmployeeRepository();
            cboEmployee.ItemsSource = employeeRepository.GetAll();
            cboEmployee.DisplayMemberPath = "Name";
            cboEmployee.SelectedValuePath = "Id";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new FailureDataWindow(failureRepository);
            if (wnd.ShowDialog() == true)
                failures.Add(wnd.Failure);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Failure> results = failureRepository
                .Search(f => (cboEmployee.SelectedItem == null || (f.Assigned != null && f.Assigned.Id == (int)cboEmployee.SelectedValue))
                          && f.Description.Contains(txtDescription.Text));

            failures = new ObservableCollection<Failure>(results);
            dgFailures.ItemsSource = failures;
        }
        
        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtDescription.Text = "";
            cboEmployee.SelectedItem = null;
            failures = new ObservableCollection<Failure>();
            dgFailures.ItemsSource = failures;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgFailures.SelectedItem != null)
            {
                if (MessageBox.Show("Biztosan törli a munkalapot?", "Megerősítés", MessageBoxButton.YesNo,
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    failureRepository.Delete((Failure)dgFailures.SelectedItem);
                    failures.Remove((Failure)dgFailures.SelectedItem);
                }
            }
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (dgFailures.SelectedItem != null)
            {
                var wnd = new FailureAssignWindow((Failure)dgFailures.SelectedItem);
                if (wnd.ShowDialog() == true)
                {
                    dgFailures.Items.Refresh();
                    dgFailures_SelectionChanged(null, null);
                }
            }
        }

        private void btnFinished_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Finish comment kezelése
            if (dgFailures.SelectedItem != null)
            {
                var selectedFailure = (Failure)dgFailures.SelectedItem;
                if (selectedFailure.Assigned != null && selectedFailure.WorkFinished == null)
                {
                    if (MessageBox.Show("Biztos?","Megerősítés",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        selectedFailure.WorkFinished = DateTime.Now;
                        failureRepository.Update(selectedFailure);
                        dgFailures.Items.Refresh();
                        dgFailures_SelectionChanged(null, null);
                    }
                }
            }
        }

        private void dgFailures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFailures.SelectedItem == null)
            {
                btnAssign.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnFinished.IsEnabled = false;
            }
            else
            {
                var selectedFailure = (Failure)dgFailures.SelectedItem;
                btnAssign.IsEnabled = selectedFailure.WorkFinished == null;
                btnDelete.IsEnabled = selectedFailure.WorkFinished == null;
                btnFinished.IsEnabled = selectedFailure.Assigned != null && selectedFailure.WorkFinished == null;
            }
        }

        
    }
}

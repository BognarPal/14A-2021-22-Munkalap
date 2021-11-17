using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FailureAssignWindow.xaml
    /// </summary>
    public partial class FailureAssignWindow : Window
    {
        private Failure failure;

        public FailureAssignWindow(Failure failure)
        {
            InitializeComponent();
            this.failure = failure;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var employeeRepository = new EmployeeRepository();
            cboEmployee.ItemsSource = employeeRepository.GetAll();
            cboEmployee.DisplayMemberPath = "Name";
            cboEmployee.SelectedValuePath = "Id";

            if (failure.Assigned != null)
            {
                cboEmployee.SelectedValue = failure.Assigned.Id;
                txtComment.Text = failure.AssignComment;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cboEmployee.SelectedItem == null)
            {
                cboEmployee.IsDropDownOpen = true;
            }
            else
            {
                failure.Assigned = (Employee)cboEmployee.SelectedItem;
                failure.AssignTimeStamp = DateTime.Now;
                failure.AssignComment = txtComment.Text;
                var failureRepository = new FailureRepository();
                failureRepository.Update(failure);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


    }
}

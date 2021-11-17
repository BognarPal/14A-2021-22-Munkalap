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
    /// Interaction logic for FailureDataWindow.xaml
    /// </summary>
    public partial class FailureDataWindow : Window
    {
        public Failure Failure { get; set; }
        private FailureRepository failureRepository;

        public FailureDataWindow(FailureRepository failureRepository)
        {
            InitializeComponent();
            this.Failure = new Failure();
            this.DataContext = this.Failure;
            this.failureRepository = failureRepository;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (requiredFieldsOk())
            {
                this.Failure.IssueTimeStamp = DateTime.Now;
                failureRepository.Create(this.Failure);
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool requiredFieldsOk()
        {
            if ( string.IsNullOrWhiteSpace(Failure.Issuer) )
            {
                lblError.Content = "Kérem adja meg a bejelentő nevét!";
                txtIssuer.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Failure.Room))
            {
                lblError.Content = "Kérem adja meg melyik helyiségben van a hiba!";
                txtRoom.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Failure.Description))
            {
                lblError.Content = "Kérem röviden írja le a hibát!";
                txtDescription.Focus();
                return false;
            }
            lblError.Content = "";
            return true;
        }
    }
}

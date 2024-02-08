using ClienteWpf.FlexPulseService;
using MaterialDesignThemes.Wpf;
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

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for WindowSignUp.xaml
    /// </summary>
    public partial class WindowSignUp : Window
    {
        private ServiceGymClient myServiceGym;
        private User myUser;
        private bool pass, repass;
        public WindowSignUp()
        {
            InitializeComponent();
            myServiceGym = new ServiceGymClient();
            pass = repass = false;
            dpBirthday.DisplayDateStart = DateTime.Today.AddYears(-120);
            dpBirthday.DisplayDateEnd = DateTime.Today.AddYears(-16);

            myUser = new User();
            myUser.Bday = DateTime.Today.AddYears(-16);
            this.DataContext = myUser;
            tbFirstName.Focus();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                MessageBox.Show("error");
                return;
            }
            //Check if username is in use?
            if (!myServiceGym.IsEmailFree(myUser))
            {
                MessageBox.Show("Email is used");
                return;
            }
            //Username if free, Create new user
            myUser.Password = pbPassword.Password;
            myUser.IsManger = false;
            //Send to service
            if (myServiceGym.InsertUser(myUser) != 1)
            {
                MessageBox.Show("Oh oh...something is wrong");
                return;
            }
            //finish and close
           
            MessageBox.Show("Thank you (-:");
        }
        private bool CheckData()
        {
            if (tbFirstName.Text.Equals(string.Empty)) return false;
            if (tbLastName.Text.Equals(string.Empty)) return false;
            if (cmbGender.Text.Equals(string.Empty)) return false;
            if (tbEmail.Text.Equals(string.Empty)) return false;
            if (pbPassword.Password.Equals(string.Empty)) return false;
            if (pbcPassword.Password.Equals(string.Empty)) return false;
            if (dpBirthday.Text.Equals(string.Empty)) return false;
            if (Validation.GetHasError(tbFirstName)) return false;
            if (Validation.GetHasError(tbLastName)) return false;
            if (Validation.GetHasError(tbEmail)) return false;
            if (Validation.GetHasError(dpBirthday)) return false;
            if (!pass) return false;
            if (!repass) return false;
            return true;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            tbFirstName.Text =
            tbLastName.Text =
            cmbGender.Text =
            tbEmail.Text =
            pbPassword.Password =
            pbcPassword.Password =
            dpBirthday.Text = string.Empty;

        }
        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword valid = new ValidPassword();
            ValidationResult result = valid.Validate(pbPassword.Password, null);
            if (result.IsValid)
            {
                pass = true;
                pbPassword.BorderBrush = Brushes.Aqua;
                HintAssist.SetHelperText(pbPassword, "Password");
            }
            else
            {
                pass=false;
                pbPassword.BorderBrush = Brushes.Red;
                HintAssist.SetHelperText(pbPassword, result.ErrorContent.ToString());
            }
        }

        private void pbcRePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password.Equals(pbcPassword.Password))
            {
                repass = true;
                pbcPassword.BorderBrush = Brushes.Aqua;
                HintAssist.SetHelperText(pbcPassword, "Passwords must match");
            }
            else
            {
                repass=false;
                pbcPassword.BorderBrush = Brushes.Red;
                HintAssist.SetHelperText(pbcPassword, "Passwords DO NOT match");
            }
        }

        private void GoToLogin_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();    
        }
   

    }
}

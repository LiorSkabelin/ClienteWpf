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
using ClienteWpf.FlexPulseService;

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private User user;
        private bool UserEmailOK, PassOK;
        private ServiceGymClient myServiceGym;
        public LoginWindow()
        {
            InitializeComponent();
            myServiceGym = new ServiceGymClient();
            user = new User();
            this.DataContext = user;
            UserEmailOK = PassOK = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!UserEmailOK || !PassOK)
            {
                MessageBox.Show("Errors found!\n FIX IT", "NO");
                return;
            }
            user.Email = tbUseremail.Text;
            user.Password = pbPassword.Password;
            user = myServiceGym.Login(user);
            if (user == null)
            {
                MessageBox.Show("no user detected");
                this.DataContext = user = new User();
                return;
            }
            if (user.IsManger)
            {
                MessageBox.Show("nice Login");
                MangerWindow wa = new MangerWindow(user);
                wa.ShowDialog();
            }
            else
            {
                MessageBox.Show("Regular user login");
                HomeWindow wp = new HomeWindow(user);
                wp.ShowDialog();
            }
            tbUseremail.Text = pbPassword.Password = string.Empty;
        }

        private void Useremail_Changed(object sender, TextChangedEventArgs e)
        {
            Validemail email = new Validemail();
            ValidationResult result = email.Validate(tbUseremail.Text, null);
            if (!result.IsValid)
            {
                tbUseremail.BorderBrush = Brushes.Red;
                tbUseremail.Foreground = Brushes.Red;
                tbUseremail.ToolTip = result.ErrorContent.ToString();
                UserEmailOK = false;
            }
            else
            {
                tbUseremail.BorderBrush = Brushes.Black;
                tbUseremail.Foreground = Brushes.Black;
                tbUseremail.ToolTip = null;
                UserEmailOK = true;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword valid = new ValidPassword();
            ValidationResult result = valid.Validate(pbPassword.Password, null);
            if (!result.IsValid)
            {
                pbPassword.BorderBrush = Brushes.Red;
                pbPassword.Foreground = Brushes.Red;
                pbPassword.ToolTip = result.ErrorContent.ToString();
                PassOK = false;
            }
            else
            {
                pbPassword.BorderBrush = Brushes.Black;
                pbPassword.Foreground = Brushes.Black;
                pbPassword.ToolTip = null;
                PassOK = true;
            }
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            user.Email = "liorskab@gmail.com";
            user.Password = "Lior123";
            user = myServiceGym.Login(user);
            HomeWindow windowHome = new HomeWindow(user);
            windowHome.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowSignUp signUp = new WindowSignUp();
            this.Hide();
            signUp.ShowDialog();
            this.Show();
        }
    }
}

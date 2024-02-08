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
using MaterialDesignThemes.Wpf;

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        TrainingProgramList programs;
        ServiceGymClient serviceGym;
        Gymer gymer;
        User user;
        public HomeWindow(User user)
        {
            InitializeComponent();
            this.DataContext = this.user = user;

            serviceGym = new ServiceGymClient();

            gymer = serviceGym.SelectGymerByUser(user);
            if(gymer != null ) 
                tbCoach.Text = "My coach is: " + gymer.Mycoach.Firstname + " " + gymer.Mycoach.Lastname;

            programs = serviceGym.SelectAllTrainingp();
            foreach(TrainingProgram program in programs)
            {
                SmallProgramUserControl uc = new SmallProgramUserControl(program);
                uc.Tag = program;
                uc.MouseDoubleClick += Uc_MouseDoubleClick;
                panelPrograms.Children.Add(uc);
            }
        }

        private void Uc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //פתיחת החלון
            SmallProgramUserControl uc = sender as SmallProgramUserControl;
            TrainingProgram program=uc.Tag as TrainingProgram;
            WorkOutWindow exercisePage = new WorkOutWindow(program);
            exercisePage.Show();
        }
    }
}

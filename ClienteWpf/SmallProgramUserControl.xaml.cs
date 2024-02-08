using ClienteWpf.FlexPulseService;
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

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for SmallProgramUserControl.xaml
    /// </summary>
    public partial class SmallProgramUserControl : UserControl
    {
        TrainingProgram program;
        public SmallProgramUserControl(TrainingProgram program)
        {
            InitializeComponent();
            this.DataContext =this.program= program;
            ServiceGymClient flexPulseService=new ServiceGymClient();
            TrainProgramDeviceList deviceList = flexPulseService.SelectProgramDevicesByProgram(program);
            tbExNum.Text = "Ex num: " + deviceList.Count;
        }

        private void OpenWorkout_Click(object sender, RoutedEventArgs e)
        {
            //מעבר לחלון אימון
        }
    }
}

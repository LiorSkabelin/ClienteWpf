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
using ClienteWpf.FlexPulseService;

namespace ClienteWpf
{
    /// <summary>
    /// Interaction logic for WorkOutWindow.xaml
    /// </summary>
    public partial class WorkOutWindow : Window
    {
        private TrainProgramDeviceList deviceList;
        private ServiceGymClient flexPulseService;
        private int num;

        public WorkOutWindow(TrainingProgram program)
        {
            InitializeComponent();
            this.DataContext = program;
            flexPulseService = new ServiceGymClient();
            deviceList = flexPulseService.SelectProgramDevicesByProgram(program);
            num = 0;
            ExerciesPanel.DataContext = deviceList[num];
            btnBack.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            num++;
            if(num == deviceList.Count-1)
            {
                btnNext.Visibility = Visibility.Collapsed;
            }
            if (num >0)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            ExerciesPanel.DataContext = deviceList[num];
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            num--;
            if (num == 0) {
                btnBack.Visibility = Visibility.Collapsed;
            }
            if (num < deviceList.Count - 1)
            {
                btnNext.Visibility = Visibility.Visible;
            }
            ExerciesPanel.DataContext = deviceList[num];
        }
    }
}

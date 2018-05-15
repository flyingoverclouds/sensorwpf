using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SensorWpf;
using System.Diagnostics;

namespace SensorWpf
{
    /// <summary>
    /// Interaction logic for SensorDebug.xaml
    /// </summary>
    public partial class SensorDebug : UserControl
    {
        public SensorDebug()
        {
            InitializeComponent();
        }


        private void butInit_Click(object sender, RoutedEventArgs e)
        {
            if (WpfSensorManager.Current.Accelerometers3D.Count < 1)
                //MessageBox.Show("No accelerometer sensor on your system", "Missing hardware", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("No accelerometer sensor on your system");
            else
                stkAccelerometer.DataContext = WpfSensorManager.Current.Accelerometers3D[0];

            if (WpfSensorManager.Current.LightSensors.Count < 1)
                //MessageBox.Show("No light sensor sensor on your system", "Missing hardware", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("No light sensor sensor on your system");
            else
                grdAmbientLight.DataContext = WpfSensorManager.Current.LightSensors[0];

            if (WpfSensorManager.Current.SwitchArrays.Count < 1)
                //MessageBox.Show("No boolean switch array sensor on your system", "Missing hardware", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Debug.WriteLine("No boolean switch array sensor on your system");
            else
                iscSwitchRank.ItemsSource = WpfSensorManager.Current.SwitchArrays;

        }

        private void butRecalibrate_Click(object sender, RoutedEventArgs e)
        {
            WpfSensor sensor = stkAccelerometer.DataContext as WpfSensor;
            if (sensor != null)
                sensor.Recalibrate();
            
            sensor = grdAmbientLight.DataContext as WpfSensor;
            if (sensor != null)
                sensor.Recalibrate();
        }

        public bool ShowXbar { 
            get { return stkX.Visibility==Visibility.Visible; }
            set { stkX.Visibility=(value)?Visibility.Visible:Visibility.Collapsed; }
        }

        public bool ShowYbar
        {
            get { return stkY.Visibility == Visibility.Visible; }
            set { stkY.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool ShowZbar
        {
            get { return stkZ.Visibility == Visibility.Visible; }
            set { stkZ.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed; }
        }


    }
}

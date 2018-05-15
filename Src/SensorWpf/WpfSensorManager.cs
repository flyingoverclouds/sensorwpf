using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Windows7.Sensors;
using System.Runtime.InteropServices;
using Windows7.Sensors.Sensors.Motion;
using System.Collections.ObjectModel;
using Windows7.Sensors.Sensors.Light;
using Windows7.Sensors.Sensors.Mechanical;

namespace SensorWpf
{
    public class WpfSensorManager 
    {
        #region Singleton
        static private  WpfSensorManager  _instance = null;

        static public WpfSensorManager  Current
        {
            get
            {
                if (_instance==null)
                    _instance=new WpfSensorManager();
                return _instance;
            }

        }
        #endregion

        private ObservableCollection<WpfAccelerometer3D> _Accelerometers3D;
        public ObservableCollection<WpfAccelerometer3D> Accelerometers3D
        {
            get { return _Accelerometers3D; }
            private set { _Accelerometers3D = value; }
        }

        private ObservableCollection<WpfAmbientLight> _LightSensors;
        public ObservableCollection<WpfAmbientLight> LightSensors
        {
            get { return _LightSensors; }
            private set { _LightSensors = value; }
        }

        private ObservableCollection<WpfSwitchArray> _SwitchArrays;
        public ObservableCollection<WpfSwitchArray> SwitchArrays
        {
            get { return _SwitchArrays; }
            private set { _SwitchArrays = value; }
        }

        private WpfSensorManager()
        {
            Accelerometers3D = new ObservableCollection<WpfAccelerometer3D>();
            LightSensors=new ObservableCollection<WpfAmbientLight>();
            SwitchArrays = new ObservableCollection<WpfSwitchArray>();

            Sensor[] sensors = GetAllSensors();
            foreach (Sensor sensor in sensors)
            {
                if (sensor is Accelerometer3D)
                    _Accelerometers3D.Add(new WpfAccelerometer3D(sensor as Accelerometer3D));
                else if (sensor is AmbientLightSensor)
                    _LightSensors.Add(new WpfAmbientLight(sensor as AmbientLightSensor));
                else if (sensor is BooleanSwitchArray)
                    _SwitchArrays.Add(new WpfSwitchArray(sensor as BooleanSwitchArray));
            }
        }


        #region encapsulation Api Sensor
        /// <summary>
        /// Helper function that wraps SensorManager.GetAllSensors()
        /// </summary>
        /// <returns>All sensors or Sensor[0] if no sensors.</returns>
        private Sensor[] GetAllSensors()
        {
            Sensor[] sensors = null;

            try
            {
                sensors = SensorManager.GetAllSensors();
            }
            catch (COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    case SensorErrors.ERROR_NOT_FOUND:
                        sensors = new Sensor[0];
                        break;
                    default:
                        throw;
                }
            }

            return sensors;
        }
        #endregion
    }
}

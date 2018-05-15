using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows7.Sensors.Sensors.Light;
using Windows7.Sensors;

namespace SensorWpf
{
    public delegate void WpfAmbientLightDataUpdatedDelegate(WpfAmbientLight ambientLightSensor, double newLux);


    /// <summary>
    /// Encapsulate a Light sensor on a WPF aware class.
    /// </summary>
    public class WpfAmbientLight : WpfSensor
    {

        public event WpfAmbientLightDataUpdatedDelegate LuxDataUpdated;

        AmbientLightSensor _ambientLightSensor = null;
        public WpfAmbientLight(AmbientLightSensor sensor)
            : base(sensor)
        {
            this._ambientLightSensor = (AmbientLightSensor)sensor;
            if (_ambientLightSensor == null)
                throw new ArgumentException("sensor must be an AmbienLightSensor type");
        }

        double currentLux;
        public double Lux
        {
            get { return currentLux; }
            private set { 
                currentLux = value;
                FirePropertyChanged("Lux");
            }
        }

        double luxMaximum;

        public double LuxMaximum
        {
            get { return luxMaximum; }
            private set { luxMaximum = value; FirePropertyChanged("LuxMaximum"); }
        }

        double luxMinimum;

        public double LuxMinimum
        {
            get { return luxMinimum; }
            private set { luxMinimum = value; FirePropertyChanged("LuxMinimum"); }
        }


        protected override void SpecificDataUpdated(Windows7.Sensors.SensorDataReport dataReport)
        {
            double lux = Math.Round((float)dataReport.GetDataField(SensorPropertyKeys.SENSOR_DATA_TYPE_LIGHT_LUX), DecimalPrecision);

            if(resetCalibrate)
            {
                LuxMinimum = lux;
                LuxMaximum = lux;
                resetCalibrate = false;
            }

            this.Lux = lux;

            if (LuxDataUpdated != null)
                LuxDataUpdated(this, lux);
        }

        private bool resetCalibrate = true;
        protected override void SpecificRecalibrate()
        {
            resetCalibrate = true;
        }
    }
}

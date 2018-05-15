using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windows7.Sensors;

namespace SensorWpf
{
    public abstract class WpfSensor : INotifyPropertyChanged
    {
        Sensor _sensor = null;
        public WpfSensor(Sensor sensor)
        {
            _sensor = sensor;
            _sensor.DataUpdated += new Windows7.Sensors.SensorDataUpdatedEventHandler(sensor_DataUpdated);
            _sensor.EventReceived += new Windows7.Sensors.SensorEventHandler(sensor_EventReceived);
            _sensor.SensorLeave += new Windows7.Sensors.SensorLeaveEventHandler(sensor_SensorLeave);
            _sensor.StateChanged += new Windows7.Sensors.SensorStateChangedEventHandler(sensor_StateChanged);

            FirePropertyChanged("Name");
            FirePropertyChanged("Model");
            FirePropertyChanged("Id");
            FirePropertyChanged("SerialNumber");
            FirePropertyChanged("Manufacturer");
            FirePropertyChanged("State");
        }

        #region Méthodes abstraites
        abstract protected void SpecificDataUpdated(SensorDataReport dataReport);
        abstract protected void SpecificRecalibrate();
        #endregion

        void sensor_StateChanged(Sensor sensor, Windows7.Sensors.SensorState state)
        {
            FirePropertyChanged("State");
        }

        void sensor_SensorLeave(Sensor sensor, Guid sensorID)
        {
            //throw new NotImplementedException();
        }

        void sensor_EventReceived(Windows7.Sensors.Sensor sensor, Guid eventID, Windows7.Sensors.SensorDataReport dataReport)
        {
            //throw new NotImplementedException();
        }

        void sensor_DataUpdated(Windows7.Sensors.Sensor sensor, Windows7.Sensors.SensorDataReport dataReport)
        {
            if (sensor != this._sensor)
                return;
            CommonDataUpdated(dataReport);  // traitement des données communes à tous les capteurs
            SpecificDataUpdated(dataReport);    // traitement des données spécifique à un capteurs particulier
            this.DataTimestamp = dataReport.Timestamp;
        }

        void CommonDataUpdated(SensorDataReport dataReport)
        {
        }


        #region Méthode publique
        public void Recalibrate()
        {
            SpecificRecalibrate();
        }
        #endregion


        #region Propriété du sensor
        public string Name
        {
            get { return _sensor.FriendlyName; }

        }

        public string Model
        {
            get { return _sensor.SensorModel; }

        }
        public Guid Id
        {
            get { return _sensor.SensorID; }
        }

        public string SerialNumber
        {
            get { return _sensor.SensorSerialNumber; }

        }

        public string Manufacturer
        {
            get { return _sensor.SensorManufacturer; }

        }

        public SensorState State
        {
            get { return _sensor.State; }

        }

        DateTime _dataTimestamp=DateTime.MinValue;
        public DateTime DataTimestamp
        {
            get { return _dataTimestamp;  }
            private set { _dataTimestamp = value; FirePropertyChanged("DataTimestamp"); }
        }
        #endregion

        #region Propriété de paramétrage
        private int _decimalPrecision=2;
        public int DecimalPrecision
        {
            get { return _decimalPrecision;  }
            set { _decimalPrecision = value; }
        }


        #endregion

        #region INotifyPropertyChanged Members

        protected void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

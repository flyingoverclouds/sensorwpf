using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Windows7.Sensors.Sensors.Motion;
using System.Diagnostics;
using Windows7.Sensors;

namespace SensorWpf
{
    public delegate void WpfAccelerometer3DDataUpdatedDelegate(WpfAccelerometer3D sensor,double newX,double newY,double newZ);

    /// <summary>
    /// Encapsule l'acces et l'utilisation d'une accelemetre pour WPF
    /// </summary>
    public class WpfAccelerometer3D : WpfSensor
    {
        #region Event
        public event WpfAccelerometer3DDataUpdatedDelegate AccelerationDataUpdated;
        #endregion

        Accelerometer3D _accelerometer = null;
        internal WpfAccelerometer3D(Windows7.Sensors.Sensors.Motion.Accelerometer3D sensor) : base(sensor)
        {
            _accelerometer = sensor as Accelerometer3D;
            if (_accelerometer == null)
                throw new ArgumentException("sensor must be of Accelerometer3D type");
            
        }

        #region traitement spécifique a l'accellerometre
        protected override void SpecificDataUpdated(Windows7.Sensors.SensorDataReport dataReport)
        {
           
             double x = Math.Round((float)dataReport.GetDataField(SensorPropertyKeys.SENSOR_DATA_TYPE_ACCELERATION_X_G),DecimalPrecision);
             double y = Math.Round((float)dataReport.GetDataField(SensorPropertyKeys.SENSOR_DATA_TYPE_ACCELERATION_Y_G),DecimalPrecision);
             double z = Math.Round((float)dataReport.GetDataField(SensorPropertyKeys.SENSOR_DATA_TYPE_ACCELERATION_Z_G), DecimalPrecision);

            if (x< AccelerationXMinimum)
                AccelerationXMinimum = x;
            if (x > AccelerationXMaximum)
                AccelerationXMaximum = x;


            if (y < AccelerationYMinimum)
                AccelerationYMinimum = y;
            if (y > AccelerationYMaximum)
                AccelerationYMaximum = y;


            if (z < AccelerationZMinimum)
                AccelerationZMinimum = z;
            if (z > AccelerationZMaximum)
                AccelerationZMaximum = z;

            this.AccelerationX = x;
            this.AccelerationY = y;
            this.AccelerationZ = z;

            if (AccelerationDataUpdated != null)
                AccelerationDataUpdated(this, x, y, z);
        }

        protected override void SpecificRecalibrate()
        {
            lock (this)
            {
                this.AccelerationXMaximum = 0.0;
                this.AccelerationXMinimum = 0.0;
                this.AccelerationYMaximum = 0.0;
                this.AccelerationYMinimum = 0.0;
                this.AccelerationZMaximum = 0.0;
                this.AccelerationZMinimum = 0.0;
            }
        }
        #endregion



        #region Propriété spécifique de l'accelerometre

        double _XMinimum = -2;
        public double AccelerationXMinimum
        {
            get { return _XMinimum; }
            private set
            {
                _XMinimum = value;
                FirePropertyChanged("AccelerationXMinimum");
            }
        }
        double _XMaximum =2;
        public double AccelerationXMaximum
        {
            get { return _XMaximum; }
            private set
            {
                _XMaximum = value;
                FirePropertyChanged("AccelerationXMaximum");
            }
        }
        double _X = 0.0;
        public double AccelerationX
        {
            get { return _X; }
            private set { 
                _X = value;
                FirePropertyChanged("AccelerationX");
            }
        }


        double _YMinimum = -2;
        public double AccelerationYMinimum
        {
            get { return _YMinimum; }
            private set
            {
                _YMinimum = value;
                FirePropertyChanged("AccelerationYMinimum");
            }
        }

        double _YMaximum = 2;
        public double AccelerationYMaximum
        {
            get { return _YMaximum; }
            private set
            {
                _YMaximum = value;
                FirePropertyChanged("AccelerationYMaximum");
            }
        }

        double _Y = 0.0;
        public double AccelerationY
        {
            get { return _Y; }
            private set { _Y = value; FirePropertyChanged("AccelerationY"); }
        }

        double _ZMinimum = -2;
        public double AccelerationZMinimum
        {
            get { return _ZMinimum; }
            private set
            {
                _ZMinimum = value;
                FirePropertyChanged("AccelerationZMinimum");
            }
        }

        double _ZMaximum = 2;
        public double AccelerationZMaximum
        {
            get { return _ZMaximum; }
            private set
            {
                _ZMaximum = value;
                FirePropertyChanged("AccelerationZMaximum");
            }
        }

        double _Z = 0.0;
        public double AccelerationZ
        {
            get { return _Z; }
            private set { _Z = value; FirePropertyChanged("AccelerationZ"); }
        }


        #endregion

   
    }
}

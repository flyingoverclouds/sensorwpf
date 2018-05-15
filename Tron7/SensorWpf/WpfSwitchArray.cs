using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows7.Sensors.Sensors.Mechanical;
using System.Collections.ObjectModel;
using Windows7.Sensors;

namespace SensorWpf
{
    public delegate void WpfSwitchArrayDataUpdatedDelegate(WpfSwitchArray switchArraySensor, string switchName,bool switchState);


    public class WpfSwitchArray : WpfSensor
    {

        public event WpfSwitchArrayDataUpdatedDelegate SwitchStateChanged;
        
        private BooleanSwitchArray _switchArray=null;
        ObservableCollection<WpfSwitch> switchStates = new ObservableCollection<WpfSwitch>();

        public ObservableCollection<WpfSwitch> SwitchStates
        {
            get { return switchStates; }
            private set { 
                switchStates = value;
                FirePropertyChanged("SwitchStates");
            }
        }


        public WpfSwitchArray(BooleanSwitchArray sensor) : base(sensor)
        {
            _switchArray = sensor;
            if (_switchArray == null)
                throw new ArgumentException("sensor must be an BooleanSwitchArray type");

            // How detecting the number of switch on a SwitchArraySensor ?????
            // assuming  this is the SensorDevelopmentKit board :(

            if (this.Name.ToLower().StartsWith("left"))
            {
                switchStates.Add(new WpfSwitch() { State = false, Name = "E1" });
                switchStates.Add(new WpfSwitch() { State = false, Name = "E2" });
                switchStates.Add(new WpfSwitch() { State = false, Name = "E3" });
                switchStates.Add(new WpfSwitch() { State = false, Name = "E4" });
            }
            else
                if (this.Name.ToLower().StartsWith("right"))
                {
                    switchStates.Add(new WpfSwitch() { State = false, Name = "E5" });
                    switchStates.Add(new WpfSwitch() { State = false, Name = "E6" });
                    switchStates.Add(new WpfSwitch() { State = false, Name = "E7" });
                    switchStates.Add(new WpfSwitch() { State = false, Name = "E8" });
                }
        }

        protected override void SpecificDataUpdated(SensorDataReport dataReport)
        {
            UInt32 arrayState = (UInt32)dataReport.GetDataField(SensorPropertyKeys.SENSOR_DATA_TYPE_BOOLEAN_SWITCH_ARRAY_STATE);
            ParseSwitchState(0, arrayState);
            ParseSwitchState(1, arrayState);
            ParseSwitchState(2, arrayState);
            ParseSwitchState(3, arrayState);

            //if ((arrayState & 0x1) != 0)    // first switch pressed
            //    switchStates[0].State = true;
            //else
            //    switchStates[0].State = false;

            //if ((arrayState & 0x2) != 0)    // second switch pressed
            //    switchStates[1].State = true;
            //else
            //    switchStates[1].State = false;

            //if ((arrayState & 0x4) != 0)    // third switch pressed
            //    switchStates[2].State = true;
            //else
            //    switchStates[2].State = false;

            //if ((arrayState & 0x8) != 0)    // fourth switch pressed
            //    switchStates[3].State = true;
            //else
            //    switchStates[3].State = false;
        }

        void ParseSwitchState(int switchNum,UInt32 arrayState)
        {
            bool oldState = switchStates[switchNum].State;
            if ((arrayState & (0x1 << switchNum)) != 0)
                switchStates[switchNum].State = true;
            else
                switchStates[switchNum].State = false;

            if (SwitchStateChanged != null && (oldState!=switchStates[switchNum].State))
                SwitchStateChanged(this, switchStates[switchNum].Name, switchStates[switchNum].State);
                
        }


        protected override void SpecificRecalibrate()
        {
            // no calibration for a switch array
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SensorWpf
{
    public class WpfSwitch : INotifyPropertyChanged
    {

        bool _state;
        public bool State
        {
            get { return _state; }
            internal set {
                if (_state == value)
                    return;
                _state = value;
                FirePropertyChanged("State");
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            internal set { 
                _name = value;
                FirePropertyChanged("Name");
            }
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

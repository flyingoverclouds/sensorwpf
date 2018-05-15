using System;
using System.Collections.Generic;
using System.Text;

namespace TronGame
{
    public class EndOfGameArgs : EventArgs
    {
        public EndOfGameArgs(int playerNumber)
        {
            _playerNumber = playerNumber;
        }

        private int _playerNumber;

        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
        }
    }
}

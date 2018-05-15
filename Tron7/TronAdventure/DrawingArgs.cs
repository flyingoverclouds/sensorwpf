using System;
using System.Collections.Generic;
using System.Text;

namespace TronAdventure
{
    public class DrawingArgs : EventArgs
    {
        public DrawingArgs(int playerNumber, Point2D from, Point2D to, Direction direction)
        {
            _playerNumber = playerNumber;
            _from = from;
            _to = to;
            _direction = direction;
        }

        private int _playerNumber;

        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
        }

        private Point2D _from;

        public Point2D From
        {
            get { return _from; }
            set { _from = value; }
        }

        private Point2D _to;

        public Point2D To
        {
            get { return _to; }
            set { _to = value; }
        }

        private Direction _direction;

        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
    }
}

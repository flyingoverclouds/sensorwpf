using System;
using System.Collections.Generic;
using System.Text;

namespace TronAdventure
{
    public class Player
    {
        public Player(int startX, int startY, Direction startDirection)
        {
            _startingPoint = new Point2D(startX, startY);
            _nextPoint = _startingPoint;

            _direction = startDirection;
        }

        private Point2D _startingPoint;
        private Point2D _nextPoint;

        internal Point2D NextPoint
        {
            get { return _nextPoint; }
            set { _nextPoint = value; }
        }
        private List<Point2D> _polyLine = new List<Point2D>();
        private Direction _direction;
        private Queue<ChangeDirection> _directionChanges = new Queue<ChangeDirection>();

        public void Turn(ChangeDirection change)
        {
            _directionChanges.Enqueue(change);
        }

        public Direction NextStep()
        {
            Direction newDirection = _direction;

            if (_directionChanges.Count == 0)
                newDirection = _direction;
            else
            {
                if (_directionChanges.Dequeue() == ChangeDirection.Left)
                    switch (_direction)
                    {
                        case Direction.Down:
                            newDirection = Direction.Right;
                            break;
                        case Direction.Right:
                            newDirection = Direction.Up;
                            break;
                        case Direction.Up:
                            newDirection = Direction.Left;
                            break;
                        case Direction.Left:
                            newDirection = Direction.Down;
                            break;
                    }
                else
                    switch (_direction)
                    {
                        case Direction.Down:
                            newDirection = Direction.Left;
                            break;
                        case Direction.Left:
                            newDirection = Direction.Up;
                            break;
                        case Direction.Up:
                            newDirection = Direction.Right;
                            break;
                        case Direction.Right:
                            newDirection = Direction.Down;
                            break;
                    }
            }

            switch (newDirection)
            {
                case Direction.Down:
                    _nextPoint.Y++;
                    break;
                case Direction.Left:
                    _nextPoint.X--;
                    break;
                case Direction.Right:
                    _nextPoint.X++;
                    break;
                case Direction.Up:
                    _nextPoint.Y--;
                    break;
            }

            _direction = newDirection;

            return newDirection;
        }
    }
}

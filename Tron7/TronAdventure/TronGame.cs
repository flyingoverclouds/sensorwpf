using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace TronAdventure
{
    public abstract partial class TronGame
    {
        public TronGame(int playerCount, int speed)
        {
            this.speed = speed;
        }

        private const int _rowCount = 100;
        private const int _colCount = 100;
        private int speed;

        private bool[,] _board = new bool[_colCount, _rowCount];

        private List<Player> _players = new List<Player>();

        private bool Contains(Point2D p)
        {
            return _board[p.X, p.Y];
        }

        private void SetPoint(Point2D p)
        {
            _board[p.X, p.Y] = true;
        }

        public void Start()
        {
            ClearBoard();

            _players.Add(new Player(1, 1, Direction.Right));

            CreateTimer(speed);
            StartTimer();
        }

        private void ClearBoard()
        {
            for (int i = 0; i < _rowCount; i++)
                for (int j = 0; j < _rowCount; j++)
                {
                    _board[i, j] = false;
                }
        }

        public void Stop()
        {
        }

        public void Turn(int playerNumber, ChangeDirection change)
        {
            _players[playerNumber].Turn(change);
        }

        public event EventHandler<DrawingArgs> NewDraw;
        public event EventHandler<EndOfGameArgs> EndOfGame;

        public void TickTimer(object sender, EventArgs args)
        {
            int playerNumber = 0;

            foreach (Player p in _players)
            {
                if (Contains(p.NextPoint))
                {
                    //Perdu
                    StopTimer();
                    if (EndOfGame != null)
                        EndOfGame(this, new EndOfGameArgs(playerNumber));
                    return;
                }
                else
                {
                    SetPoint(p.NextPoint);
                    Point2D oldPoint = p.NextPoint;
                    Direction d = p.NextStep();
                    if (OutOfBorder(p.NextPoint))
                    {
                        //Perdu
                        StopTimer();
                        if (EndOfGame != null)
                            EndOfGame(this, new EndOfGameArgs(playerNumber));
                        return;
                    }
                    else
                        if (NewDraw != null)
                            NewDraw(this, new DrawingArgs(playerNumber, oldPoint, p.NextPoint, d));
                        
                }
                playerNumber++;
            }
        }

        protected abstract void CreateTimer(int ms);
        protected abstract void StartTimer();
        protected abstract void StopTimer();
        
        private bool OutOfBorder(Point2D p)
        {
            return (p.X < 0) || (p.X >= _colCount) || (p.Y < 0) || (p.Y >= _rowCount);
        }
    }
}

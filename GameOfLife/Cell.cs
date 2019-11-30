using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Cell
    {
        private bool _state;

        public Cell(bool state = false)
        {
            _state = state;
        }

        public void SetToAlive()
        {
            _state = true;
        }

        public void SetToDie()
        {
            _state = false;
        }

        public char Print()
        {
            return _state ? 'X' : '.';
        }

        public bool IsAlive()
        {
            return _state;
        }

        public GridCellStatus GridStatus()
        {
            return _state ? GridCellStatus.Alive : GridCellStatus.Dead;
        }

        public void SetToRandomState()
        {
            _state = new Random().Next(100) % 2 == 0;
        }
    }
}

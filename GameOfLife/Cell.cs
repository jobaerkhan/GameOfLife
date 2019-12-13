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

        public CellStatus GridCellStatus()
        {
            return _state ? CellStatus.Alive : CellStatus.Dead;
        }

        public void SetToRandomState()
        {
            _state = new Random().Next(100) % 2 == 0;
        }
    }
}

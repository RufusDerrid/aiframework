using Assets.Code.world;
using System.Collections.Generic;

namespace Assets.Code.ai
{
    public class Path
    {
        private List<Cell> _pointCells;
        private int _currentIndex = 0;

        public Path(List<Cell> pointCells)
        {
            _pointCells = pointCells;
        }

        public Cell GetPointCell()
        {
            if (_pointCells != null && _pointCells.Count > 0 && _currentIndex < _pointCells.Count)
            {
                var position = _pointCells[_currentIndex];
                _currentIndex++;
                return position;
            }

            return null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.ai
{
    public class Path
    {
        private List<Transform> _points;
        private int _currentIndex = 0;

        public Path(List<Transform> points)
        {
            _points = points;
        }

        public Transform GetPoint()
        {
            if (_points != null && _points.Count > 0 && _currentIndex < _points.Count)
            {
                var transform = _points[_currentIndex];
                _currentIndex++;
                return transform;
            }

            return null;
        }
    }
}
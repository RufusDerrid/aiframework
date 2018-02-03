using UnityEngine;

namespace Assets.Code.world
{
    public class Cell
    {
        public Vector2 Position { get; private set; }

        private bool _walkable;
        private Transform _transform;

        public Cell(Transform transform, bool walkable)
        {
            _transform = transform;
            Position = new Vector2(_transform.position.x / 2, _transform.position.z / 2);
            _walkable = walkable;
        }

        public bool IsWalkable { get { return _walkable; } }
    }
}
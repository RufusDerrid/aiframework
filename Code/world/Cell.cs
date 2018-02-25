using UnityEngine;

namespace Assets.Code.world
{
    public class Cell
    {
        public Vector3 Position { get { return _gameObject.transform.position; } }
        public GameObject GameObject { get { return _gameObject; } }

        private bool _walkable;
        private GameObject _gameObject;

        public Cell(GameObject gameObject, bool walkable)
        {
            _gameObject = gameObject;
            _walkable = walkable;
        }

        public bool IsWalkable { get { return _walkable; } }

        public void ChangeColor(Color color)
        {
            var rend = _gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = color;
            }
        }

        public void Dispose()
        {
            GameObject.Destroy(_gameObject);
        }
    }
}
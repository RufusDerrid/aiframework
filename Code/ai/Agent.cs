using Assets.Code.ai.kinematics;
using UnityEngine;

namespace Assets.Code.ai
{
    public class Agent
    {
        public Kinematic Kinematic {get { return _kinematic; } }

        private Transform _transform;
        private Kinematic _kinematic;

        public Agent(Transform transform)
        {
            _transform = transform;
            _kinematic = new Kinematic();
            _kinematic.Position = new Vector2(_transform.position.x / 2, _transform.position.z / 2);
            _kinematic.Orientation = _transform.rotation.eulerAngles.y / 90;
        }

        public void Update(SteeringOutput steering)
        {
            _kinematic.Update(steering);
            _transform.position = new Vector3(_kinematic.Position.x * 2, _transform.position.y, _kinematic.Position.y * 2);
            Debug.Log(_kinematic.Orientation);
            _transform.rotation = Quaternion.Euler(0, _kinematic.Orientation * 90, 0);
        }

        public void ChangePosition(Vector2 position)
        {
            _kinematic.Position = position;
            _transform.position = new Vector3(_kinematic.Position.x * 2, _transform.position.y, _kinematic.Position.y * 2);
        }
    }
}
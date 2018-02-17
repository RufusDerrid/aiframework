using UnityEngine;

namespace Assets.Code.ai
{
    public class Agent
    {
        public Transform Transform;
        public Vector3 Velocity;
        //private float _rotation;

        public Agent(Transform transform)
        {
            Transform = transform;
        }

        public void Update(SteeringOutput steering)
        {
            Transform.position += Velocity * Time.deltaTime;

            var eulers = Transform.rotation.eulerAngles;
            var y = eulers.y + steering.Rotation * Time.deltaTime;
            //_transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
            var vec = new Vector3(0, steering.Rotation, 0) * Time.deltaTime;
            var vec2 = Vector3.up * Time.deltaTime;
            Transform.Rotate(Vector3.up, steering.Rotation * Time.deltaTime);

            //_velocity += steering.Velocity * Time.deltaTime;
            //_rotation += steering.Rotation * Time.deltaTime;

            Velocity = steering.Velocity;
        }
    }
}
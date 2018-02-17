using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Wander : ISteeringBehavior
    {
        private Agent _character;
        private float _maxSpeed;
        private float _maxRotation;

        public Wander(Agent character, float maxSpeed, float maxRotation)
        {
            _character = character;
            _maxSpeed = maxSpeed;
            _maxRotation = maxRotation;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            var angleInRad = _character.Transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            var direction = _maxSpeed * KinematicUtils.VectorFromAngle(angleInRad);
            steering.Velocity = new Vector3(direction.x, 0, direction.y);

            steering.Rotation = Random.Range(-1.0f, 1.1f) * _maxRotation;

            return steering;
        }
    }
}

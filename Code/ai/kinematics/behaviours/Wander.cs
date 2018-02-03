using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Wander : ISteeringBehavior
    {
        private Kinematic _character;
        private float _maxSpeed;
        private int _rotationMaxTime;
        private float _rotationTime;

        public Wander(Kinematic character, float maxSpeed, int rotationMaxTime)
        {
            _character = character;
            _maxSpeed = maxSpeed;
            _rotationMaxTime = rotationMaxTime;
            _rotationTime = 0.0f;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            _rotationTime += Time.deltaTime;
            if (_rotationTime > _rotationMaxTime)
            {
                _rotationTime = 0;
                var rotation = Random.Range(0, 4);
                _character.Orientation = rotation;
                //_character.Orientation += rotation;
                //if(_character.Orientation > 3)
                //{
                //    _character.Orientation %= 3;
                //}

            }

            steering.Velocity = _maxSpeed * KinematicUtils.VectorFromAngle(_character.Orientation * 90 * Mathf.Deg2Rad);


            return steering;
        }
    }
}

using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Arrive : ISteeringBehavior
    {
        private Kinematic _character;
        private Kinematic _target;
        private float _maxSpeed;
        private float _radius;

        public Arrive(Kinematic character, Kinematic target, float maxSpeed, float radius)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
            _radius = radius;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            var distance = _target.Position - _character.Position;
            
            if (distance.magnitude <= _radius)
            {
                return steering;
            }

            if (distance.magnitude > _maxSpeed)
            {
                distance.Normalize();

                distance.x = Mathf.Round(distance.x);
                distance.y = Mathf.Round(distance.y);

                if (distance.x != 0)
                {
                    distance.y = 0;
                }

                distance *= _maxSpeed;
            }

            steering.Velocity = distance;
           
            return steering;
        }
    }
}
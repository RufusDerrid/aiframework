using System.Collections.Generic;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Separation : ISteeringBehavior
    {
        private Agent _character;
        private List<Agent> _targets;
        private float _maxSpeed;
        private float _treshold;

        public Separation(Agent character, List<Agent> targets, float maxSpeed, float treshold)
        {
            _character = character;
            _targets = targets;
            _maxSpeed = maxSpeed;
            _treshold = treshold;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            foreach (var target in _targets)
            {
                var direction = _character.Transform.position - target.Transform.position;
                var distance = direction.magnitude;
                if (distance < _treshold)
                {
                    steering.Velocity += direction;
                }
            }

            steering.Velocity.Normalize();

            steering.Velocity *= _maxSpeed;

            return steering;
        }
    }
}

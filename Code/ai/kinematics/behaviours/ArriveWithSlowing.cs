namespace Assets.Code.ai.kinematics.behaviours
{
    public class ArriveWithSlowing : ISteeringBehavior
    {
        private Agent _character;
        private Agent _target;
        private float _maxSpeed;
        private float _radius;
        private float _timeToTarget;

        public ArriveWithSlowing(Agent character, Agent target, float maxSpeed, float radius, float timeToTarget)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
            _radius = radius;
            _timeToTarget = timeToTarget;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            steering.Velocity = _target.Transform.position - _character.Transform.position;

            if (steering.Velocity.magnitude <= _radius)
            {
                return new SteeringOutput();
            }

            steering.Velocity /= _timeToTarget;

            if (steering.Velocity.magnitude > _maxSpeed)
            {
                steering.Velocity.Normalize();
                steering.Velocity *= _maxSpeed;
            }

            return steering;
        }
    }
}
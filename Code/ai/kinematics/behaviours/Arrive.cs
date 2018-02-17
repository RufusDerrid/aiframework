namespace Assets.Code.ai.kinematics.behaviours
{
    public class Arrive : ISteeringBehavior
    {
        private Agent _character;
        private Agent _target;
        private float _maxSpeed;
        private float _radius;

        public Arrive(Agent character, Agent target, float maxSpeed, float radius)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
            _radius = radius;
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            steering.Velocity = _target.Transform.position - _character.Transform.position;

            if (steering.Velocity.magnitude <= _radius)
            {
                return new SteeringOutput();
            }

            if (steering.Velocity.magnitude > _maxSpeed)
            {
                steering.Velocity.Normalize();
                steering.Velocity *= _maxSpeed;
            }

            return steering;
        }
    }
}
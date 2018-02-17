namespace Assets.Code.ai.kinematics.behaviours
{
    public class Flee : ISteeringBehavior
    {
        private Agent _character;
        private Agent _target;
        private float _maxSpeed;

        public Flee(Agent character, Agent target, float maxSpeed)
        {
            _character = character;
            _target = target;
            _maxSpeed = maxSpeed;
        }

        public SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            steering.Velocity = _character.Transform.position - _target.Transform.position;
            steering.Velocity.Normalize();

            steering.Velocity *= _maxSpeed;

            return steering;
        }
    }
}

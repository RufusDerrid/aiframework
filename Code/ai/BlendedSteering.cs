using Assets.Code.ai.kinematics.behaviours;
using System.Collections.Generic;

namespace Assets.Code.ai
{
    public class BlendedSteering
    {
        private List<BehaviorAndWeight> _behaviors;
        private float _maxSpeed;

        public BlendedSteering()
        {
            _behaviors = new List<BehaviorAndWeight>();
            _maxSpeed = 4;
        }

        public void AddBehavior(ISteeringBehavior behavior, float weight)
        {
            _behaviors.Add(new BehaviorAndWeight(behavior, weight));
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            foreach (var behavior in _behaviors)
            {
                steering += behavior.Weight * behavior.Behavior.GetSteering();

                steering.Velocity.Normalize();

                steering.Velocity *= _maxSpeed;
            }

            return steering;
        }
    }
}

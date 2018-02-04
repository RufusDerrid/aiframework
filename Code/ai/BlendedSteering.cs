using Assets.Code.ai.kinematics.behaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.ai
{
    public class BlendedSteering
    {
        private List<BehaviorAndWeight> _behaviors;
        private float _maxSpeed;

        public BlendedSteering()
        {
            _behaviors = new List<BehaviorAndWeight>();
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

                steering.Velocity.x = Mathf.Round(steering.Velocity.x);
                steering.Velocity.y = Mathf.Round(steering.Velocity.y);

                if (steering.Velocity.x != 0)
                {
                    steering.Velocity.y = 0;
                }
            }

            return steering;
        }
    }
}

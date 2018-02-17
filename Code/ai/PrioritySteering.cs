using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.ai
{
    public class PrioritySteering
    {
        private List<BlendedSteering> _groups;
        private float _epsilon;

        public PrioritySteering(float epsilon)
        {
            _epsilon = epsilon;
            _groups = new List<BlendedSteering>();
        }

        public void AddGroup(BlendedSteering blendedSteering)
        {
            _groups.Add(blendedSteering);
        }

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();

            foreach(var group in _groups)
            {
                steering = group.GetSteering();

                if(steering.Velocity.magnitude > _epsilon || Mathf.Abs(steering.Rotation) > _epsilon)
                {
                    return steering;
                }
            }

            var lastGroup = _groups.Last();
            if (lastGroup != null)
            {
                steering = lastGroup.GetSteering();
            }

            return steering;
        }
    }
}

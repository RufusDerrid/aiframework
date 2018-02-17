using Assets.Code.world;
using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class ObstacleAvoidance : Seek
    {
        private float _avoidDistance;
        private float _lookahead;

        public ObstacleAvoidance(Agent character, float avoidDistance, float lookahead, float maxSpeed)
            : base(character, null, maxSpeed)
        {
            _avoidDistance = avoidDistance;
            _lookahead = lookahead;
            var proxyTarget = new GameObject("proxyTarget");
            _target = new Agent(proxyTarget.transform);
        }

        public override SteeringOutput GetSteering()
        {
            var rayVector = _character.Velocity;
            rayVector.Normalize();
            rayVector *= _lookahead;

            RaycastHit hit;
            Ray ray = new Ray(_character.Transform.position, rayVector);
            if (Physics.Raycast(ray, out hit))
            {
                _target.Transform.position = hit.point + hit.normal * _avoidDistance;
            }
            else
            {
                return new SteeringOutput();
            }

            return base.GetSteering();
        }
    }
}

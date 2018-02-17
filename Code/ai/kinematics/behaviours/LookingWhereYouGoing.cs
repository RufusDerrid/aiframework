using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class LookingWhereYouGoing : Align
    {
        public LookingWhereYouGoing(Agent character, float maxRotation)
            : base(character, null, maxRotation)
        {
            var proxyTarget = new GameObject("proxyTarget");
            _target = new Agent(proxyTarget.transform);
        }

        public override SteeringOutput GetSteering()
        {
            if (_character.Velocity.sqrMagnitude == 0)
            {
                return new SteeringOutput();
            }

            var rotation = Mathf.Atan2(-_character.Velocity.z, _character.Velocity.x) * Mathf.Rad2Deg;

            _target.Transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

            return base.GetSteering();
        }
    }
}

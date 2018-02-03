using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class LookingWhereYouGoing : Align
    {
        public LookingWhereYouGoing(Kinematic character, float maxRotation)
            : base(character, new Kinematic(), maxRotation)
        {}

        public override SteeringOutput GetSteering()
        {
            if(_character.Velocity.sqrMagnitude == 0)
            {
                return new SteeringOutput();
            }

            _target.Orientation = Mathf.Atan2(-_character.Velocity.y, _character.Velocity.x);

            return base.GetSteering();
        }
    }
}

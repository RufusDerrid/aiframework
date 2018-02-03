using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Face : Align
    {
        private Kinematic _originalTarget;

        public Face(Kinematic character, Kinematic target, float maxRotation) 
            : base(character, target, maxRotation)
        {
            _originalTarget = target;
        }

        public override SteeringOutput GetSteering()
        {
            var direction = _originalTarget.Position - _character.Position;
            if (direction.sqrMagnitude == 0)
            {
                return new SteeringOutput();
            }

            _target = new Kinematic();
            var orientation = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;

            orientation = Mathf.Round(orientation / 90);

            if(orientation < 0)
            {
                orientation += 4;
            } else if(orientation > 3)
            {
                orientation %= 4;
            }

            _target.Orientation = orientation;

            return base.GetSteering();
        }
    }
}
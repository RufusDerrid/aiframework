using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class Face : Align
    {
        private Agent _originalTarget;

        public Face(Agent character, Agent target, float maxRotation)
            : base(character, null, maxRotation)
        {
            _originalTarget = target;
            var faceTarget = new GameObject("faceTarget");
            _target = new Agent(faceTarget.transform);
        }

        public override SteeringOutput GetSteering()
        {
            var direction = _originalTarget.Transform.position - _character.Transform.position;
            if (direction.sqrMagnitude == 0)
            {
                return new SteeringOutput();
            }

            var rotation = Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;

            _target.Transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

            return base.GetSteering();
        }
    }
}
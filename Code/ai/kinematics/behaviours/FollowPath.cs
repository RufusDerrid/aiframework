using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class FollowPath : Seek
    {
        private Path _path;
        private float _radius;

        public FollowPath(Path path, float radius, Agent character, float maxSpeed)
            : base(character, null, maxSpeed)
        {
            _path = path;
            _radius = radius;
            var auxTarget = new GameObject("auxTarget");
            _target = new Agent(auxTarget.transform);
            _target.Transform = _path.GetPoint();
        }

        public override SteeringOutput GetSteering()
        {
            var newOutput = base.GetSteering();

            var distance = (_target.Transform.position - _character.Transform.position).magnitude;

            if (distance <= _radius)
            {
                var point = _path.GetPoint();
                if (point != null)
                {
                    _target.Transform = point;
                }
            }

            return newOutput;
        }
    }
}

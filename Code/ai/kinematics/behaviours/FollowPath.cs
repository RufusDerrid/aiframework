using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class FollowPath : Arrive
    {
        private Path _path;
        private float _radius;

        public FollowPath(Path path, float radius, Agent character, float maxSpeed)
            : base(character, null, maxSpeed, radius)
        {
            _path = path;
            _radius = radius;
            var auxTarget = new GameObject("auxTarget");
            _target = new Agent(auxTarget.transform);
            _target.Transform = _path.GetPoint();
        }

        public void UpdateTarget()
        {
            _target.Transform = _path.GetPoint();
        }

        public override SteeringOutput GetSteering()
        {
            if (_target.Transform != null)
            {
                var newOutput = base.GetSteering();

                if (newOutput.Velocity.magnitude == 0.0f)
                {
                    var point = _path.GetPoint();
                    _target.Transform = point;
                }

                return newOutput;
            }

            return new SteeringOutput();
        }

        //public override SteeringOutput GetSteering()
        //{
        //    if (_target.Transform != null)
        //    {
        //        var newOutput = base.GetSteering();

        //        var distance = (_target.Transform.position - _character.Transform.position).magnitude;

        //        if (distance <= _radius)
        //        {
        //            var point = _path.GetPoint();

        //            if (point == null)
        //            {
        //                return new SteeringOutput();
        //            }

        //            _target.Transform = point;
        //        }

        //        return newOutput;
        //    }

        //    return new SteeringOutput();
        //}
    }
}

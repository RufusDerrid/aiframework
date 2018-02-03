namespace Assets.Code.ai.kinematics.behaviours
{
    public class FollowPath : Seek
    {
        private Path _path;

        public FollowPath(Path path, Kinematic character, float maxSpeed) 
            : base(character, new Kinematic(), maxSpeed)
        {
            _path = path;
            _target.Position = _path.GetPointCell().Position;
        }

        public override SteeringOutput GetSteering()
        {
            var newOutput = base.GetSteering();

            if(newOutput.Velocity.sqrMagnitude == 0.0f)
            {
                var pointCell = _path.GetPointCell();
                if (pointCell != null)
                {
                    _target.Position = pointCell.Position;
                }
            }

            return newOutput;
        }
    }
}

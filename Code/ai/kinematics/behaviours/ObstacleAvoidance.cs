using Assets.Code.world;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class ObstacleAvoidance : Seek
    {
        private World _world;

        public ObstacleAvoidance(Kinematic character, Kinematic target, float maxSpeed) 
            : base(character, new Kinematic(), maxSpeed)
        {
        }

        public override SteeringOutput GetSteering()
        {
            var rayVector = _character.Velocity;
            rayVector.Normalize();

            var nextPosition = _character.Position + rayVector;

            if(_world.CanWalk(nextPosition))
            {
                return new SteeringOutput();
            }

            var nearestWalkableCell = _world.GetNearestWalkableCell(_character.Position, nextPosition);

            _target.Position = nearestWalkableCell.Position;

            return base.GetSteering();
        }
    }
}

using Assets.Code.world;
using UnityEngine;

namespace Assets.Code.ai.kinematics.behaviours
{
    public class ObstacleAvoidance : Seek
    {
        private World _world;

        public ObstacleAvoidance(Kinematic character, float maxSpeed, World world) 
            : base(character, new Kinematic(), maxSpeed)
        {
            _world = world;
        }

        public override SteeringOutput GetSteering()
        {
            var rayVector = _character.Velocity;
            rayVector.Normalize();

            if(rayVector.x == 1 && _character.Position.x >= 1 && _character.Position.y >= 1)
            {
                Debug.Log("");
            }

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

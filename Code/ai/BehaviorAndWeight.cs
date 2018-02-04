using Assets.Code.ai.kinematics.behaviours;

namespace Assets.Code.ai
{
    public class BehaviorAndWeight
    {
        public ISteeringBehavior Behavior { get { return _behavior; } }
        public float Weight { get { return _weight; } }

        private ISteeringBehavior _behavior;
        private float _weight;

        public BehaviorAndWeight(ISteeringBehavior behavior, float weight)
        {
            _behavior = behavior;
            _weight = weight;
        }
    }
}

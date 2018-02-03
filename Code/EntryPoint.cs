using Assets.Code.ai;
using Assets.Code.ai.kinematics;
using Assets.Code.ai.kinematics.behaviours;
using Assets.Code.world;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    //public List<Transform> TargetsTransforms;

    public Transform TargetCharacterTransform;
    public Transform CharacterTransform;

    private Agent _character;
    private Agent _targetAgent;
   
    private ISteeringBehavior _behaviour1;
    private ISteeringBehavior _behaviour2;
    private float _timer;

	void Start ()
    {
        var world = new World();
        //var nearestCell = world.GetNearestWalkableCell(new Vector2(1, 0), new Vector2(2, 0));

        _timer = 0;
        _character = new Agent(CharacterTransform);

        _targetAgent = new Agent(TargetCharacterTransform);

        //_behaviour = new KinematicSeek(_character.Kinematic, targetKinematic, 2);
        //_behaviour = new Flee(_character.Kinematic, targetKinematic, 3);
        //_behaviour1 = new Arrive(_character.Kinematic, targetKinematic, 1, 1);
        //_behaviour1 = new Wander(_targetAgent.Kinematic, 3, 2);
        //_behaviour1 = new Evade(_character.Kinematic, _targetAgent.Kinematic, 1, 5);
        //_behaviour1 = new Flee(_character.Kinematic, targetKinematic, 1);
        //_behaviour1 = new Face(_character.Kinematic, targetKinematic, 1);

        //var targetsKinematics = new List<Kinematic>();

        //foreach(var targetTransform in TargetsTransforms)
        //{
        //    var agent = new Agent(targetTransform);
        //    targetsKinematics.Add(agent.Kinematic);
        //}

        //_behaviour1 = new Separation(_character.Kinematic, targetsKinematics, 1, 1);
    }
	
	void Update ()
    {
        _timer = 0.0f;
        _character.Update(_behaviour1.GetSteering());
        //_targetAgent.Update(_behaviour2.GetSteering());
    }
}
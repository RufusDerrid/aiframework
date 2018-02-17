using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriritySteeringScene : MonoBehaviour
{
    public Transform TargetCharacterTransform;
    public Transform CharacterTransform;

    private Agent _character;
    private Agent _targetAgent;

    private PrioritySteering _prioritySteering;

    void Start ()
    {
        _targetAgent = new Agent(TargetCharacterTransform);
        _character = new Agent(CharacterTransform);

        _prioritySteering = new PrioritySteering(0.01f);

        var firstGroup = new BlendedSteering();
        var obstAvoidance = new ObstacleAvoidance(_character, 10, 5, 3);
        firstGroup.AddBehavior(obstAvoidance, 1);
        _prioritySteering.AddGroup(firstGroup);

        var secondGroup = new BlendedSteering();
        var seek = new Seek(_character, _targetAgent, 3);
        //var looking = new LookingWhereYouGoing(_character, 100);
        secondGroup.AddBehavior(seek, 1);
        _prioritySteering.AddGroup(secondGroup);
    }
	
	void Update ()
    {
        _character.Update(_prioritySteering.GetSteering());
	}
}

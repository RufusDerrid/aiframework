using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public Transform TargetCharacterTransform;
    public Transform CharacterTransform;

    private Agent _character;
    private Agent _targetAgent;

    private ISteeringBehavior _behavior;

	void Start ()
    {
        _targetAgent = new Agent(TargetCharacterTransform);
        _character = new Agent(CharacterTransform);

        _behavior = new Face(_character, _targetAgent, 10);
    }
	
	void Update ()
    {
        _character.Update(_behavior.GetSteering());
    }
}
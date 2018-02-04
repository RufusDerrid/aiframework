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
    public List<Transform> CharacterTransforms;

    private List<Agent> _characters;
    private Agent _targetAgent;
    private Separation _separation;

    private List<BlendedSteering> _blendedSteerings;

	void Start ()
    {
        var world = new World();
        //var nearestCell = world.GetNearestWalkableCell(new Vector2(1, 0), new Vector2(2, 0));

        _targetAgent = new Agent(TargetCharacterTransform);

        _blendedSteerings = new List<BlendedSteering>();
        
        foreach (var characterTransform in CharacterTransforms)
        {
            _characters.Add(new Agent(characterTransform));
        }

        foreach (var character in _characters)
        {
            var seek = new Seek(character.Kinematic, _targetAgent.Kinematic, 1);

            var otherChars = new List<Kinematic>();            
            foreach (var otherChar in _characters)
            {
                if(otherChar != character)
                {
                    otherChars.Add(otherChar.Kinematic);
                }
            }

            var separation = new Separation(character.Kinematic, otherChars, 1, 2);

            var blendedSteering = new BlendedSteering();
            blendedSteering.AddBehavior(seek, 1);
            blendedSteering.AddBehavior(separation, 1);

            _blendedSteerings.Add(blendedSteering);
        }

        

        //_seek = new Seek(_character.Kinematic, _targetAgent.Kinematic, 1);
        //_separation = new Separation(_character.Kinematic)
        ////_obstAvoid = new ObstacleAvoidance(_character.Kinematic, 1, world);

        //_blendedSteering = new BlendedSteering();
        //_blendedSteering.AddBehavior(_seek, 1);
        //_blendedSteering.AddBehavior(_obstAvoid, 2);

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
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].Update(_blendedSteerings[i].GetSteering());
        }
        //_character.Update(_blendedSteering.GetSteering());
        //_targetAgent.Update(_behaviour2.GetSteering());
    }
}
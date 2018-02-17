using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingScene : MonoBehaviour
{
    public Transform TargetCharacterTransform;
    public List<Transform> CharacterTransforms;

    private List<Agent> _characters;
    private Agent _targetAgent;
    private List<BlendedSteering> _blendedSteerings;

    void Start ()
    {
        _targetAgent = new Agent(TargetCharacterTransform);

        _blendedSteerings = new List<BlendedSteering>();
        _characters = new List<Agent>();

        foreach (var characterTransform in CharacterTransforms)
        {
            _characters.Add(new Agent(characterTransform));
        }

        //var wander = new Wander(_characters[0], 3, 100);
        //var firstBlendedSteering = new BlendedSteering();
        //firstBlendedSteering.AddBehavior(wander, 1);
        //_blendedSteerings.Add(firstBlendedSteering);

        //for (int i=1; i < _characters.Count; i++)
        //{
        //    var arrive = new Arrive(_characters[i], _characters[0], 3, 2);

        //    var otherChars = new List<Agent>();
        //    foreach (var otherChar in _characters)
        //    {
        //        if (otherChar != _characters[i])
        //        {
        //            otherChars.Add(otherChar);
        //        }
        //    }
        //    var separation = new Separation(_characters[i], otherChars, 3, 2);

        //    var looking = new LookingWhereYouGoing(_characters[i], 100);

        //    var blendedSteering = new BlendedSteering();
        //    //blendedSteering.AddBehavior(arrive, 1);
        //    blendedSteering.AddBehavior(seek, 1);
        //    blendedSteering.AddBehavior(separation, 2);
        //    blendedSteering.AddBehavior(looking, 1);

        //    _blendedSteerings.Add(blendedSteering);
        //}

        foreach (var character in _characters)
        {
            var seek = new Seek(character, _targetAgent, 3);

            var arrive = new Arrive(character, _targetAgent, 3, 5);

            var otherChars = new List<Agent>();
            foreach (var otherChar in _characters)
            {
                if (otherChar != character)
                {
                    otherChars.Add(otherChar);
                }
            }

            var separation = new Separation(character, otherChars, 3, 2);

            var looking = new LookingWhereYouGoing(character, 100);

            var obstAvoid = new ObstacleAvoidance(character, 2, 10, 3);

            var blendedSteering = new BlendedSteering();
            //blendedSteering.AddBehavior(arrive, 1);
            blendedSteering.AddBehavior(seek, 1);
            blendedSteering.AddBehavior(separation, 2);
            blendedSteering.AddBehavior(looking, 1);
            blendedSteering.AddBehavior(obstAvoid, 10);

            _blendedSteerings.Add(blendedSteering);
        }
    }

	void Update ()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].Update(_blendedSteerings[i].GetSteering());
        }
    }
}

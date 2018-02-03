using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using Assets.Code.world;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingScene : MonoBehaviour
{
    public Transform CharacterTransform;
    public List<Transform> PointCellsTransforms;

    private Path _path;
    private ISteeringBehavior _followPath;
    private Agent _character;

	void Start ()
    {
        var pointCells = new List<Cell>();

        foreach(var cellTransform in PointCellsTransforms)
        {
            var cell = new Cell(cellTransform, true);
            pointCells.Add(cell);
        }

        _path = new Path(pointCells);

        _character = new Agent(CharacterTransform);

        _followPath = new FollowPath(_path, _character.Kinematic, 1);
	}
	
	void Update ()
    {
        _character.Update(_followPath.GetSteering());
	}
}

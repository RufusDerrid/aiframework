using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using Assets.Code.ai.pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingScene : MonoBehaviour
{
    public Transform CharacterTransform;
    public List<Transform> PointCellsTransforms;
    public string Map = "new.map";

    private Path _path;
    private ISteeringBehavior _followPath;
    private Agent _character;
    private Graph _graph;

	void Start ()
    {
        _graph = new Graph();

        string path = Application.dataPath + "/Maps/" + Map;
        _graph.ParseFromMap(path);

        _path = new Path(PointCellsTransforms);

        _character = new Agent(CharacterTransform);

        _followPath = new FollowPath(_path, 1, _character, 3);
	}
	
	void Update ()
    {
        _character.Update(_followPath.GetSteering());
	}
}

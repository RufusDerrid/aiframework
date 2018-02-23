using Assets.Code;
using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using Assets.Code.ai.pathfinding;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathFollowingScene : MonoBehaviour
{
    public Transform CharacterTransform;
    public List<Transform> PointCellsTransforms;
    public string Map = "new.map";
    public GameObject VertexPrefab;
    public GameObject ObstaclePrefab;
    public int CellSize = 10;
    public GameObject CubsGenPrefab;

    private Assets.Code.ai.Path _path;
    private FollowPath _followPath;
    private Agent _character;
    private Graph _graph;
    private CubesGenerator _cubesGen;

    private List<GameObject> _map;

    void Start ()
    {
        _cubesGen = new CubesGenerator(CubsGenPrefab);

        _map = new List<GameObject>();

        _graph = new Graph();

        string path = Application.dataPath + "/Maps/" + Map;
        _graph.ParseFromMap(path);
        //_graph.CreateTest();
        var connections = _graph.PathfindDijkstra(_graph._nodes[0], _graph._nodes[9]);

        InitMap(path);
        //var points = GetPointsFromConnections(connections, true);
        _path = new Assets.Code.ai.Path(null);

        _character = new Agent(CharacterTransform);

        _followPath = new FollowPath(_path, 0.2f, _character, 3);
    }

    private void CreateRandomMap(int rows, int cols, int frequency)
    {
        if(_map.Count > 0)
        {
            foreach(var cell in _map)
            {
                Destroy(cell);
            }

            _map.Clear();
        }

        Vector3 position = new Vector3();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                position.x = j * CellSize;
                position.z = i * CellSize;
                var t = UnityEngine.Random.Range(0, 100);
                if (t > frequency)
                {
                    _map.Add(Instantiate(ObstaclePrefab, position, Quaternion.identity) as GameObject);
                }
                else
                {
                    _map.Add(Instantiate(VertexPrefab, position, Quaternion.identity) as GameObject);
                }
            }
        }
    }

    private void InitMap(string path)
    {
        try
        {
            StreamReader streamReader = new StreamReader(path);
            using (streamReader)
            {
                _map = new List<GameObject>();

                string line = streamReader.ReadLine();
                line = streamReader.ReadLine();
                var rows = int.Parse(line.Split(' ')[1]);
                line = streamReader.ReadLine();
                var cols = int.Parse(line.Split(' ')[1]);
                line = streamReader.ReadLine();

                Vector3 position = new Vector3();
                

                for (int i = 0; i < rows; i++)
                {
                    line = streamReader.ReadLine();
                    for (int j = 0; j < cols; j++)
                    {
                        position.x = j * CellSize;
                        position.z = i * CellSize;

                        if (line[j] == '.')
                        {
                            _map.Add(Instantiate(VertexPrefab, position, Quaternion.identity) as GameObject);
                        }
                        else
                        {
                            _map.Add(Instantiate(ObstaclePrefab, position, Quaternion.identity) as GameObject);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private List<Transform> GetPointsFromConnections(List<Connection> connections, bool drawPath)
    {
        foreach (var node in _graph._nodes)
        {
            if (drawPath)
            {
                foreach (var cell in _map)
                {
                    int j = (int)cell.transform.position.x / CellSize;
                    int i = (int)cell.transform.position.z / CellSize;

                    var nodePos = node.GetPosition();
                    if ((int)nodePos.x == j && (int)nodePos.y == i)
                    {
                        var rend = cell.GetComponent<Renderer>();
                        if (rend != null)
                        {
                            rend.material.color = Color.green;
                        }
                    }
                }
            }
        }

        var points = new List<Transform>();

        var nodes = new List<Node>();

        foreach (var connection in connections)
        {
            var nodeFrom = connection.GetFromNode();
            if (!nodes.Contains(nodeFrom))
            {
                nodes.Add(nodeFrom);
            }

            var nodeTo = connection.GetToNode();
            if (!nodes.Contains(nodeTo))
            {
                nodes.Add(nodeTo);
            }
        }

        foreach (var node in nodes)
        {
            foreach (var cell in _map)
            {
                int j = (int)cell.transform.position.x / CellSize;
                int i = (int)cell.transform.position.z / CellSize;

                var nodePos = node.GetPosition();
                if ((int)nodePos.x == j && (int)nodePos.y == i)
                {
                    points.Add(cell.transform);

                    if (drawPath)
                    {
                        var rend = cell.GetComponent<Renderer>();
                        if (rend != null)
                        {
                            rend.material.color = Color.red;
                        }
                    }
                }
            }
        }

        return points;
    }
	
	void Update ()
    {
        _character.Update(_followPath.GetSteering());

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                int counter = 0;
                foreach (var cell in _map)
                {
                    if (cell == hitInfo.transform.gameObject)
                    {
                        var posStart = new Vector2(Mathf.Round(_character.Transform.position.x), Mathf.Round(_character.Transform.position.z));
                        var nodeStart = _graph.GetNodeByPosition(posStart);
                        var posEnd = new Vector2(cell.transform.position.x, cell.transform.position.z);
                        var nodeEnd = _graph.GetNodeByPosition(posEnd);

                        var startNodeId = nodeStart.GetId();
                        var endNodeId = nodeEnd.GetId();

                        var connections = _graph.PathfindDijkstra(_graph._nodes[startNodeId], _graph._nodes[endNodeId]);
                        var points = GetPointsFromConnections(connections, true);
                        _path.AddNiewPoints(points);
                        _followPath.UpdateTarget();
                        break;
                    }

                    counter++;
                }
            }
        }
    }
}

using Assets.Code;
using Assets.Code.ai;
using Assets.Code.ai.kinematics.behaviours;
using Assets.Code.ai.pathfinding;
using Assets.Code.world;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathFollowingScene : MonoBehaviour
{
    public Transform CharacterTransform;
    public Transform MonsterTransform;

    public List<Transform> PointCellsTransforms;
    public string Map = "new.map";
    public GameObject VertexPrefab;
    public GameObject ObstaclePrefab;
    public GameObject KeyPrefab;
    public GameObject DoorPrefab;
    public int CellSize = 10;
    //public GameObject CubsGenPrefab;

    private Assets.Code.ai.Path _path;
    private Assets.Code.ai.Path _monsterPath;
    private FollowPath _followPath;
    private FollowPath _monsterFollowPath;
    private Agent _character;
    private Agent _monster;
    private Graph _graph;
    //private CubesGenerator _cubesGen;

    private List<Cell> _map;
    private float _time;

    private List<GameObject> _keys;
    private GameObject _door;

    public void GettingKey(GameObject key)
    {
        _keys.Remove(key);

        if(_keys.Count == 0)
        {
            CreateDoor();
        }
    }

    public void Victory()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void CreateDoor()
    {
        var pos = new Vector3(UnityEngine.Random.Range(1, 8),
                0.56f, UnityEngine.Random.Range(1, 7));
        _door = Instantiate(DoorPrefab, pos, Quaternion.identity) as GameObject;
        _door.name = "door";
    }

    void Start ()
    {
        _keys = new List<GameObject>();

        _time = 0;

        //_cubesGen = new CubesGenerator(CubsGenPrefab);

        _map = new List<Cell>();

        string path = Application.dataPath + "/Maps/" + Map;
        //InitMap(path);

        CreateRandomMap(7, 8, 60);

        _graph = new Graph();
        _graph.ParseFromMap(_map);
        
        //var points = GetPointsFromConnections(connections, true);
        _path = new Assets.Code.ai.Path(null);
        _monsterPath = new Assets.Code.ai.Path(null);

        _character = new Agent(CharacterTransform);
        _monster = new Agent(MonsterTransform);

        _followPath = new FollowPath(_path, 0.2f, _character, 3);
        _monsterFollowPath = new FollowPath(_monsterPath, 0.2f, _monster, 3);

        CreateKeys();
    }

    private void CreateRandomMap(int rows, int cols, int frequency)
    {
        if(_map.Count > 0)
        {
            foreach(var cell in _map)
            {
                cell.Dispose();
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
                int charX = Mathf.RoundToInt(CharacterTransform.position.x);
                int charZ = Mathf.RoundToInt(CharacterTransform.position.z);
                int monsterX = Mathf.RoundToInt(MonsterTransform.position.x);
                int monsterZ = Mathf.RoundToInt(MonsterTransform.position.z);

                if (t > frequency && charX != position.x && charZ != position.z
                    && monsterX != position.x && monsterZ != position.z)
                {
                    _map.Add(new Cell(Instantiate(ObstaclePrefab, position, Quaternion.identity) as GameObject, false));
                }
                else
                {
                    _map.Add(new Cell(Instantiate(VertexPrefab, position, Quaternion.identity) as GameObject, true));
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
                _map = new List<Cell>();

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
                            _map.Add(new Cell(Instantiate(VertexPrefab, position, Quaternion.identity) as GameObject, true));
                        }
                        else
                        {
                            _map.Add(new Cell(Instantiate(ObstaclePrefab, position, Quaternion.identity) as GameObject, false));
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
                    int j = (int)cell.Position.x / CellSize;
                    int i = (int)cell.Position.z / CellSize;

                    var nodePos = node.GetPosition();
                    if ((int)nodePos.x == j && (int)nodePos.y == i)
                    {
                        cell.ChangeColor(Color.green);
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
                int j = (int)cell.Position.x / CellSize;
                int i = (int)cell.Position.z / CellSize;

                var nodePos = node.GetPosition();
                if ((int)nodePos.x == j && (int)nodePos.y == i)
                {
                    points.Add(cell.GameObject.transform);

                    if (drawPath)
                    {
                        cell.ChangeColor(Color.red);
                    }
                }
            }
        }

        return points;
    }

    private void CreateKeys()
    {
        for (int i=0; i < 3; i++)
        {
            var pos = new Vector3(UnityEngine.Random.Range(1, 8),
                0.56f, UnityEngine.Random.Range(1, 7));

            var checkPos = false;
            if(_keys.Count > 0)
                checkPos = true;

            while(checkPos)
            {
                pos = new Vector3(UnityEngine.Random.Range(1, 8),
                    0.56f, UnityEngine.Random.Range(1, 7));

                foreach (var key in _keys)
                {
                    if(key.transform.position == pos)
                    {
                        checkPos = true;
                    } else
                    {
                        checkPos = false;
                    }
                }
            }

            var go = Instantiate(KeyPrefab, pos, Quaternion.identity) as GameObject;
            go.name = "key";
            _keys.Add(go);
        }
    }

    void Update()
    {
        _character.Update(_followPath.GetSteering());

        _monster.Update(_monsterFollowPath.GetSteering());

        var posStartM = new Vector2(Mathf.Round(_monster.Transform.position.x), Mathf.Round(_monster.Transform.position.z));
        var posEndM = new Vector2(_character.Transform.position.x, _character.Transform.position.z);
        var nodeStartM = _graph.GetNodeByPosition(posStartM);
        var nodeEndM = _graph.GetNodeByPosition(posEndM);

        if (nodeStartM != null && nodeEndM != null)
        {

            var startNodeIdM = nodeStartM.GetId();
            var endNodeIdM = nodeEndM.GetId();

            var connectionsM = _graph.PathfindDijkstra(_graph._nodes[startNodeIdM], _graph._nodes[endNodeIdM]);
            if (connectionsM != null)
            {
                var points = GetPointsFromConnections(connectionsM, true);
                _monsterPath.AddNiewPoints(points);
                _monsterFollowPath.UpdateTarget();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                int counter = 0;
                foreach (var cell in _map)
                {
                    if (cell.GameObject == hitInfo.transform.gameObject)
                    {
                        var posStart = new Vector2(Mathf.Round(_character.Transform.position.x), Mathf.Round(_character.Transform.position.z));
                        var posEnd = new Vector2(cell.Position.x, cell.Position.z);
                        var nodeStart = _graph.GetNodeByPosition(posStart);
                        var nodeEnd = _graph.GetNodeByPosition(posEnd);

                        if (nodeStart != null && nodeEnd != null)
                        {

                            var startNodeId = nodeStart.GetId();
                            var endNodeId = nodeEnd.GetId();

                            var connections = _graph.PathfindDijkstra(_graph._nodes[startNodeId], _graph._nodes[endNodeId]);
                            if (connections != null)
                            {
                                var points = GetPointsFromConnections(connections, true);
                                _path.AddNiewPoints(points);
                                _followPath.UpdateTarget();
                            }

                            break;
                        }
                    }

                    counter++;
                }
            }
        }

        _time += Time.deltaTime;
        if (_time > 5)
        {
            _time = 0;

            CreateRandomMap(7, 8, 60);
            _graph.ParseFromMap(_map);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.ai.pathfinding
{
    public class Graph
    {
        public List<Node> _nodes;
        private Dictionary<int, List<Connection>> _connections;

        public Graph()
        {
            _nodes = new List<Node>();
            _connections = new Dictionary<int, List<Connection>>();
        }

        public void ParseFromMap(string path)
        {
            try
            {
                StreamReader streamReader = new StreamReader(path);
                using (streamReader)
                {
                    string line = streamReader.ReadLine();
                    line = streamReader.ReadLine();
                    var rows = int.Parse(line.Split(' ')[1]);
                    line = streamReader.ReadLine();
                    var cols = int.Parse(line.Split(' ')[1]);
                    line = streamReader.ReadLine();

                    _nodes = new List<Node>();
                    _connections = new Dictionary<int, List<Connection>>();

                    int id = 0;
                    for (int i = 0; i < rows; i++)
                    {
                        line = streamReader.ReadLine();
                        for (int j = 0; j < cols; j++)
                        {
                            if (line[j] == '.')
                            {
                                _nodes.Add(new Node(id, new Vector2(j, i)));
                                id++;
                            }
                        }
                    }

                    foreach (var node in _nodes)
                    {
                        var connections = CreateConnections(node, _nodes, 1.0f);
                        _connections.Add(node.GetId(), connections);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void CreateTest()
        {
            Node A = new Node(0, Vector2.zero);
            _nodes.Add(A);
            Node B = new Node(1, Vector2.zero);
            _nodes.Add(B);
            Node C = new Node(2, Vector2.zero);
            _nodes.Add(C);
            Node D = new Node(3, Vector2.zero);
            _nodes.Add(D);
            Node E = new Node(4, Vector2.zero);
            _nodes.Add(E);
            Node F = new Node(5, Vector2.zero);
            _nodes.Add(F);
            Node G = new Node(6, Vector2.zero);
            _nodes.Add(G);

            Connection connection1 = new Connection(A, B, 1.3f);
            Connection connection2 = new Connection(A, C, 1.6f);
            Connection connection3 = new Connection(A, D, 1.6f);
            _connections.Add(A.GetId(), new List<Connection> { connection1, connection2, connection3 });

            Connection connection4 = new Connection(B, E, 1.5f);
            Connection connection5 = new Connection(B, F, 1.9f);
            _connections.Add(B.GetId(), new List<Connection> { connection4, connection5 });

            Connection connection6 = new Connection(C, D, 1.3f);
            _connections.Add(C.GetId(), new List<Connection> { connection6 });

            Connection connection7 = new Connection(F, G, 1.4f);
            _connections.Add(F.GetId(), new List<Connection> { connection7 });
        }

        public Node GetNodeByPosition(Vector2 pos)
        {
            foreach (var node in _nodes)
            {
                var nodePos = node.GetPosition();
                if (nodePos.x == pos.x && nodePos.y == pos.y)
                {
                    return node;
                }
            }

            return null;
        }

        public List<Connection> GetConnections(Node fromNode)
        {
            if (_connections.ContainsKey(fromNode.GetId()))
            {
                return _connections[fromNode.GetId()];
            }
            else
            {
                return new List<Connection>();
            }
        }

        public List<Connection> PathfindDijkstra(Node start, Node end)
        {
            var startRecord = new NodeRecord();
            startRecord.Node = start;
            startRecord.Connection = null;
            startRecord.CostSoFar = 0;

            var open = new List<NodeRecord>();
            open.Add(startRecord);
            var closed = new List<NodeRecord>();
            NodeRecord current = null;

            while (open.Count > 0)
            {
                current = SmallestElement(open);
                if(current.Node == end)
                {
                    break;
                }

                var connections = GetConnections(current.Node);
                foreach(var connection in connections)
                {
                    var toNode = connection.GetToNode();
                    var toNodeCost = current.CostSoFar + connection.GetCost();
                    NodeRecord toNodeRecord = null;

                    if(NodeRecordsContains(closed, toNode))
                    {
                        continue;
                    }
                    else if(NodeRecordsContains(open, toNode))
                    {
                        toNodeRecord = FindByNode(open, toNode);
                        if (toNodeRecord.CostSoFar <= toNodeCost)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        toNodeRecord = new NodeRecord();
                        toNodeRecord.Node = toNode;
                    }

                    toNodeRecord.CostSoFar = toNodeCost;
                    toNodeRecord.Connection = connection;

                    if (!NodeRecordsContains(open, toNode))
                    {
                        open.Add(toNodeRecord);
                    }
                }

                open.Remove(current);
                closed.Add(current);
            }

            if (current.Node != end)
            {
                return null;
            }
            else
            {
                var path = new List<Connection>();
                while(current.Node != start)
                {
                    path.Add(current.Connection);
                    current = FindByNode(closed, current.Connection.GetFromNode());
                }

                path.Reverse();

                return path;
            }
        }

        private NodeRecord FindByNode(List<NodeRecord> records, Node node)
        {
            foreach (var record in records)
            {
                if (record.Node == node)
                {
                    return record;
                }
            }

            return null;
        }

        private bool NodeRecordsContains(List<NodeRecord> records, Node node)
        {
            foreach(var record in records)
            {
                if (record.Node == node)
                {
                    return true;
                }
            }

            return false;
        }

        private NodeRecord SmallestElement(List<NodeRecord> records)
        {
            if (records.Count > 0)
            {
                float smallestCost = records[0].CostSoFar;
                int smallestIndex = 0;

                for (int i = 1; i < records.Count; i++)
                {
                    if(records[i].CostSoFar < smallestCost)
                    {
                        smallestCost = records[i].CostSoFar;
                        smallestIndex = i;
                    }
                }

                return records[smallestIndex];
            }
            else
            {
                return null;
            }
        }

        private List<Connection> CreateConnections(Node node, List<Node> nodes, float cost)
        {
            var leftPosition = node.GetPosition() + new Vector2(-1, 0);
            var rightPosition = node.GetPosition() + new Vector2(1, 0);
            var topPosition = node.GetPosition() + new Vector2(0, -1);
            var bottomPosition = node.GetPosition() + new Vector2(0, 1);

            var connections = new List<Connection>();

            foreach (var otherNode in nodes)
            {
                var otherPosition = otherNode.GetPosition();

                if (otherPosition == leftPosition || otherPosition == rightPosition 
                    || otherPosition == topPosition || otherPosition == bottomPosition)
                {
                    var connection = new Connection(node, otherNode, cost);
                    connections.Add(connection);
                }
            }

            return connections;
        }
    }
}

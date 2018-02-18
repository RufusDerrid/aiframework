using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Code.ai.pathfinding
{
    public class Graph
    {
        public List<Node> _nodes;
        private List<List<Connection>> _connections;

        public Graph()
        {
            _nodes = new List<Node>();
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

                    _nodes = new List<Node>(rows * cols);
                    _connections = new List<List<Connection>>(rows * cols);

                    int id = 0;
                    for (int i = 0; i < rows; i++)
                    {
                        line = streamReader.ReadLine();
                        for (int j = 0; j < rows; j++)
                        {
                            if(line[j] == '.')
                            {
                                _nodes.Add(new Node(id, new Vector2(j, i)));
                                id++;
                            }
                        }
                    }

                    foreach (var node in _nodes)
                    {
                        var connections = CreateConnections(node, _nodes, 1.0f);
                        _connections.Add(connections);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public List<Connection> GetConnections(Node fromNode)
        {
            return _connections[fromNode.GetId()];
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
                    path.Reverse();

                    return path;
                }
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

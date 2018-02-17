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

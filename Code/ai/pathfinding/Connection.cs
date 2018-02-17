using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.ai.pathfinding
{
    public class Connection
    {
        private float _cost;
        private Node _from;
        private Node _to;

        public Connection(Node from, Node to, float cost)
        {
            _from = from;
            _to = to;
            _cost = cost;
        }

        public float GetCost()
        {
            return _cost;
        }

        public Node GetFromNode()
        {
            return _from;
        }

        public Node GetToNode()
        {
            return _to;
        }
    }
}

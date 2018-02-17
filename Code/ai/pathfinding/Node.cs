using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.ai.pathfinding
{
    public class Node
    {
        private int _id;
        private Vector2 _position;

        public Node(int id, Vector2 position)
        {
            _id = id;
            _position = position;
        }

        public int GetId()
        {
            return _id;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }
    }
}

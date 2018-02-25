//using System.Collections.Generic;
//using UnityEngine;

//namespace Assets.Code.world
//{
//    public class World
//    {
//        private List<Cell> _cells;

//        public World()
//        {
//            var cellGOs = GameObject.FindGameObjectsWithTag("Cell");
//            _cells = new List<Cell>();

//            foreach (var cellGO in cellGOs)
//            {
//                var walkable = true;

//                var walkableComp = cellGO.GetComponent<Walkable>();
//                if (walkableComp != null)
//                {
//                    walkable = walkableComp.Value;
//                }

//                var cell = new Cell(cellGO.transform, walkable);
//                _cells.Add(cell);
//            }
//        }

//        public Cell GetCellByPosition(Vector2 position)
//        {
//            foreach (var cell in _cells)
//            {
//                if (cell.Position == position)
//                {
//                    return cell;
//                }
//            }

//            return null;
//        }

//        public bool CanWalk(Vector2 position)
//        {
//            foreach (var cell in _cells)
//            {
//                if(cell.Position == position)
//                {
//                    return cell.IsWalkable;
//                }
//            }

//            return false;
//        }

//        public Cell GetNearestWalkableCell(Vector2 characterPosition, Vector2 position)
//        {
//            var walkableCells = new List<Cell>();

//            for(float i = characterPosition.x-1; i <= characterPosition.x+1; i++)
//            {
//                for (float j = characterPosition.y - 1; j <= characterPosition.y + 1; j++)
//                {
//                    var cellPosition = new Vector2(i, j);
//                    if (cellPosition != characterPosition)
//                    {
//                        var cell = GetCellByPosition(cellPosition);
//                        if(cell != null && cell.IsWalkable)
//                        {
//                            walkableCells.Add(cell);
//                        }
//                    }
//                }
//            }

//            if(walkableCells.Count > 0)
//            {
//                var resultCell = walkableCells[0];

//                for (int i = 1; i < walkableCells.Count; i++)
//                {
//                    var oldDistance = (position - resultCell.Position).sqrMagnitude;
//                    var newDistance = (position - walkableCells[i].Position).sqrMagnitude;

//                    if (newDistance < oldDistance)
//                    {
//                        resultCell = walkableCells[i];
//                    }
//                }

//                return resultCell;
//            } else
//            {
//                return null;
//            }
//        }
//    }
//}

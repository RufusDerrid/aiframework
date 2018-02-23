
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class CubesGenerator
    {
        private List<GameObject> _cubes;
        private GameObject _prefab;

        public CubesGenerator(GameObject prefab)
        {
            _cubes = new List<GameObject>();
            _prefab = prefab;
        }

        public List<GameObject> GenerateCubes(int rows, int cols, int frequency)
        {
            if(frequency > 100)
            {
                frequency = 100;
            }

            if(_cubes.Count > 0)
            {
                foreach(var cube in _cubes)
                {
                    GameObject.Destroy(cube);
                }

                _cubes.Clear();
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var t = Random.Range(0, 100);
                    if (t > frequency)
                        _cubes.Add(Object.Instantiate(_prefab, new Vector3(i, j, 0), Quaternion.identity));
                }
            }

            return _cubes;
        }
    }
}

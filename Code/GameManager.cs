using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Prefab;
    public bool On;

    private List<GameObject> _cubes;
    private float _time;
    public float s = 1.0f;

    void Start ()
    {
        _cubes = new List<GameObject>();

		for(int i=0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var t = Random.Range(0, 100);
                if(t > 30)
                    _cubes.Add(Instantiate(Prefab, new Vector3(i, j, 0), Quaternion.identity));
            }
        }
	}
	
	void Update ()
    {


        _time += Time.deltaTime;
        if(_time > 5)
        {
            _time = 0;

            if(_cubes.Count > 0)
            {
                var deasaper = _cubes[0].GetComponent<Deasaper>();
                deasaper.OnDelete += Deasaper_OnDelete;
            }

            foreach (var cube in _cubes)
            {
                var deasaper = cube.GetComponent<Deasaper>();
                deasaper.On = true;
            }
        }

        if(_cubes.Count == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var t = Random.Range(0, 100);
                    if (t > 60)
                        _cubes.Add(Instantiate(Prefab, new Vector3(i, j, 0), Quaternion.identity));
                }
            }
        }
	}

    private void Deasaper_OnDelete()
    {
        _cubes.Clear();
    }
}

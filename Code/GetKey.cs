using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        var pathFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PathFollowingScene>();
        if(pathFollow != null)
        {
            if (col.gameObject.name == "key")
            {

                pathFollow.GettingKey(col.gameObject);
                Destroy(col.gameObject);
            }

            if(col.gameObject.name == "door")
            {
                pathFollow.Victory();
            }
        }   
    }
}

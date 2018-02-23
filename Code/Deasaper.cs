using UnityEngine;

public class Deasaper : MonoBehaviour
{
    public float s = 1.0f;
    public bool On = false;

    public delegate void MethodContainer();
    public event MethodContainer OnDelete;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (On)
        {
            transform.localScale = new Vector3(s, s, s);
            s -= Time.deltaTime * 3;
            if (s < 0)
            {
                Destroy(gameObject);
                if(OnDelete != null)
                {
                    OnDelete();
                }
            }
        }
	}
}

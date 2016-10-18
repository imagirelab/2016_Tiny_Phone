using UnityEngine;
using System.Collections;

public class atackCol : MonoBehaviour
{
    public float destroyTime = 0.3f;
    public float punchSpeed = 2f;

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        GetComponent<Rigidbody2D>().velocity = Vector3.left * punchSpeed; 

        if(timer > destroyTime)
        {
            Destroy(this.gameObject);
        }
	}
}

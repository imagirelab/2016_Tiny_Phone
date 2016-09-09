using UnityEngine;
using System.Collections;

public class Rotaring : MonoBehaviour
{
    private float timer;
    public float vel = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (this.transform.position.x > 7 || this.transform.position.x < -7)
        {
            vel *= -1;
            this.transform.position += new Vector3(vel, 0, 0);
        }
        else
        {
            this.transform.position += new Vector3(vel, 0, 0);
        }
        this.transform.eulerAngles = new Vector3 (0, 0, timer * 500);
	}
}

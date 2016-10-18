using UnityEngine;
using System.Collections;

public class Rotaring : MonoBehaviour
{
    private float timer;
    public float vel = 0.3f;
    public float velY = 0.3f;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.right * vel);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        //if (this.transform.position.x > 7.5 || this.transform.position.x < -7.5)
        //{
        //    vel *= -1;
        //    this.transform.position += new Vector3(vel, velY, 0);
        //}
        //else if(this.transform.position.y > 4 || this.transform.position.y < -4)
        //{
        //    velY *= -1;
        //    this.transform.position += new Vector3(vel, velY, 0);
        //}
        //else
        //{
        //    this.transform.position += new Vector3(vel, velY, 0);
        //}
        //this.transform.eulerAngles = new Vector3 (0, 0, timer * 500);

        
	}
}

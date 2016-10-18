using UnityEngine;
using System.Collections;

public class PUPUPunch : MonoBehaviour
{
    public GameObject atackCollider;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("PunchFlag", true);
            Instantiate(atackCollider, transform.position, transform.rotation);
        }
        else if(Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("PunchFlag", false);
            
        }
    }

    public void Punch()
    {

    }
}

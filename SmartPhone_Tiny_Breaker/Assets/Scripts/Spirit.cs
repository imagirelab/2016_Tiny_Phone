using UnityEngine;
using System.Collections;

public class Spirit : SpiritManager
{
    public int id;

	// Use this for initialization
	void Start ()
    {
        id = spiritList.Count - 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (useSpiritFlag && id == 0)
        { 
            Destroy(this.gameObject);
        }
        else if(useSpiritFlag && id != 0)
        {
            --id;
            transform.position = new Vector3(spiritHolder.transform.position.x, spiritHolder.transform.position.y + spiritHolder.GetComponent<SpriteRenderer>().bounds.size.y / 3 - 1.5f * id, spiritHolder.transform.position.z);
            if(id == spiritList.Count - 1)
            {
                useSpiritFlag = false;
            }
        }
	}
}

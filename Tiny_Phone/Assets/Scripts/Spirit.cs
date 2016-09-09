using UnityEngine;
using System.Collections;

public class Spirit : SpiritManager
{
    public int id;

    // Use this for initialization
    void Start ()
    {
        //id = spiritList.Count - 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckSpirit();
	}

    public void CheckSpirit()
    {
        if (useSpiritFlag)
        {
            if (id == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                --id;
                //transform.position = new Vector3(SummonPos.transform.position.x, SummonPos.transform.position.y, 0);

                if (id == spiritList.Count - 1)
                {
                    useSpiritFlag = false;
                }
            }          
        }
    }
}

using UnityEngine;
using System.Collections;

public class Spirit : SpiritManager
{
    public int id;
    public bool usedFlag;

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
        //if (useSpiritFlag)
        //{
        //    if (id == 0)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //    else
        //    {
        //        --id;
        //        //transform.position = new Vector3(SummonPos.transform.position.x, SummonPos.transform.position.y, 0);

        //        if (id == spiritList.Count - 1)
        //        {
        //            useSpiritFlag = false;
        //        }
        //    }          
        //}

        if(SpiritManager.spiritList[0].GetComponent<Spirit>().id == this.id && SpiritManager.spiritList[0].GetComponent<Spirit>().usedFlag)
        {
            SpiritManager.spiritList.RemoveAt(0);
            Destroy(this.gameObject);
        }
    }
}

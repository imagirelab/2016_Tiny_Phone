using UnityEngine;
using System.Collections;

public class Spirit : SpiritManager
{
    public int id;
    public bool usedFlag;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckSpirit();
	}

    public void CheckSpirit()
    {
        if(SpiritManager.spiritList[0].GetComponent<Spirit>().id == this.id && SpiritManager.spiritList[0].GetComponent<Spirit>().usedFlag)
        {
            SpiritManager.spiritList.RemoveAt(0);
            Destroy(this.gameObject);
        }
    }
}

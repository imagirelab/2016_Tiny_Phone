using UnityEngine;
using System.Collections;

public class Spirit : SpiritManager
{
    public int id;
    //魂の仮ステータス
    public GrowPoint growPoint;

    // Use this for initialization
    void Start ()
    {
        id = spiritList.Count - 1;
        Debug.Log(id);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeSpirit();
	}

    public void ChangeSpirit()
    {
        if (useSpiritFlag)
        {
            if (id == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                id -= 1;
                transform.position = new Vector3(spiritHolder.transform.position.x, spiritHolder.transform.position.y + spiritHolder.GetComponent<SpriteRenderer>().bounds.size.y / 3 - 1.5f * id, spiritHolder.transform.position.z);

                if (id == spiritList.Count - 1)
                {
                    useSpiritFlag = false;
                }
            }

            
        }
    }
}

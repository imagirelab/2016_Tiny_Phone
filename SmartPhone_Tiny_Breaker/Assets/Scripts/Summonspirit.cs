using UnityEngine;
using System.Collections;

public class Summonspirit : SpiritManager
{
    [HideInInspector]
    public bool buttonDownFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)
    [HideInInspector]
    public bool runFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(runFlag)
        {
            if (spiritList.Count < SpiritLimit)
            {
                spiritList.Add(spiritData);
                spiritList[spiritList.Count - 1].transform.position = new Vector3(spiritHolder.transform.position.x, spiritHolder.transform.position.y + spiritHolder.GetComponent<SpriteRenderer>().bounds.size.y / 3 - 1.5f * (spiritList.Count - 1), spiritHolder.transform.position.z);
                Instantiate(spiritList[spiritList.Count - 1]);
            }

            runFlag = false;
        }
	}
}

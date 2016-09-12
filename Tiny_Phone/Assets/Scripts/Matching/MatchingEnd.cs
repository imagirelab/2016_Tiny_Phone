using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class MatchingEnd : MonoBehaviour
{
    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 300;

    private int flameCount = 0;
    private bool allDelete = false;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        flameCount++;

        if (flameCount > ReceiveCountflame && !allDelete)
        {
            Recieve();
            allDelete = true;
            flameCount = 0;
        }
    }

    void Recieve()
    {
        //クエリを作成
        NCMBQuery<NCMBObject> playerQuery = new NCMBQuery<NCMBObject>("PlayerData");

        //検索
        playerQuery.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索失敗時
            if (e != null)
            {
                Debug.Log(e.ToString());
                return;
            }
            else
            {
                //リストにある数だけ回す
                foreach (NCMBObject ncmbObj in objList)
                {
                    if (ncmbObj["PlayerNo"].ToString() == "0")
                    {
                    }
                    else
                    {
                        ncmbObj.DeleteAsync();
                    }
                }
            }
        });
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class Matching : MonoBehaviour
{
    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 60;

    private int flameCount = 0;

    private int latestNo;

    private bool Player1okFlag;
    private bool Player2okFlag;

    private NCMBObject matchingObj = new NCMBObject("PlayerData");


    // Use this for initialization
    void Start ()
    {
        flameCount = 0;
        latestNo = 0;
        Player1okFlag = false;
        Player2okFlag = false;

        Sort();

        matchingObj["PlayerNo"] = (latestNo + 1).ToString();
        matchingObj["Mode"] = "matching";

        matchingObj.SaveAsync();
    }
	
	// Update is called once per frame
	void Update ()
    {
        flameCount++;

        if(flameCount > ReceiveCountflame)
        {
            Recieve();
        }

        if(Player1okFlag && Player2okFlag)
        {
            Application.LoadLevel(1);
        }
    }

    void Recieve()
    {
        //クエリを作成
        NCMBQuery<NCMBObject> playerQuery = new NCMBQuery<NCMBObject>("PlayerData");

        playerQuery.WhereNotEqualTo("Mode", 10);

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
                    if(ncmbObj["PlayerNo"].ToString() == "1" && ncmbObj["Mode"].ToString() == "matching")
                    {
                        Player1okFlag = true;
                        ncmbObj.DeleteAsync();
                    }
                    else if(ncmbObj["PlayerNo"].ToString() == "2" && ncmbObj["Mode"].ToString() == "matching")
                    {
                        Player2okFlag = true;
                        ncmbObj.DeleteAsync();
                    }
                    else
                    {
                        ncmbObj.DeleteAsync();
                    }
                }                
            }
        });
    }

    void Sort()
    {
        //クエリを作成
        NCMBQuery<NCMBObject> playerQuery = new NCMBQuery<NCMBObject>("PlayerData");

        playerQuery.OrderByDescending("PlayerNo");
        playerQuery.Limit = 1;

        //検索
        playerQuery.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索失敗時
            if (e != null)
            {
                latestNo = 0;
            }
            else
            {
                //リストにある数だけ回す
                foreach (NCMBObject ncmbObj in objList)
                {
                    latestNo = System.Convert.ToInt32(ncmbObj["PlayerNo"].ToString());
                }
            }
        });
    }
}

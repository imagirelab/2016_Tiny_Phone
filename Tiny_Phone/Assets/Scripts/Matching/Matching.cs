using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class Matching : MonoBehaviour
{
    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 300;

    private int flameCount = 0;

    private int latestNo;

    private bool Player1okFlag;
    private bool Player2okFlag;
    private bool sortokFlag;

    private NCMBObject matchingObj;

    // Use this for initialization
    void Start ()
    {
        flameCount = 0;
        latestNo = 0;
        Player1okFlag = false;
        Player2okFlag = false;
        sortokFlag = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        flameCount++;

        if(sortokFlag)
        {
            matchingObj = new NCMBObject("PlayerData");

            matchingObj["PlayerNo"] = (latestNo).ToString();
            matchingObj["Mode"] = "matching";

            matchingObj.SaveAsync();
            sortokFlag = false;
        }

        if(flameCount > ReceiveCountflame)
        {
            if (latestNo == 0)
            {
                Debug.Log("SortCall");
                Sort();
            }
            else
            {
                Debug.Log("RecieveCall");
                Recieve();
            }
            flameCount = 0;
        }

        if(Player1okFlag && Player2okFlag && latestNo <= 2)
        {
            Application.LoadLevel("main");
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
                    if(ncmbObj["PlayerNo"].ToString() == "1" && (ncmbObj["Mode"].ToString() == "matching" || ncmbObj["Mode"].ToString() == "PCOK"))
                    {
                        Player1okFlag = true;
                    }
                    else if(ncmbObj["PlayerNo"].ToString() == "2" && (ncmbObj["Mode"].ToString() == "matching" || ncmbObj["Mode"].ToString() == "PCOK"))
                    {
                        Player2okFlag = true;
                    }
                    else if(ncmbObj["PlayerNo"].ToString() == "0")
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
                Debug.Log(e.ToString());
                return;
            }
            else
            {
                if (objList.Count > 0)
                {
                    //リストにある数だけ回す
                    foreach (NCMBObject ncmbObj in objList)
                    {
                        latestNo = System.Convert.ToInt32(ncmbObj["PlayerNo"].ToString());
                        latestNo += 1;
                        sortokFlag = true;
                    }
                }
                else
                {
                    latestNo = 1;
                    sortokFlag = true;
                }
            }
        });
    }

    void OnDisable()
    {
        if(matchingObj != null)
            matchingObj.DeleteAsync();
    }

    private void OnApplicationQuit()
    {
        if (matchingObj != null)
            matchingObj.DeleteAsync();
    }
}

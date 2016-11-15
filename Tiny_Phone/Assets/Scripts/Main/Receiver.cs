using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class Receiver : MonoBehaviour
{

    //フレーム数のカウント数える用
    [HideInInspector]
    protected static int flamecounter;

    [HideInInspector]
    protected static bool spiritOKFlag;
    protected static bool costOKFlag;

    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 60;

    // Use this for initialization
    void Start ()
    {
        flamecounter = 0;
        spiritOKFlag = false;
        costOKFlag = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        flamecounter++;

        if(spiritOKFlag && costOKFlag)
        {
            StaticVariables.GetState = false;
            flamecounter = 0;
            spiritOKFlag = false;
        }
    }

    public void SpiritReceive(List<GameObject> spiritList, GameObject POPOspirit, GameObject PUPUspirit, GameObject PIPIspirit, int SpiritLimit, int summonCounter, int SummonLag)
    {
        //クエリを作成
        NCMBQuery<NCMBObject> demonQuery = new NCMBQuery<NCMBObject>("SpiritData");

        demonQuery.WhereContainedIn("PlayerNo", (StaticVariables.PlayerNo).ToString());

        //検索
        demonQuery.FindAsync((List<NCMBObject> objList, NCMBException e) =>
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
                    //21体以上同時受信した時に弾くために
                    if (spiritList.Count < SpiritLimit)
                    {
                        //召喚をほぼ同時に行わせないようにタイムラグを発生させる
                        if (summonCounter > SummonLag)
                        {
                            switch (ncmbObj["TYPE"].ToString())
                            {
                                case "POPO":
                                    GameObject POPOobj = Instantiate(POPOspirit);
                                    POPOobj.transform.position = new Vector3(this.transform.position.x + Random.Range(-0.1f, 0.1f), this.transform.position.y, this.transform.position.z);
                                    POPOobj.GetComponent<Spirit>().id = spiritList.Count;
                                    SummonSpirit(POPOobj, spiritList, summonCounter);
                                    break;
                                case "PUPU":
                                    GameObject PUPUobj = Instantiate(PUPUspirit);
                                    PUPUobj.transform.position = new Vector3(this.transform.position.x + Random.Range(-0.1f, 0.1f), this.transform.position.y, this.transform.position.z);
                                    PUPUobj.GetComponent<Spirit>().id = spiritList.Count;
                                    SummonSpirit(PUPUobj, spiritList, summonCounter);
                                    break;
                                case "PIPI":
                                    GameObject PIPIobj = Instantiate(PIPIspirit);
                                    PIPIobj.transform.position = new Vector3(this.transform.position.x + Random.Range(-0.1f, 0.1f), this.transform.position.y, this.transform.position.z);
                                    PIPIobj.GetComponent<Spirit>().id = spiritList.Count;
                                    SummonSpirit(PIPIobj, spiritList, summonCounter);
                                    break;
                                default:
                                    Debug.Log("Player.cs Receive() ncmbObj[Type] Exception");
                                    break;
                            }

                            //記録を取ったら消す
                            ncmbObj.DeleteAsync();
                        }
                    }
                }
            }
        });

        spiritOKFlag = true;
    }

    void SummonSpirit(GameObject _spirit, List<GameObject> spiritList, int summonCounter)
    {
        spiritList.Add(_spirit);
        spiritList[spiritList.Count - 1].GetComponent<Spirit>().usedFlag = false;
        summonCounter = 0;
        for (int i = 0; i < spiritList.Count; i++)
        {
            Debug.Log("<color=green></color>" + i + "\n<color=green>ID</color>" + spiritList[i].GetComponent<Spirit>().id);
            Debug.Log("<color=red>i =</color>" + i + "\n<color=red>Flag</color>" + spiritList[i].GetComponent<Spirit>().usedFlag);
        }
    }

    public int CostReceive(int currentCost, int MaxCost)
    {
        //クエリを作成
        NCMBQuery<NCMBObject> costQuery = new NCMBQuery<NCMBObject>("CostData");

        costQuery.WhereContainedIn("PlayerNo", (StaticVariables.PlayerNo).ToString());

        //検索
        costQuery.FindAsync((List<NCMBObject> objList, NCMBException e) =>
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
                    int addCostData = System.Convert.ToInt32(ncmbObj["Cost"].ToString());

                    currentCost = AddCost(currentCost, addCostData, MaxCost);

                    Debug.Log("\n<color=green>Cost = </color>" + currentCost);

                    //記録を取ったら消す
                    ncmbObj.DeleteAsync();
                }
            }
        });

        costOKFlag = true;

        return currentCost;
    }

    //コストを足す
    //上限を超えたときは上限値を代入する
    int AddCost(int currentCost, int addcost, int MaxCost)
    {
        if (currentCost + addcost <= MaxCost)
            currentCost += addcost;
        else
            currentCost = MaxCost;

        return currentCost;
    }
}

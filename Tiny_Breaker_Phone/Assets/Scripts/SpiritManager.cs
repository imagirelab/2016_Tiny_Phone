using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class SpiritManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject demon;

    public GameObject POPOspirit;
    public GameObject PUPUspirit;
    public GameObject PIPIspirit;

    public GameObject spiritData;

    public GameObject spiritHolder;

    public int SpiritLimit = 5;

    public static List<GameObject> spiritList = new List<GameObject>();

    public static bool useSpiritFlag = false;

    GrowPoint spiritGrowPoint;

    //成長値の保存変数
    public GrowPoint growPoint;

    //フレーム数のカウント数える用
    private int counter;

    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 60;

    // Use this for initialization
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;

        if (counter > ReceiveCountflame)
        {
            counter = 0;

            //受信処理
            Receive();
        }

        if(spiritList.Count == 0)
        {
            useSpiritFlag = false;
        }

    }

    public void useSpirit()
    {
        //仮に何も設定されていなかったら空のゲームオブジェクトを入れる
        if (demon == null)
        {
            demon = new GameObject();
        }
        else
        {
            //悪魔の成長値を記憶する
            growPoint = demon.GetComponent<Demons>().GrowPoint;
        }

        if (spiritList.Count > 0)
        {
            spiritGrowPoint = spiritList[0].GetComponent<Spirit>().growPoint;

            //成長値の足し方
            growPoint.CurrentHP_GrowPoint += growPoint.GetHP_GrowPoint + spiritGrowPoint.GetHP_GrowPoint;
            growPoint.CurrentATK_GrowPoint += growPoint.GetATK_GrowPoint + spiritGrowPoint.GetATK_GrowPoint;
            growPoint.CurrentSPEED_GrowPoint += growPoint.GetSPEED_GrowPoint + spiritGrowPoint.GetSPEED_GrowPoint;
            growPoint.CurrentAtackTime_GrowPoint += growPoint.GetAtackTime_GrowPoint + spiritGrowPoint.GetAtackTime_GrowPoint;

            demon.GetComponent<Demons>().GrowPoint = growPoint;

            demon.GetComponent<Demons>().powerUp();

            useSpiritFlag = true;

            spiritList.RemoveAt(0);
        }
    }

    void Receive()
    {
        //クエリを作成
        NCMBQuery<NCMBObject> demonQuery = new NCMBQuery<NCMBObject>("SpiritData");

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
                    switch(ncmbObj["TYPE"].ToString())
                    {
                        case "POPO":
                            SummonSpirit(POPOspirit);
                            break;
                        case "PUPU":
                            SummonSpirit(PUPUspirit);
                            break;
                        case "PIPI":
                            SummonSpirit(PIPIspirit);
                            break;
                        default:
                            Debug.Log("Player.cs Receive() ncmbObj[Type] Exception");
                            break;
                    }

                    //記録を取ったら消す
                    ncmbObj.DeleteAsync();
                }
            }
        });
    }

    void SummonSpirit(GameObject spirit)
    {
        spiritList.Add(spirit);
        spiritList[spiritList.Count - 1].transform.position = new Vector3(spiritHolder.transform.position.x, spiritHolder.transform.position.y + spiritHolder.GetComponent<SpriteRenderer>().bounds.size.y / 3 - 1.5f * (spiritList.Count - 1), spiritHolder.transform.position.z);
        Instantiate(spiritList[spiritList.Count - 1]);
    }
}

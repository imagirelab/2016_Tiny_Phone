using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class SpiritManager : MonoBehaviour
{
    public GameObject POPOspirit;
    public GameObject PUPUspirit;
    public GameObject PIPIspirit;

    public GameObject spiritHolder;

    public int SpiritLimit = 5;

    public static List<GameObject> spiritList = new List<GameObject>();

    public static bool useSpiritFlag = false;

    public float spiritSpace = 1.5f;

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
        //６体以上同時受信した時に弾くために
        if (spiritList.Count < 5)
        {
            spiritList.Add(spirit);
            spirit.GetComponent<Spirit>().id = spiritList.Count - 1;
            spiritList[spiritList.Count - 1].transform.position = new Vector3(spiritHolder.transform.position.x, spiritHolder.transform.position.y + spiritHolder.GetComponent<SpriteRenderer>().bounds.size.y / 3 - spiritSpace * spirit.GetComponent<Spirit>().id, spiritHolder.transform.position.z);
            Instantiate(spiritList[spiritList.Count - 1]);
        }
    }
}

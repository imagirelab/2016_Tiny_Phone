using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;

public class SpiritManager : MonoBehaviour
{
    public GameObject POPOspirit;
    public GameObject PUPUspirit;
    public GameObject PIPIspirit;

    public GameObject SummonPos;

    //魂の上限設定値
    public int SpiritLimit = 5;

    //魂の作成時間(同時に作れると判定が重なるため作成)
    [SerializeField, TooltipAttribute("フレーム計算で")]
    public int SummonLag = 30;

    //魂をリスト管理するために作成
    public static List<GameObject> spiritList = new List<GameObject>();

    //魂を使ったかのフラグ判定(他のソースでも使う)
    public static bool useSpiritFlag = false;

    public float spiritSpace = 1.5f;

    //フレーム数のカウント数える用
    private int flamecounter;

    //召喚のカウントを数える用
    private int summonCounter;

    [TooltipAttribute("受信フレームカウント")]
    public int ReceiveCountflame = 60;

    // Use this for initialization
    void Start()
    {
        flamecounter = 0;
        summonCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        flamecounter++;
        summonCounter++;

        if (flamecounter > ReceiveCountflame)
        {
            flamecounter = 0;

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

                    if (ncmbObj["PlayerNo"].ToString() == (StaticVariables.PlayerNo - 1).ToString())
                    {
                        //召喚をほぼ同時に行わせないようにタイムラグを発生させる
                        if (summonCounter > SummonLag)
                        {
                            switch (ncmbObj["TYPE"].ToString())
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
                }
            }
        });
    }

    void SummonSpirit(GameObject spirit)
    {
        //21体以上同時受信した時に弾くために
        if (spiritList.Count < SpiritLimit)
        {
                spiritList.Add(spirit);
                spirit.GetComponent<Spirit>().id = spiritList.Count - 1;
                spiritList[spiritList.Count - 1].transform.position = new Vector3(SummonPos.transform.position.x + Random.Range(-0.5f , 0.5f), SummonPos.transform.position.y, 0);
                Instantiate(spiritList[spiritList.Count - 1]);
                summonCounter = 0;
        }
    }
}

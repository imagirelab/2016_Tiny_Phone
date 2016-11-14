using UnityEngine;
using System.Collections.Generic;

public class SpiritManager : Receiver
{
    public GameObject POPOspirit;
    public GameObject PUPUspirit;
    public GameObject PIPIspirit;

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

    //召喚のカウントを数える用
    private int summonCounter;

    // Use this for initialization
    void Start()
    {
        summonCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        summonCounter++;

        if (flamecounter > ReceiveCountflame && StaticVariables.GetState)
        {
            //受信処理
            SpiritReceive(spiritList, POPOspirit, PUPUspirit, PIPIspirit, SpiritLimit, summonCounter, SummonLag);
        }

        if(spiritList.Count == 0)
        {
            useSpiritFlag = false;
        }

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiritManager : MonoBehaviour
{
    public GameObject demon;

    public GameObject spiritData;

    public GameObject spiritHolder;

    public int SpiritLimit = 5;

    public static List<GameObject> spiritList = new List<GameObject>();

    public static bool useSpiritFlag = false;

    //成長値の保存変数
    GrowPoint growPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void useSpirit()
    {
        //仮に何も設定されていなかったら空のゲームオブジェクトを入れる
        if (demon == null)
            demon = new GameObject();
        else
        {
            //悪魔の成長値を記憶する
            growPoint = demon.GetComponent<Demons>().GrowPoint;
            growPoint.SetGrowPoint();
        }

        if (spiritList.Count > 0)
        {
            GrowPoint spiritGrowPoint = spiritList[0].GetComponent<Spirit>().growPoint;

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
        else
        {
            useSpiritFlag = false;
        }
    }
}

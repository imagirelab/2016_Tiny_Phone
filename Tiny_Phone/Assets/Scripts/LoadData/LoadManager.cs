using UnityEngine;
using System.Collections;
using System.IO;
using Loader;
using StaticClass;

public class LoadManager : MonoBehaviour
{
    [SerializeField]
    bool IsLoad = true;

    private static readonly string paramurl = "https://yoo3006.github.io/ParamData.csv";
    private static readonly string costurl = "https://yoo3006.github.io/CostData.csv";

    [SerializeField]
    GameObject prePOPO;
    [SerializeField]
    GameObject prePUPU;
    [SerializeField]
    GameObject prePIPI;

    //void Start()
    public IEnumerator Start()
    {
        //ロードしない設定なら飛ばす
        if (IsLoad)
        {
            //ゲームオブジェクトの設定しわすれがあった時、
            //メッセージを名前にして空のオブジェクトを作る
            if (prePOPO == null)
                prePOPO = new GameObject(this.ToString() + " prePOPO Null");
            if (prePUPU == null)
                prePUPU = new GameObject(this.ToString() + " prePUPU Null");
            if (prePIPI == null)
                prePIPI = new GameObject(this.ToString() + " prePIPI Null");

            //gh-pageから文字列を取得
            WWW paramwww = new WWW(paramurl);
            WWW costwww = new WWW(costurl);

            yield return paramwww;
            yield return costwww;

            string paramtext = paramwww.text;
            string costtext = costwww.text;

            ////プロジェクト内のファイルを取得
            //string paramtext = GetCSVString("/Resources/CSVData/ParamData.csv");
            //string growtext = GetCSVString("/Resources/CSVData/GrowData.csv");
            //string costtext = GetCSVString("/Resources/CSVData/CostData.csv");

            ParamData ParamTable = new ParamData();
            ParamTable.Load(paramtext);

            CostData CostTable = new CostData();
            CostTable.Load(costtext);

            //パラメータデータの取り込み
            foreach (var param in ParamTable.All)
            {
                switch (param.ID)
                {
                    case "popo":
                        if (prePOPO != null)
                            SetParm(param, prePOPO);
                        break;
                    case "pupu":
                        if (prePUPU != null)
                            SetParm(param, prePUPU);
                        break;
                    case "pipi":
                        if (prePIPI != null)
                            SetParm(param, prePIPI);
                        break;
                    default:
                        break;
                }
            }

            //コストデータの取り込み
            SetCost(CostTable);

            Debug.Log("Load END");
        }
    }

    /// <summary>
    ///　CSVファイルの文字列を取得
    /// </summary>
    /// <param name="path">Assetフォルダ以下のCSVファイルの位置を書く</param>
    /// <returns>CSVファイルの文字列</returns>
    string GetCSVString(string path)
    {
        StreamReader sr = new StreamReader(Application.dataPath + path);
        string strStream = sr.ReadToEnd();

        return strStream;
    }

    void SetParm(ParamMaster param, GameObject unit)
    {
        if (unit.GetComponent<Unit>())
        {
            unit.GetComponent<Unit>().status.SetDefault(param.HP, param.ATK, param.SPEED, param.ATKSPEED);
            unit.GetComponent<Unit>().ATKRange = param.ATKRENGE;
        }
    }

    void SetCost(CostData CostTable)
    {
        foreach (var cost in CostTable.All)
        {
            for(int i = 0; i < GameRule.playerNum; i++)
            {
                GameObject player = GameObject.Find("Player");
                if (player.GetComponent<PlayerCost>())
                    player.GetComponent<PlayerCost>().SetDefault(cost.MaxCost, cost.StateCost, cost.CostParSecond, cost.DemonCost, cost.DemonCostRate, cost.SoldierCost, cost.HouseCost);
            }
        }
    }
}

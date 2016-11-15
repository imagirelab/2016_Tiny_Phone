using UnityEngine;
using System.Collections.Generic;
using NCMB;

public class PlayerCost : Receiver {

    [SerializeField, TooltipAttribute("最大コスト")]
    int MaxCost = 0;
    public int GetMaxCost { get { return MaxCost; } }

    [SerializeField, TooltipAttribute("初期コスト")]
    int StateCost = 0;

    [SerializeField, TooltipAttribute("毎秒上がるコスト")]
    int CostParSecond = 0;

    [SerializeField, TooltipAttribute("悪魔召喚の初期コスト")]
    int DemonCost = 0;
    public int GetDemonCost { get { return DemonCost; } }

    [SerializeField, TooltipAttribute("悪魔召喚コストの増加率")]
    float DemonCostRate = 0.5f;
    public float GetDemonCostRate { get { return DemonCostRate; } }

    [SerializeField, TooltipAttribute("兵士の撃破獲得コスト")]
    int SoldierCost = 0;
    public int GetSoldierCost { get { return SoldierCost; } }

    [SerializeField, TooltipAttribute("小屋の撃破獲得コスト")]
    int HouseCost = 0;
    public int GetHouseCost { get { return HouseCost; } }

    //現在のコスト
    int currentCost = 0;
    public int CurrentCost { get { return currentCost; } }

    float time = 0;     //時間

    TextMesh _text;

    void Start ()
    {
        currentCost = StateCost;
        _text = this.GetComponent<TextMesh>();
        _text.text = "COST: " + currentCost + "/" + MaxCost;
    }
	
	void Update ()
    {
        //毎秒増えるコスト
        if (time >= 1.0f)
        {
            time = 0;

            if (currentCost + CostParSecond <= MaxCost)
                currentCost += CostParSecond;
        }

        //1フレームあたりの時間を取得
        time += Time.deltaTime;

        if (flamecounter > ReceiveCountflame && StaticVariables.GetState)
        {
            currentCost = CostReceive(currentCost, MaxCost);
            Debug.Log("\n<color=yellow>Cost = </color>" + currentCost);
        }

        _text.text = "COST: " + currentCost + "/" + MaxCost;
    }

    //コストが使えるかどうか
    //使える場合は数値を引いたのち引けたことを返す
    public bool UseableCost(int cost)
    {

        if (currentCost - (cost + DemonCost) >= 0)
        {
            currentCost -= (cost + DemonCost);
            
            return true;
        }
        else
            return false;
    }

    public void SetDefault(int max, int state, int costparsecond, int demon, float demonRate, int soldier, int house)
    {
        MaxCost = max;
        StateCost = state;
        CostParSecond = costparsecond;
        DemonCost = demon;
        DemonCostRate = demonRate;
        SoldierCost = soldier;
        HouseCost = house;

        Start();
    }
}

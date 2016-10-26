using UnityEngine;

//悪魔達の初期情報(structの代わりにclassを使用)
[System.Serializable]
public class Status
{
    [SerializeField, TooltipAttribute("体力")]
    private float HP = 0;
    [SerializeField, TooltipAttribute("最大体力")]
    private float maxHP = 0;
    [SerializeField, TooltipAttribute("攻撃力")]
    private float ATK = 0;
    [SerializeField, TooltipAttribute("最大攻撃力")]
    private float maxATK = 0;
    [SerializeField, TooltipAttribute("速度")]
    private float SPEED = 0;
    [SerializeField, TooltipAttribute("最大速度")]
    private float maxSPEED = 0;
    [SerializeField, TooltipAttribute("攻撃間隔")]
    private float AtackTime = 0;
    [SerializeField, TooltipAttribute("最大攻撃間隔")]
    private float maxAtackTime = 0;
    [SerializeField, TooltipAttribute("コスト")]
    private int DemonCost = 0;
    [SerializeField, TooltipAttribute("最大コスト")]
    private int maxDemonCost = 0;

    public float GetHP { get { return HP; } }
    public float GetATK { get { return ATK; } }
    public float GetSPEED { get { return SPEED; } }
    public float GetAtackTime { get { return AtackTime; } }
    public int GetDemonCost { get { return DemonCost; } }

    public float maxGetHP { get { return maxHP; } }
    public float maxGetATK { get { return maxATK; } }
    public float maxGetSPEED { get { return maxSPEED; } }
    public float maxGetAtackTime { get { return maxAtackTime; } }
    public int maxGetDemonCost { get { return maxDemonCost; } }

    //プレハブのすべて共有の値になってしまうため
    //元々のステータスはいじらないようにするため
    //別の変数を用意
    private float currentHP;
    private float currentATK;
    private float currentSPEED;
    private float currentAtackTime;
    private int currentCost;

    private float maxcurrentHP;
    private float maxcurrentATK;
    private float maxcurrentSPEED;
    private float maxcurrentAtackTime;
    private int maxcurrentCost;

    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
    public float CurrentATK
    {
        get { return currentATK; }
        set { currentATK = value; }
    }
    public float CurrentSPEED
    {
        get { return currentSPEED; }
        set { currentSPEED = value; }
    }
    public float CurrentAtackTime
    {
        get { return currentAtackTime; }
        set { currentAtackTime = value; }
    }
    public int CurrentCost
    {
        get { return currentCost; }
        set { currentCost = value; }
    }



    public float maxCurrentHP
    {
        get { return maxcurrentHP; }
        set { maxcurrentHP = value; }
    }
    public float maxCurrentATK
    {
        get { return maxcurrentATK; }
        set { maxcurrentATK = value; }
    }
    public float maxCurrentSPEED
    {
        get { return maxcurrentSPEED; }
        set { maxcurrentSPEED = value; }
    }
    public float maxCurrentAtackTime
    {
        get { return maxcurrentAtackTime; }
        set { maxcurrentAtackTime = value; }
    }
    public int maxCurrentCost
    {
        get { return maxcurrentCost; }
        set { maxcurrentCost = value; }
    }

    Status()
    {
        SetStatus();
    }

    //現在のステータスに代入する
    public void SetStatus()
    {
        currentHP = HP;
        currentATK = ATK;
        currentSPEED = SPEED;
        currentAtackTime = AtackTime;
        currentCost = DemonCost;

        maxcurrentHP = maxHP;
        maxcurrentATK = maxATK;
        maxcurrentSPEED = maxSPEED;
        maxcurrentAtackTime = maxAtackTime;
        maxcurrentCost = maxDemonCost;
    }

    //基準を変えたいときに呼び出す
    public void SetDefault(int hp, int atk, float speed, float atkspeed)
    {
        HP = hp;
        ATK = atk;
        SPEED = speed;
        AtackTime = atkspeed;

        SetStatus();
    }
}
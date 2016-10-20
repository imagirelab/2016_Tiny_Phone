// 悪魔単体の処理をするクラス

using UnityEngine;

public class Demons : Unit
{
    //成長値
    bool changeGrowPoint = false;   //外部から変更があったかどうかのフラグ
    [SerializeField, TooltipAttribute("悪魔の成長値ポイント")]
    private GrowPoint growPoint;
    public GrowPoint GrowPoint
    {
        get { return growPoint; }

        set
        {
            growPoint = value;
            changeGrowPoint = true;
        }
    }

    //悪魔がなる魂オブジェクト
    public GameObject spirit;

    //悪魔に作られた画面のプレイヤーのIDを入れておく変数
    private int playerID;
    public int PlayerID { set { playerID = value; } }
    
    //private float time;                 //時間

    // 触れているものの情報
    // 接触しているゲームオブジェクト
    private GameObject hitObject;

    void Start ()
    {
        //初期ステータス(プロパティからの設定情報)
        status.SetStutas();
        //外部からの変更がなかった場合初期の成長値を設定する
        if (!changeGrowPoint)
            growPoint.SetGrowPoint();

        for (int i = 0; i < growPoint.CurrentHP_GrowPoint - growPoint.GetHP_GrowPoint; i++)
            status.CurrentHP += (int)(status.GetHP * 0.5f);
        for (int i = 0; i < growPoint.CurrentATK_GrowPoint - growPoint.GetATK_GrowPoint; i++)
            status.CurrentATK += (int)(status.GetATK * 0.5f);
        for (int i = 0; i < growPoint.CurrentSPEED_GrowPoint - growPoint.CurrentSPEED_GrowPoint; i++)
            status.CurrentSPEED += (int)(status.GetSPEED * 0.15f);
        //for (int i = 0; i < growPoint.CurrentAtackTime_GrowPoint - growPoint.CurrentAtackTime_GrowPoint; i++)
        //    status.CurrentAtackTime += (int)(status.GetAtackTime * 0.15f);
    }

    //破壊されたときにリストから外す
    void OnDisable()
    {
         
    }

    void Update ()
    {
        
    }

    public void powerUp()
    {
        //外部からの変更がなかった場合初期の成長値を設定する
        if (!changeGrowPoint)
        {
            growPoint.SetGrowPoint();
        }

        status.CurrentHP += (int)(status.GetHP * 0.1f);       
        status.CurrentATK += (int)(status.GetATK * 0.1f);       
        //status.CurrentSPEED += (status.GetSPEED * 0.1f);

        if(status.CurrentHP > status.maxCurrentHP)
        {
            status.CurrentHP = status.maxCurrentHP;
        }
        if (status.CurrentATK > status.maxCurrentATK)
        {
            status.CurrentATK = status.maxCurrentATK;
        }
        if (status.CurrentSPEED > status.maxCurrentSPEED)
        {
            status.CurrentSPEED = status.maxCurrentSPEED;
        }
        if (status.CurrentCost < 200)
        {
            status.CurrentCost += 5;                                                                   //コストの増加量は相談
        }
        //for (int i = 0; i < growPoint.CurrentAtackTime_GrowPoint - growPoint.CurrentAtackTime_GrowPoint; i++)
        //    status.CurrentAtackTime += (int)(status.GetAtackTime * 0.15f);
    }

}

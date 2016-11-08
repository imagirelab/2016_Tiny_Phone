using UnityEngine;
using System.Collections;
using NCMB;

public class CharaButton : MonoBehaviour
{
    public enum Type
    {
        PUPU,
        POPO,
        PIPI
    }
    [SerializeField, TooltipAttribute("種類")]
    private Type DemonType = Type.POPO;

    public enum Order
    {
        Top,
        Middle,
        Bottom
    }
    [SerializeField, TooltipAttribute("種類")]
    private Order demonOrder = Order.Top;

    [SerializeField, TooltipAttribute("デーモンのステータス情報が入ってるオブジェクトを選択")]
    public GameObject _DemonData;           //送信するためのオブジェクトデータ

    private Demons growpointData;

    public GameObject CommandObj;

    public Sprite notButtonDown;            //ボタンが押されてない時のスプライト
    public Sprite ButtonDown;               //ボタンが押された時のスプライト


    public PlayerCost CostData;             //プレイヤーコスト

    [HideInInspector]
    public bool buttonDownFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)
    [HideInInspector]
    public bool runFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)

    private SpriteRenderer _spriteRender;   //GetComponentを多く使うので事前に確保

    // クラスのNCMBObjectを作成するためのオブジェクト
    NCMBObject demonDataClass;        //デーモンのデータ情報

    public float cooltime = 0.5f;
    private float timer = 0;

    // Use this for initialization
    void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        growpointData = _DemonData.GetComponent<Demons>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //ボタンが押されている時の処理
        if (Input.GetMouseButton(0) && buttonDownFlag)
        {
            _spriteRender.sprite = ButtonDown;
        }

        //ボタンが離された時の処理
        if (Input.GetMouseButtonUp(0))
        {
            _spriteRender.sprite = notButtonDown;
            buttonDownFlag = false;
        }

        //TapManagerからrunFlagを受け取っているか確認
        if (runFlag)
        {
            Summon();
            runFlag = false;       
        }
    }

    public void CommandCheck()
    {
        Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        if (aCollider2d && timer > cooltime)
        {
            if (this.demonOrder == Order.Top)
            {                
                Summon();
            }
            else if (this.demonOrder == Order.Middle)
            {
                Summon();
            }
            else if (this.demonOrder == Order.Bottom)
            {
                Summon();
            }

            timer = 0;
        }

        CommandObj.SetActive(false);
    }

    public void Summon()
    {
        if ((StaticVariables.PlayerNo == 1 || StaticVariables.PlayerNo == 2))
        {
            if (CostData.UseableCost(_DemonData.GetComponent<Unit>().status.CurrentCost))
            {
                demonDataClass = new NCMBObject("DemonData");

                demonDataClass["PlayerNo"] = StaticVariables.PlayerNo - 1;
                demonDataClass["Level"] = System.Convert.ToInt32(growpointData.GrowPoint.CurrentSpiritLevel.ToString());
                demonDataClass["Direction"] = demonOrder.ToString();
                demonDataClass["Type"] = DemonType.ToString();

                // データストアへの登録
                demonDataClass.SaveAsync();
            }
        }
    }
}

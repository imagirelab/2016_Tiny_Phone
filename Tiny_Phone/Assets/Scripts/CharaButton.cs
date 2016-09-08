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

    //public GameObject CommandObj;

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

    // Use this for initialization
    void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンが押されている時の処理
        if (Input.GetMouseButton(0) && buttonDownFlag)
        {
            _spriteRender.sprite = ButtonDown;
            //CommandObj.SetActive(true);
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
            if (CostData.UseableCost(_DemonData.GetComponent<Unit>().status.CurrentCost))
            {
                demonDataClass = new NCMBObject("DemonData");

                demonDataClass["PlayerNo"] = "1";
                demonDataClass["HP"] = (_DemonData.GetComponent<Demons>().GrowPoint.CurrentHP_GrowPoint).ToString();
                demonDataClass["ATK"] = (_DemonData.GetComponent<Demons>().GrowPoint.CurrentATK_GrowPoint).ToString();
                demonDataClass["DEX"] = (_DemonData.GetComponent<Demons>().GrowPoint.CurrentSPEED_GrowPoint).ToString();
                demonDataClass["Order"] = "Summon";
                demonDataClass["Direction"] = demonOrder.ToString();
                demonDataClass["Type"] = DemonType.ToString();

                // データストアへの登録
                demonDataClass.SaveAsync();
            }
            CommandInit();
            runFlag = false;       
        }
    }

    public void CommandCheck()
    {
        Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        if (aCollider2d)
        {
            if (aCollider2d.tag == "Top")
            {
                demonDataClass = new NCMBObject("DemonData");
                demonOrder = Order.Top;
                demonDataClass["Order"] = demonOrder.ToString();
                demonDataClass["Type"] = DemonType.ToString();
                // データストアへの登録
                demonDataClass.SaveAsync();
            }
            else if (aCollider2d.tag == "Middle")
            {
                demonDataClass = new NCMBObject("DemonData");
                demonOrder = Order.Middle;
                demonDataClass["Order"] = demonOrder.ToString();
                demonDataClass["Type"] = DemonType.ToString();
                // データストアへの登録
                demonDataClass.SaveAsync();
            }
            else if (aCollider2d.tag == "Bottom")
            {
                demonDataClass = new NCMBObject("DemonData");
                demonOrder = Order.Bottom;
                demonDataClass["Order"] = demonOrder.ToString();
                demonDataClass["Type"] = DemonType.ToString();
                // データストアへの登録
                demonDataClass.SaveAsync();
            }
        }

        CommandInit();
    }

    public void CommandInit()
    {
        //CommandObj.SetActive(false);
    }
}

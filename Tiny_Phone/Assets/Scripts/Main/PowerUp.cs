using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{ 
    public Sprite notButtonDown;            //ボタンが押されてない時のスプライト
    public Sprite ButtonDown;               //ボタンが押された時のスプライト

    public GameObject demon;                //参照用のデーモンデータ

    [HideInInspector]
    public bool buttonDownFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)
    [HideInInspector]
    public bool runFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)

    private SpriteRenderer _spriteRender;   //GetComponentを多く使うので事前に確保

    GrowPoint spiritGrowPoint;              //仮保存するための成長値置物

    //成長値の保存変数
    public GrowPoint growPoint;

    public GameObject CommandObj;           //コマンドボタン出現させるため

    // Use this for initialization
    void Start ()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ボタンが押されている時の処理
        if (Input.GetMouseButton(0) && buttonDownFlag)
        {
            _spriteRender.sprite = ButtonDown;
            CommandObj.SetActive(true);
        }

        //ボタンが離された時の処理
        if(Input.GetMouseButtonUp(0))
        {
            _spriteRender.sprite = notButtonDown;
            buttonDownFlag = false;        
        }

        //TapManagerからrunFlagを受け取っているか確認
        if (runFlag)
        {
            useSpirit();

            if (SpiritManager.useSpiritFlag)
            {
                //ボタンのゲームオブジェクトの名前から参照("名前をしっかり整えること")
                if (this.gameObject.name.ToString().Contains("pupu"))
                {
                    Debug.Log("ププは魂を吸収して強化した");
                }
                else if (this.gameObject.name.ToString().Contains("popo"))
                {
                    Debug.Log("ポポは魂を吸収して強化した");
                }
                else if (this.gameObject.name.ToString().Contains("pipi"))
                { 
                    Debug.Log("ピピは魂を吸収して強化した");
                }
            }
            CommandObj.SetActive(false);
            runFlag = false;
        }
    }

    public void useSpirit()
    {
        //仮に何も設定されていなかったら空のゲームオブジェクトを入れる
        if (demon == null)
        {
            demon = new GameObject();
        }
        else
        {
            //悪魔の成長値を記憶する
            growPoint = demon.GetComponent<Demons>().GrowPoint;
        }

        //仮として次のレベル分の魂を要求される。
        if (SpiritManager.spiritList.Count > growPoint.CurrentSpiritLevel + 1)
        {
            //成長値の足し方
            growPoint.CurrentSpiritLevel += 1;

            demon.GetComponent<Demons>().GrowPoint = growPoint;

            demon.GetComponent<Demons>().powerUp();

            SpiritManager.useSpiritFlag = true;

            SpiritManager.spiritList.RemoveAt(0);
        }
    }

}

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

    public float requestRate = 5;

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
                Debug.Log("魂を使って強化した");
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
        if (SpiritManager.spiritList.Count >= (int)(growPoint.CurrentSpiritLevel / requestRate) + 1)
        {
            for (int i = 0; i < (int)(growPoint.CurrentSpiritLevel / requestRate) + 1; i++)
            {              
                Debug.Log("<color=green>ID</color>" + i + "\n<color=green>ID</color>" + SpiritManager.spiritList[i].GetComponent<Spirit>().id);
                SpiritManager.spiritList[i].GetComponent<Spirit>().usedFlag = true;
                Debug.Log("<color=red>Flag</color>" + SpiritManager.spiritList[i].GetComponent<Spirit>().usedFlag);
            }

            //成長値の足し方
            growPoint.CurrentSpiritLevel += 1;

            demon.GetComponent<Demons>().GrowPoint = growPoint;

            demon.GetComponent<Demons>().powerUp();

            SpiritManager.useSpiritFlag = true;
        }
    }

}

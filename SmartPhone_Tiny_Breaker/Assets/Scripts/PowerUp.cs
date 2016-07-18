using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{ 
    public Sprite notButtonDown;            //ボタンが押されてない時のスプライト
    public Sprite ButtonDown;               //ボタンが押された時のスプライト

    [HideInInspector]
    public bool buttonDownFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)
    [HideInInspector]
    public bool runFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)

    private GameObject checkObj;            //TapManagerでタップしたオブジェクトを確保する。
    private SpriteRenderer _spriteRender;   //GetComponentを多く使うので事前に確保

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

            runFlag = false;
        }
    }
}

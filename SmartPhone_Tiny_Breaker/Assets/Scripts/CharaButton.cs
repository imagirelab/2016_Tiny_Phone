using UnityEngine;
using System.Collections;

public class CharaButton : MonoBehaviour
{
    public GameObject CommandObj;

    public Sprite notButtonDown;            //ボタンが押されてない時のスプライト
    public Sprite ButtonDown;               //ボタンが押された時のスプライト

    [HideInInspector]
    public bool buttonDownFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)
    [HideInInspector]
    public bool runFlag = false;     //ボタンが押されたかのフラグを確認(呼び出しにだけ使うのでInspectorには表示しない。)

    private SpriteRenderer _spriteRender;   //GetComponentを多く使うので事前に確保

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
            CommandObj.SetActive(true);
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
            //ボタンのゲームオブジェクトの名前から参照("名前をしっかり整えること")
            if (this.gameObject.name.ToString().Contains("pupu"))
            {
                Debug.Log("ププを召喚した");
            }
            else if (this.gameObject.name.ToString().Contains("popo"))
            {
                Debug.Log("ポポを召喚した");
            }
            else if (this.gameObject.name.ToString().Contains("pipi"))
            {
                Debug.Log("ピピを召喚した");
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
            //ボタンのゲームオブジェクトの名前から参照("名前をしっかり整えること")
            if (this.gameObject.name.ToString().Contains("pupu"))
            {
                if (aCollider2d.name == "castle")
                {
                    Debug.Log("ププは城");
                }
                else if (aCollider2d.name == "house")
                {
                    Debug.Log("ププは家");
                }
                else if (aCollider2d.name == "soldier")
                {
                    Debug.Log("ププは兵");
                }
                else if (aCollider2d.name == "spirit")
                {
                    Debug.Log("ププは魂");
                }
            }
            else if (this.gameObject.name.ToString().Contains("popo"))
            {
                if (aCollider2d.name == "castle")
                {
                    Debug.Log("ポポは城");
                }
                else if (aCollider2d.name == "house")
                {
                    Debug.Log("ポポは家");
                }
                else if (aCollider2d.name == "soldier")
                {
                    Debug.Log("ポポは兵");
                }
                else if (aCollider2d.name == "spirit")
                {
                    Debug.Log("ポポは魂");
                }
            }
            else if (this.gameObject.name.ToString().Contains("pipi"))
            {
                if (aCollider2d.name == "castle")
                {
                    Debug.Log("ピピは城");
                }
                else if (aCollider2d.name == "house")
                {
                    Debug.Log("ピピは家");
                }
                else if (aCollider2d.name == "soldier")
                {
                    Debug.Log("ピピは兵");
                }
                else if (aCollider2d.name == "spirit")
                {
                    Debug.Log("ピピは魂");
                }
            }
        }

        CommandInit();
    }

    public void CommandInit()
    {
        CommandObj.SetActive(false);
    }
}

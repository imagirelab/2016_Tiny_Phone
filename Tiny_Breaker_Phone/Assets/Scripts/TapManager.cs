using UnityEngine;
using System.Collections;

public class TapManager : MonoBehaviour
{
    private GameObject buttonDownObj;     //仮の置物
    private GameObject buttonUpObj;       //仮の置物

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        //クリックしたときのみ
        if (Input.GetMouseButtonDown(0))
        {
            //タップ地点に当たり判定があった場合
            if (aCollider2d)
            {
                //タップした地点のオブジェクト取得 
                buttonDownObj = aCollider2d.gameObject;

                //パワーアップボタンだった場合
                if (buttonDownObj.tag == "PowerUp")
                {
                    //ボタンオブジェクトに入れてるPowerUpスクリプトのbuttonDownFlagをOnにして処理を実行させる。
                    buttonDownObj.GetComponent<PowerUp>().buttonDownFlag = true;
                }
                //キャラボタンだった場合
                else if (buttonDownObj.tag == "Chara")
                {
                    //ボタンオブジェクトに入れてるButtonスクリプトのbuttonDownFlagをOnにして処理を実行させる。
                    buttonDownObj.GetComponent<CharaButton>().buttonDownFlag = true;
                }
            }
        }

        //マウスボタンを離したときのみ
        if(Input.GetMouseButtonUp(0))
        {
            //タップ地点に当たり判定があった場合
            if (aCollider2d)
            {
                buttonUpObj = aCollider2d.gameObject;

                //パワーアップボタンかつ押した時と同じオブジェクトか確認
                if (buttonUpObj.tag == "PowerUp" && buttonUpObj == buttonDownObj)
                {
                    buttonDownObj.GetComponent<PowerUp>().runFlag = true;
                }
                //キャラボタンかつ押した時と同じオブジェクトか確認
                else if (buttonUpObj.tag == "Chara" && buttonUpObj == buttonDownObj)
                {
                    buttonDownObj.GetComponent<CharaButton>().runFlag = true;
                }
                //離した時にコマンドボタンか確認
                else if (buttonUpObj.tag == "Command")
                {
                    buttonDownObj.GetComponent<CharaButton>().CommandCheck();
                }
            }
            else
            {
                //キャラボタンだった場合
                if (buttonDownObj.tag == "Chara")
                {
                    buttonDownObj.GetComponent<CharaButton>().CommandInit();
                }
            }
        }
    }
}

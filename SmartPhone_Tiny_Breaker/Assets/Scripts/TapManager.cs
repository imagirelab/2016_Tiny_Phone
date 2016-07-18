using UnityEngine;
using System.Collections;

public class TapManager : MonoBehaviour
{
    public GameObject commandObj;

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

        //タップ地点に当たり判定があった場合
        if (aCollider2d)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //タップした地点のオブジェクト取得 
                buttonDownObj = aCollider2d.gameObject;

                //パワーアップボタンだった場合
                if (buttonDownObj.tag == "PowerUp")
                {
                    //ボタンオブジェクトに入れてるPowerUpスクリプトのbuttonDownFlagをOnにして処理を実行させる。
                    buttonDownObj.GetComponent<PowerUp>().buttonDownFlag = true;
                }
                //コマンドボタンだった場合
                else if(buttonDownObj.tag == "Command")
                {
                    //ボタンオブジェクトに入れてるButtonスクリプトのbuttonDownFlagをOnにして処理を実行させる。
                    buttonDownObj.GetComponent<CommandButton>().buttonDownFlag = true;
                    commandObj.transform.position = buttonDownObj.transform.position;
                    commandObj.SetActive(true);
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                buttonUpObj = aCollider2d.gameObject;

                //パワーアップボタンかつ押した時と同じオブジェクトか確認
                if (buttonUpObj.tag == "PowerUp" && buttonUpObj == buttonDownObj)
                {
                    buttonUpObj.GetComponent<PowerUp>().runFlag = true;
                }
                //コマンドボタンかつ押した時と同じオブジェクトか確認
                else if (buttonUpObj.tag == "Command" && buttonUpObj == buttonDownObj)
                {
                    buttonUpObj.GetComponent<CommandButton>().runFlag = true;
                }                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            commandObj.SetActive(false);
        }

    }
}

using UnityEngine;
using System.Collections;
using NCMB;

public class Deadly : MonoBehaviour
{
    public enum DeadlyType
    {
        Fire,
    }
    public DeadlyType myType = DeadlyType.Fire;

    public int deadlyCost = 3;

    // クラスのNCMBObjectを作成するためのオブジェクト
    NCMBObject deadlyRequest;        //リクエスト情報

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

        if (aCollider2d)
        {
            //ボタンが押されている時の処理
            if (Input.GetMouseButtonDown(0) && SpiritManager.spiritList.Count >= deadlyCost && aCollider2d.name == this.gameObject.name)
            {
                SpiritManager.useSpiritFlag = true;

                for(int i = 0; i < deadlyCost; i++)
                {
                    SpiritManager.spiritList.RemoveAt(0);
                }

                //データ送信
                deadlyRequest = new NCMBObject("DeadlyData");

                deadlyRequest["PlayerNo"] = StaticVariables.PlayerNo - 1;
                deadlyRequest["Type"] = myType.ToString();

                //データストアに登録
                deadlyRequest.SaveAsync();
            }
        }
    }
}

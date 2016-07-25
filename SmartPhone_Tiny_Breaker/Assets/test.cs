using UnityEngine;
using System.Collections;
using NCMB;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        // クラスのNCMBObjectを作成
        NCMBObject testClass = new NCMBObject("TestClass");

        // オブジェクトに値を設定

        testClass["message"] = "Hello, NCMB!";
        // データストアへの登録
        testClass.SaveAsync();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

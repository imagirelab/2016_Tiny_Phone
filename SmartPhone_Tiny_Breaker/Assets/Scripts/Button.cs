using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public Sprite notButtonDown;            //ボタンが押されてない時のスプライト
    public Sprite ButtonDown;               //ボタンが押された時のスプライト

    

    private bool oneCall       = false;                   //処理を一回だけにするためのフラグ
    private SpriteRenderer _spriteRender;   //GetComponentを多く使うので事前に確保
    private string str;                     //取得したオブジェクトの名前用

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}

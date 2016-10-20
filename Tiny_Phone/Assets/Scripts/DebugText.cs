using UnityEngine;
using System.Collections;

public class DebugText : MonoBehaviour
{
    private TextMesh _text;

    private string temp;

	// Use this for initialization
	void Start ()
    {
        _text = this.GetComponent<TextMesh>();
        temp = _text.text;        
	}
	
	// Update is called once per frame
	void Update ()
    {
        _text.text = temp + StaticVariables.PlayerNo.ToString();
	}
}

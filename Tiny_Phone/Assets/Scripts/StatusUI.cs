using UnityEngine;

public class StatusUI : MonoBehaviour
{
    private GameObject parent;

    private TextMesh _text;

	// Use this for initialization
	void Start ()
    {
        parent = transform.parent.gameObject;
        _text = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _text.text = "HP:" + parent.GetComponent<Unit>().status.CurrentHP + 
                                            "\nATK:" + parent.GetComponent<Unit>().status.CurrentATK + 
                                            "\nDEX:" + parent.GetComponent<Unit>().status.CurrentSPEED;
	}
}

using UnityEngine;
using System.Collections;

public class CostText : MonoBehaviour
{
    public GameObject player;

    private Demons parentDemon;

    private TextMesh _text;

	// Use this for initialization
	void Start ()
    {
        parentDemon = this.GetComponentInParent<Demons>();
        _text = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _text.text = "Lv: " + parentDemon.GrowPoint.CurrentSpiritLevel + " CP: " + (parentDemon.status.CurrentCost + player.GetComponent<PlayerCost>().GetDemonCost).ToString();
	}
}

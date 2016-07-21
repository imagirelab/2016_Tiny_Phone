using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiritManager : MonoBehaviour
{
    public GameObject spiritData;

    public GameObject spiritHolder;

    public int SpiritLimit = 5;

    public static List<GameObject> spiritList = new List<GameObject>();

    public static bool useSpiritFlag = false;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void useSpirit()
    {
        if (spiritList.Count > 0)
        {
            spiritList.RemoveAt(0);
            useSpiritFlag = true;
        }
        else
        {
            useSpiritFlag = false;
        }
    }
}

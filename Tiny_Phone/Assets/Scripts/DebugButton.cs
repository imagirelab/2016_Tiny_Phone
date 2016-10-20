using UnityEngine;
using System.Collections;

public class DebugButton : MonoBehaviour
{
    public enum _PlayerNo
    {
        One,
        Two
    }
    public _PlayerNo debugNo = _PlayerNo.One;

    public void OnClick()
    {
        if(debugNo == _PlayerNo.One)
        {
            StaticVariables.PlayerNo = 1;
        }
        else if(debugNo == _PlayerNo.Two)
        {
            StaticVariables.PlayerNo = 2;
        }
    }
}

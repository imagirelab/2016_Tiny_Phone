using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour
{
    public enum StatusType
    {
        ATACK,
        HP,
        SPEED
    }
    [SerializeField, TooltipAttribute("種類")]
    private StatusType statusType = StatusType.ATACK;

    [SerializeField, TooltipAttribute("デーモンのステータス情報が入ってるオブジェクトを選択")]
    public GameObject DemonData;

    private Status _statusData;

	// Use this for initialization
	void Start ()
    {
        _statusData = DemonData.GetComponent<Unit>().status;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (statusType == StatusType.HP)
        {
            this.transform.localScale = new Vector3(_statusData.CurrentHP / _statusData.maxCurrentHP, 1.0f, 1.0f);
            Debug.Log(_statusData.CurrentHP);
            Debug.Log(_statusData.maxCurrentHP);
        }
        if (statusType == StatusType.ATACK)
        {
            this.transform.localScale = new Vector3(_statusData.CurrentATK / _statusData.maxCurrentATK, 1, 1);
        }
        if (statusType == StatusType.SPEED)
        {
            this.transform.localScale = new Vector3(_statusData.CurrentSPEED / _statusData.maxCurrentSPEED, 1, 1);
        }
    }
}

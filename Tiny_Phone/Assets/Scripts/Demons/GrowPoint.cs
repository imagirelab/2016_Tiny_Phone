using UnityEngine;

//悪魔達の初期情報(structの代わりにclassを使用)
[System.Serializable]
public class GrowPoint
{
    public enum Type
    {
        POPO,
        PUPU,
        PIPI
    }
    [SerializeField, TooltipAttribute("種類")]
    private Type DemonType = Type.POPO;
    [SerializeField, TooltipAttribute("レベル")]
    private int SpiritLevel = 0;

    public Type GetDemonType { get { return DemonType; } }
    public int GetSpiritLevel { get { return SpiritLevel; } }

    //プレハブのすべて共有の値になってしまうため
    //元々のステータスはいじらないようにするため
    //別の変数を用意
    private int currentSpiritLevel;

    public int CurrentSpiritLevel
    {
        get { return currentSpiritLevel; }
        set { currentSpiritLevel = value; }
    }

    //現在の成長値に代入する
    public void SetGrowPoint()
    {
        currentSpiritLevel = SpiritLevel;
    }

    //基準を変えたいときに呼び出す
    public void SetDefault(Type type, int level)
    {
        DemonType = type;

        SpiritLevel = level;

        SetGrowPoint();
    }
}
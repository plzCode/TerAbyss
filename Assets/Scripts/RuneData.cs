using UnityEngine;

[CreateAssetMenu(fileName = "RuneData", menuName = "Scriptable Objects/RuneData")]
public class RuneData : ScriptableObject
{
    [SerializeField] private string runeName;
    [SerializeField] private Sprite runeInSlotSprite;
    [SerializeField] private Sprite runeSprite;

    public string RuneName => runeName;
    public Sprite RuneInSlotSprite => runeInSlotSprite;
    public Sprite RuneSprite => runeSprite;
}

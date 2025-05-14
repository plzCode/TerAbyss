using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string skillName;
    [SerializeField] private string skillDesc;

    public Sprite Icon => icon;
    public string SkillName => skillName;
    public string SkillDesc => skillDesc;
}

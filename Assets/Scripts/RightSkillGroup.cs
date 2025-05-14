using UnityEngine;
using UnityEngine.UI;

public class RightSkillGroup : MonoBehaviour
{
    public static RightSkillGroup Instance { get; private set; }

    [SerializeField] private Image icon;
    [SerializeField] private GameObject skillName;
    [SerializeField] private GameObject skillDesc;

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePage(SkillData data)
    {
        icon.sprite = data.Icon;

        RuneSpawner.Instance.SpawnFromString(data.SkillName, skillName.transform);

        RuneSpawner.Instance.SpawnFromString(data.SkillDesc, skillDesc.transform);
    }
}

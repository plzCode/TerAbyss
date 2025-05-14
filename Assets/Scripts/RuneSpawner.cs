using UnityEngine;
using UnityEngine.UI;

public class RuneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject runeBase;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Text theTextFromUI;
    [SerializeField] private bool autoTranslation;

    public static RuneSpawner Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (autoTranslation) { SpawnFromTextComponent(); }
    }

    public void SpawnFromString(string text, Transform parent)
    {
        // 기존 룬 제거
        foreach (Transform child in parent) { Destroy(child.gameObject); }

        foreach (char name in text.ToLower())
        {
            if (AllRunes.Instance.TryGetRune(name, out var data))
            {
                var rune = Instantiate(runeBase, parent);
                rune.GetComponent<RuneInstance>().InitWord(data);
            }
        }
    }

    public void SpawnFromTextComponent()
    {
        SpawnFromString(theTextFromUI.text, spawnParent);
    }
}

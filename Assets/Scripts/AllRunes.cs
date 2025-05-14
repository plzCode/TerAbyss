using System.Collections.Generic;
using UnityEngine;

public class AllRunes : MonoBehaviour
{
    public static AllRunes Instance { get; private set; }

    [SerializeField] private List<RuneData> allRuneDatas;
    private Dictionary<char, RuneData> runeMap;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        runeMap = new();

        foreach (var data in allRuneDatas)
        {
            if (!string.IsNullOrEmpty(data.RuneName))
            {
                char key = char.ToLower(data.RuneName[0]);
                runeMap[key] = data;
            }
        }
    }

    public bool TryGetRune(char name, out RuneData data)
    {
        return runeMap.TryGetValue(char.ToLower(name), out data);
    }
}

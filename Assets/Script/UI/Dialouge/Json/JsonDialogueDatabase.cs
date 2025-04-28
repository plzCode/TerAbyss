using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonDialogueLine
{
    public string characterName; // 캐릭터 이름
    public string text; // 대사 내용
}

[System.Serializable]
public class JsonDialogueData
{
    public List<JsonDialogueLine> lines; // 여러 줄의 대사
}

public class JsonDialogueDatabase : MonoBehaviour
{
    [Header("Settings")]
    public string dialogueFileName = "DialogueData"; // Resources 폴더 안의 JSON 파일 이름

    private Dictionary<string, JsonDialogueData> dialogues; // 대화 ID를 키로 하는 대화 데이터 저장소

    void Start()
    {
        LoadDialogueFile(); // 대화 데이터 로드
    }

    // 대화 데이터 로드 함수
    public void LoadDialogueFile()
    {
        dialogues = new Dictionary<string, JsonDialogueData>();

        // Resources 폴더에서 JSON 파일을 읽어옴
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFileName);
        if (jsonFile == null)
        {
            Debug.LogError("Dialogue JSON file not found!");
            return;
        }

        JsonDialogueDatabaseWrapper wrapper = JsonUtility.FromJson<JsonDialogueDatabaseWrapper>(jsonFile.text); // JSON 데이터를 클래스에 매핑

        // 대화 ID를 키로 대화 데이터를 딕셔너리에 저장
        foreach (var group in wrapper.dialogueGroups)
        {
            dialogues[group.dialogueId] = group.dialogueData;
        }
    }

    // 대화 ID로 대화 데이터를 가져오는 함수
    public JsonDialogueData GetDialogue(string dialogueId)
    {
        if (dialogues.ContainsKey(dialogueId))
        {
            return dialogues[dialogueId];
        }
        else
        {
            Debug.LogWarning($"Dialogue ID '{dialogueId}' not found!");
            return null;
        }
    }
}

// JSON 데이터 구조
[System.Serializable]
public class JsonDialogueDatabaseWrapper
{
    public List<JsonDialogueGroup> dialogueGroups;  // 여러 대화 그룹
}

[System.Serializable]
public class JsonDialogueGroup
{
    public string dialogueId;        // 대화 ID
    public JsonDialogueData dialogueData;  // 대화 내용
}
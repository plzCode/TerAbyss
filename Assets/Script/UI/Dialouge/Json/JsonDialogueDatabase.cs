using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonDialogueLine
{
    public string characterName; // ĳ���� �̸�
    public string text; // ��� ����
}

[System.Serializable]
public class JsonDialogueData
{
    public List<JsonDialogueLine> lines; // ���� ���� ���
}

public class JsonDialogueDatabase : MonoBehaviour
{
    [Header("Settings")]
    public string dialogueFileName = "DialogueData"; // Resources ���� ���� JSON ���� �̸�

    private Dictionary<string, JsonDialogueData> dialogues; // ��ȭ ID�� Ű�� �ϴ� ��ȭ ������ �����

    void Start()
    {
        LoadDialogueFile(); // ��ȭ ������ �ε�
    }

    // ��ȭ ������ �ε� �Լ�
    public void LoadDialogueFile()
    {
        dialogues = new Dictionary<string, JsonDialogueData>();

        // Resources �������� JSON ������ �о��
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFileName);
        if (jsonFile == null)
        {
            Debug.LogError("Dialogue JSON file not found!");
            return;
        }

        JsonDialogueDatabaseWrapper wrapper = JsonUtility.FromJson<JsonDialogueDatabaseWrapper>(jsonFile.text); // JSON �����͸� Ŭ������ ����

        // ��ȭ ID�� Ű�� ��ȭ �����͸� ��ųʸ��� ����
        foreach (var group in wrapper.dialogueGroups)
        {
            dialogues[group.dialogueId] = group.dialogueData;
        }
    }

    // ��ȭ ID�� ��ȭ �����͸� �������� �Լ�
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

// JSON ������ ����
[System.Serializable]
public class JsonDialogueDatabaseWrapper
{
    public List<JsonDialogueGroup> dialogueGroups;  // ���� ��ȭ �׷�
}

[System.Serializable]
public class JsonDialogueGroup
{
    public string dialogueId;        // ��ȭ ID
    public JsonDialogueData dialogueData;  // ��ȭ ����
}
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName; // ĳ���� �̸�
    public string text; // ��� ����
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> lines; // ���� ���� ���
}

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Dialogue/Database", order = 1)]
public class DialogueDatabase : ScriptableObject
{
    [SerializeField] // �� �Ӽ��� �߰��Ͽ� Inspector���� ���� �����ϵ��� ����
    public List<DialogueGroup> dialogueGroups;

    public DialogueData GetDialogue(string dialogueId)
    {
        foreach (var group in dialogueGroups)
        {
            if (group.dialogueId == dialogueId)
            {
                return group.dialogueData;
            }
        }
        return null;
    }
}

[System.Serializable]
public class DialogueGroup
{
    public string dialogueId; // ��ȭ ID
    public DialogueData dialogueData; // ��ȭ ����
}
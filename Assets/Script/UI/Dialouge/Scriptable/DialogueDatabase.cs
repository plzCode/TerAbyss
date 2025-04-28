using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string characterName; // 캐릭터 이름
    public string text; // 대사 내용
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueLine> lines; // 여러 줄의 대사
}

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Dialogue/Database", order = 1)]
public class DialogueDatabase : ScriptableObject
{
    [SerializeField] // 이 속성을 추가하여 Inspector에서 수정 가능하도록 설정
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
    public string dialogueId; // 대화 ID
    public DialogueData dialogueData; // 대화 내용
}
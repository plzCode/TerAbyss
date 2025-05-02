using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogueId; // 이 NPC가 사용할 대화 ID

    private bool isPlayerInRange = false;

    void Update()
    {
        // 플레이어가 범위 안에 있고, E 키를 눌렀을 때 대화 시작
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {   
            JsonDialogueManager dialogueManager = FindFirstObjectByType<JsonDialogueManager>();
            if (dialogueManager != null)
            {
                if (!dialogueManager.IsDialogueActive())  // 대화가 진행 중이지 않으면
                {
                    dialogueManager.gameObject.SetActive(true);  // DialogueManager 활성화
                    dialogueManager.StartDialogue(dialogueId);  // 대화 시작
                }
                else
                {
                    dialogueManager.NextLine();  // 대화가 진행 중이라면 다음 대사로 진행
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JsonDialogueManager dialogueManager = FindFirstObjectByType<JsonDialogueManager>();
            dialogueManager.EndDialogue();
            isPlayerInRange = false;
        }
    }
}
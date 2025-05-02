using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string dialogueId; // �� NPC�� ����� ��ȭ ID

    private bool isPlayerInRange = false;

    void Update()
    {
        // �÷��̾ ���� �ȿ� �ְ�, E Ű�� ������ �� ��ȭ ����
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {   
            JsonDialogueManager dialogueManager = FindFirstObjectByType<JsonDialogueManager>();
            if (dialogueManager != null)
            {
                if (!dialogueManager.IsDialogueActive())  // ��ȭ�� ���� ������ ������
                {
                    dialogueManager.gameObject.SetActive(true);  // DialogueManager Ȱ��ȭ
                    dialogueManager.StartDialogue(dialogueId);  // ��ȭ ����
                }
                else
                {
                    dialogueManager.NextLine();  // ��ȭ�� ���� ���̶�� ���� ���� ����
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
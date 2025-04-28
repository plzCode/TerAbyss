using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Settings")]
    public string dialogueFileName = "DialogueData"; // Resources ���� �ȿ� DialogueData.json
    public float typingSpeed = 0.05f; // Ÿ���� ������ (1���� ��¸��� ��ٸ��� �ð�)

    private DialogueDatabase dialogueDatabase;  // DialogueDatabase�� ������ �ʵ� �߰�
    private List<DialogueLine> currentDialogue;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        // DialogueDatabase�� Resources���� �ҷ����� (�Ǵ� �ν����Ϳ��� �Ҵ� ����)
        dialogueDatabase = Resources.Load<DialogueDatabase>("DialogueDatabase"); // "DialogueDatabase"�� ���� �̸�
        if (dialogueDatabase == null)
        {
            Debug.LogError("DialogueDatabase�� ã�� �� �����ϴ�.");
        }
    }

    public void StartDialogue(string dialogueId)
    {
        // DialogueDatabase���� ��ȭ ID�� ��ȭ �����͸� ã�ƿ�
        if (dialogueDatabase != null)
        {
            currentDialogue = dialogueDatabase.GetDialogue(dialogueId)?.lines;
            if (currentDialogue != null)
            {
                currentLineIndex = 0;
                DisplayCurrentLine();
            }
            else
            {
                Debug.LogWarning($"Dialogue ID '{dialogueId}' not found in database!");
            }
        }
    }

    public void DisplayCurrentLine()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue[currentLineIndex];
        nameText.text = line.characterName;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(line.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (isTyping)
        {
            // Ÿ���� �߿� ��ư ������ �ٷ� ��ü ���� ���
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogueText.text = currentDialogue[currentLineIndex].text;
            isTyping = false;
            return;
        }

        currentLineIndex++;
        DisplayCurrentLine();
    }

    void EndDialogue()
    {
        Debug.Log("��ȭ ����");
        nameText.text = "";
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(false); // �г� ��Ȱ��ȭ
    }
}
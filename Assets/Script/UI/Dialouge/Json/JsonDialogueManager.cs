using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class JsonDialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nameText;     // ��ȭ ĳ���� �̸��� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI dialogueText; // ��ȭ ������ ǥ���� �ؽ�Ʈ
    public Transform uiPanel; // UI �г� (��ȭâ)

    [Header("Settings")]
    public string dialogueFileName = "DialogueData"; // Resources ���� ���� JSON ���� �̸�
    public float typingSpeed = 0.05f;               // Ÿ���� �ӵ� (1���ڸ��� ��� �ð�)

    private Dictionary<string, JsonDialogueData> dialogues;   // ��ȭ ID�� Ű�� �ϴ� ��ȭ ������ �����
    private List<JsonDialogueLine> currentDialogue;           // ���� ��ȭ ���
    private int currentLineIndex = 0;                     // ���� ��ȭ �� �ε���
    private Coroutine typingCoroutine;                     // Ÿ���� �ڷ�ƾ
    private bool isTyping = false;                        // Ÿ���� ������ ����

    void Start()
    {
        LoadDialogueFile();  // JSON ���� �ε�
    }

    // ��ȭ ���� �Լ� (dialogueId�� ���� ��ȭ ����)
    public void StartDialogue(string dialogueId)
    {
        if (dialogues.ContainsKey(dialogueId))  // �־��� ��ȭ ID�� �ش��ϴ� ��ȭ �����Ͱ� �ִ��� Ȯ��
        {
            currentDialogue = dialogues[dialogueId].lines;
            currentLineIndex = 0;   // ��ȭ �� �ε��� �ʱ�ȭ
            DisplayCurrentLine();    // ù ��° ��� ���
        }
        else
        {
            Debug.LogWarning($"Dialogue ID '{dialogueId}' not found!");
        }
    }

    // ���� ��� ���
    public void DisplayCurrentLine()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.Count)
        {
            EndDialogue();  // ��ȭ�� ������ ����
            return;
        }
        uiPanel.gameObject.SetActive(true); // UI Ȱ��ȭ
        JsonDialogueLine line = currentDialogue[currentLineIndex];  // ���� ���
        nameText.text = line.characterName;    // ĳ���� �̸� ���

        // Ÿ���� ���̶�� ���� Ÿ���� �ڷ�ƾ�� ����
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(line.text));  // ���ο� ��� ��� (Ÿ���� ȿ��)
    }

    // �� ���ھ� Ÿ���� ȿ���� ���
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";  // ���� �ؽ�Ʈ ����

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;  // ���� �߰�
            yield return new WaitForSeconds(typingSpeed);  // ������ Ÿ���� �ӵ���ŭ ���
        }

        isTyping = false;  // Ÿ���� ��
    }

    // ���� ���� ����
    public void NextLine()
    {
        if (isTyping)  // Ÿ���� ���̶�� �ٷ� ��ü ������ ���
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogueText.text = currentDialogue[currentLineIndex].text;  // ��ü ���� ���
            isTyping = false;
            return;
        }

        currentLineIndex++;   // ���� ���� �̵�
        if (currentLineIndex < currentDialogue.Count)
        {
            DisplayCurrentLine();  // ���� ��� ���
        }
        else
        {
            EndDialogue();  // ��ȭ ��
        }
    }

    // ��ȭ ����
    public void EndDialogue()
    {
        Debug.Log("Dialogue Ended");
        nameText.text = "";
        dialogueText.text = "";
        currentDialogue = null;  // ��ȭ ��� �ʱ�ȭ
        uiPanel.gameObject.SetActive(false);  // UI ��Ȱ��ȭ
    }

    // JSON ������ Resources �������� �ε��ϴ� �Լ�
    void LoadDialogueFile()
    {
        dialogues = new Dictionary<string, JsonDialogueData>();

        // Resources �������� JSON ������ �о��
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFileName);
        if (jsonFile == null)
        {
            Debug.LogError("Dialogue JSON file not found!");
            return;
        }

        JsonDialogueDatabaseWrapper wrapper = JsonUtility.FromJson<JsonDialogueDatabaseWrapper>(jsonFile.text);  // JSON �����͸� Ŭ������ ����

        // ��ȭ ID�� Ű�� ��ȭ �����͸� ��ųʸ��� ����
        foreach (var group in wrapper.dialogueGroups)
        {
            dialogues[group.dialogueId] = group.dialogueData;
        }
    }

    public bool IsDialogueActive()
    {
        return currentDialogue != null && currentLineIndex < currentDialogue.Count;
    }
}
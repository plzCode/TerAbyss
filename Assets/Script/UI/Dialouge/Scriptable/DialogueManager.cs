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
    public string dialogueFileName = "DialogueData"; // Resources 폴더 안에 DialogueData.json
    public float typingSpeed = 0.05f; // 타이핑 딜레이 (1글자 출력마다 기다리는 시간)

    private DialogueDatabase dialogueDatabase;  // DialogueDatabase를 참조할 필드 추가
    private List<DialogueLine> currentDialogue;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        // DialogueDatabase를 Resources에서 불러오기 (또는 인스펙터에서 할당 가능)
        dialogueDatabase = Resources.Load<DialogueDatabase>("DialogueDatabase"); // "DialogueDatabase"는 파일 이름
        if (dialogueDatabase == null)
        {
            Debug.LogError("DialogueDatabase를 찾을 수 없습니다.");
        }
    }

    public void StartDialogue(string dialogueId)
    {
        // DialogueDatabase에서 대화 ID로 대화 데이터를 찾아옴
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
            // 타이핑 중에 버튼 누르면 바로 전체 문장 출력
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
        Debug.Log("대화 종료");
        nameText.text = "";
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(false); // 패널 비활성화
    }
}
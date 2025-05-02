using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class JsonDialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nameText;     // 대화 캐릭터 이름을 표시할 텍스트
    public TextMeshProUGUI dialogueText; // 대화 내용을 표시할 텍스트
    public Transform uiPanel; // UI 패널 (대화창)

    [Header("Settings")]
    public string dialogueFileName = "DialogueData"; // Resources 폴더 안의 JSON 파일 이름
    public float typingSpeed = 0.05f;               // 타이핑 속도 (1글자마다 대기 시간)

    private Dictionary<string, JsonDialogueData> dialogues;   // 대화 ID를 키로 하는 대화 데이터 저장소
    private List<JsonDialogueLine> currentDialogue;           // 현재 대화 목록
    private int currentLineIndex = 0;                     // 현재 대화 줄 인덱스
    private Coroutine typingCoroutine;                     // 타이핑 코루틴
    private bool isTyping = false;                        // 타이핑 중인지 여부

    void Start()
    {
        LoadDialogueFile();  // JSON 파일 로드
    }

    // 대화 시작 함수 (dialogueId를 통해 대화 선택)
    public void StartDialogue(string dialogueId)
    {
        if (dialogues.ContainsKey(dialogueId))  // 주어진 대화 ID에 해당하는 대화 데이터가 있는지 확인
        {
            currentDialogue = dialogues[dialogueId].lines;
            currentLineIndex = 0;   // 대화 줄 인덱스 초기화
            DisplayCurrentLine();    // 첫 번째 대사 출력
        }
        else
        {
            Debug.LogWarning($"Dialogue ID '{dialogueId}' not found!");
        }
    }

    // 현재 대사 출력
    public void DisplayCurrentLine()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.Count)
        {
            EndDialogue();  // 대화가 끝나면 종료
            return;
        }
        uiPanel.gameObject.SetActive(true); // UI 활성화
        JsonDialogueLine line = currentDialogue[currentLineIndex];  // 현재 대사
        nameText.text = line.characterName;    // 캐릭터 이름 출력

        // 타이핑 중이라면 기존 타이핑 코루틴을 종료
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(line.text));  // 새로운 대사 출력 (타이핑 효과)
    }

    // 한 글자씩 타이핑 효과로 출력
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";  // 이전 텍스트 비우기

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;  // 글자 추가
            yield return new WaitForSeconds(typingSpeed);  // 지정된 타이핑 속도만큼 대기
        }

        isTyping = false;  // 타이핑 끝
    }

    // 다음 대사로 진행
    public void NextLine()
    {
        if (isTyping)  // 타이핑 중이라면 바로 전체 문장을 출력
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogueText.text = currentDialogue[currentLineIndex].text;  // 전체 문장 출력
            isTyping = false;
            return;
        }

        currentLineIndex++;   // 다음 대사로 이동
        if (currentLineIndex < currentDialogue.Count)
        {
            DisplayCurrentLine();  // 다음 대사 출력
        }
        else
        {
            EndDialogue();  // 대화 끝
        }
    }

    // 대화 종료
    public void EndDialogue()
    {
        Debug.Log("Dialogue Ended");
        nameText.text = "";
        dialogueText.text = "";
        currentDialogue = null;  // 대화 목록 초기화
        uiPanel.gameObject.SetActive(false);  // UI 비활성화
    }

    // JSON 파일을 Resources 폴더에서 로드하는 함수
    void LoadDialogueFile()
    {
        dialogues = new Dictionary<string, JsonDialogueData>();

        // Resources 폴더에서 JSON 파일을 읽어옴
        TextAsset jsonFile = Resources.Load<TextAsset>(dialogueFileName);
        if (jsonFile == null)
        {
            Debug.LogError("Dialogue JSON file not found!");
            return;
        }

        JsonDialogueDatabaseWrapper wrapper = JsonUtility.FromJson<JsonDialogueDatabaseWrapper>(jsonFile.text);  // JSON 데이터를 클래스에 매핑

        // 대화 ID를 키로 대화 데이터를 딕셔너리에 저장
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
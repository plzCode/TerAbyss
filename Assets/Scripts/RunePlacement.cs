using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunePlacement : MonoBehaviour
{
    public static RunePlacement Instance { get; private set; }

    [SerializeField] private GameObject runeButtonPrefab;
    [SerializeField] private GraphicRaycaster raycaster; // 클릭 판정용
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Canvas parentCanv;
    private Camera uiCam;

    private GameObject currentRune;
    private RuneData selectedRuneData;
    private bool isFollowing;

    private bool isEraseMode;
    public bool IsEraseMode => isEraseMode;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        isFollowing = false;
        isEraseMode = false;
    }

    public void StartFollow(RuneData data, GameObject runePrefab)
    {
        if (currentRune) { Destroy(currentRune); } // 저장된 룬 폐기

        // == 룬 데이터 저장 및 비주얼라이징 ==
        currentRune = Instantiate(runePrefab, parentCanv.transform);
        selectedRuneData = data;
        isFollowing = true;
        // ==
    }

    void Update()
    {
        if(!isFollowing || !currentRune) { return; }

        // == 룬이 마우스 따라가는 매커니즘 ==
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanv.transform as RectTransform,
            Input.mousePosition,
            uiCam,
            out Vector2 localPoint
        );
        currentRune.GetComponent<RectTransform>().localPosition = localPoint;
        // ==

        if(Input.GetMouseButtonDown(0))
        {
            // == 클릭 판정을 위한 이벤트 데이터 ==
            PointerEventData eventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };
            // ==

            // == 현재 마우스 위치에서 raycast ==
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);
            // ==

            foreach (var result in results) // 슬롯 체크
            {
                if (result.gameObject.CompareTag("RuneSlot"))
                {
                    Transform slotParent = result.gameObject.transform.parent;
                    Vector3 slotPos = result.gameObject.transform.position;

                    Destroy(result.gameObject); // 슬롯 제거

                    // == 버튼 생성, 데이터 할당 ==
                    var runeBtn = Instantiate(runeButtonPrefab, slotPos, Quaternion.identity, slotParent);
                    runeBtn.GetComponent<RuneInstance>().InitSlot(selectedRuneData);
                    // ==

                    Destroy(currentRune);
                    currentRune = null;
                    isFollowing = false;
                    return;
                }

            }
            Destroy(currentRune);
            currentRune = null;
            isFollowing = false;
        }
    }

    public void SetEraseMode(bool ToF)
    {
        isEraseMode = ToF;
    }
}

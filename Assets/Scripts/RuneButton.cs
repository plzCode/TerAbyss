using UnityEngine;

public class RuneButton : MonoBehaviour
{
    [SerializeField] private RuneData data;
    [SerializeField] private GameObject runePrefab;
    [SerializeField] private GameObject slotPrefab; // 원본 슬롯

    public void OnClick()
    {
        if (RunePlacement.Instance.IsEraseMode)
        {
            Transform parent = transform.parent;
            Instantiate(slotPrefab, parent.position, Quaternion.identity, parent);
            Destroy(gameObject);
        }
        else
        {
            RunePlacement.Instance.StartFollow(data, runePrefab);
        }
    }
}

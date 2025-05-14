using UnityEngine;
using UnityEngine.UI;

public class RuneInstance : MonoBehaviour
{
    [SerializeField] private RuneData data;
    public RuneData Data => data;

    public void InitSlot(RuneData data)
    {
        this.data = data;
        GetComponent<Image>().sprite = this.data.RuneInSlotSprite;
    }

    public void InitWord(RuneData data)
    {
        this.data = data;
        GetComponent<Image>().sprite = this.data.RuneSprite;
    }
}

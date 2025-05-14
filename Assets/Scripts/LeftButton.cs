using UnityEngine;

public class LeftButton : MonoBehaviour
{
    [SerializeField] private SkillData data;

    public void SkillDataShoot()
    {
        if (data != null)
        {
            RightSkillGroup.Instance.UpdatePage(data);
        }
    }
}

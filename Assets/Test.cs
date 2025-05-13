using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject[] array = new GameObject[5];
    int inteeger = 0;
    public void Start()
    {
    }

    void Update()
    {
        

    }

    public void OnEnable()
    {
        Debug.Log("사용가능!");
    }
    public void OnDisable()
    {
        Debug.Log("사용 불가능..");
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
    public void OnCollisionExit(Collision collision) 
    {

    }
    public void OnCollisionStay(Collision collision)
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

    }
    public void OnTriggerExit(Collider other)
    {
    }
    public void OnTriggerStay(Collider other)
    {
        
    }

}


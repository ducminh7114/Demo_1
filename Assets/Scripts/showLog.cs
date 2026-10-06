using UnityEngine;

public class showLog : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello world");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update called " + Time.frameCount);
    }
}

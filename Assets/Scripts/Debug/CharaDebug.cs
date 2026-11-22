using UnityEngine;

public class CharaDebug : MonoBehaviour
{
    private bool timerStart;
    private float num;
    void Update()
    {
        if (timerStart) Test();
        if (Input.GetKeyDown(KeyCode.R))
        {
            num = 0;
            timerStart = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            timerStart = true;
        }
        Debug.Log (num);
    }
    private void Test()
    {
        num += 10f * Time.deltaTime;
        
    }


}



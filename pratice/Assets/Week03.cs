using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week03 : MonoBehaviour
{
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
            Debug.Log(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cnt++;
            Debug.Log("'space" + cnt + "'");
        }

        if (Input.GetMouseButtonDown(1))
        {
            cnt--;
            Debug.Log("'Mouse" + cnt + "'");
        }
    }
}

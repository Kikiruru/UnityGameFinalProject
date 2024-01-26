using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello_script : MonoBehaviour
{
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos;
        pos = this.gameObject.transform.position;

        //Debug.Log(pos);
        Debug.Log("Check 0: " + count);
        count += 2;
        Debug.Log("Check 1: " + count);

        if (count > 2)
        {
            Debug.Log("Count is two or more");
            Debug.Log("check 2: " + count);
        }

        Debug.Log("Check 3: " + count);
    }

    // Update is called once per frame
    void Update()
    { 
    }
}

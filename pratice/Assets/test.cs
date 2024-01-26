using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Debug.Log("2(@) is pressed!!");
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log(Input.mousePosition);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject pf_wall;
    public float interval;

    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            interval = Random.Range(1.0f, 2.0f);
            Instantiate(pf_wall, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(13, Random.Range(-3.0f, 4.0f), 0);
    }
}

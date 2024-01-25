using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_destroy : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { //10초가 지나면 자동 삭제
        timer += Time.deltaTime;
        if (timer >= 10.0f)
            Destroy(gameObject);
    }
}

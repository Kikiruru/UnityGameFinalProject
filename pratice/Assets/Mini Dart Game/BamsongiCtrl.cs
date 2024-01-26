using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiCtrl : MonoBehaviour
{
    public static float timer = 0.0f;
    bool is_shot = false;
    bool is_hit = false;
    public static float row_wind;
    public static float col_wind;
    private GUIStyle guistyle = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        // 1초당 밤송이에 가해지는 풍속 
        row_wind = Random.Range(-400.0f, 400.0f);
        col_wind = Random.Range(-100.0f, 400.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.05 && !is_shot)
        {
            //Shoot(new Vector3(0, 500, 600));
            is_shot = true;
        }

        if (is_hit || timer > 2.0f)
        {
            this.Shoot(Vector3.right * (row_wind / 60));
            this.Shoot(Vector3.up * (col_wind / 60));
        }
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);
        // 마우스 왼쪽 클릭시 삭제 
    }
    
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }
    
    void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();
        Vector3 collided_position = transform.position;
        if(other.gameObject.tag == "Target")
            is_hit = true;
        float distance = collided_position.x * collided_position.x +
            (collided_position.y - 6.5f) * (collided_position.y - 6.5f);
        distance = Mathf.Sqrt(distance);

        if (distance >= 0.0f && distance <= 0.4f)
            BamsongiGenerator.score += 100;
        else if (distance > 0.4f && distance <= 0.8f)
            BamsongiGenerator.score += 90;
        else if (distance > 0.8f && distance <= 1.2f)
            BamsongiGenerator.score += 70;
        else if (distance > 1.2f && distance <= 1.6f)
            BamsongiGenerator.score += 50;
        else if (distance > 1.6f && distance <= 2.0f)
            BamsongiGenerator.score += 30;
        else
            BamsongiGenerator.score += 0;

        Debug.Log(collided_position);
        Debug.Log(distance);
    }
}

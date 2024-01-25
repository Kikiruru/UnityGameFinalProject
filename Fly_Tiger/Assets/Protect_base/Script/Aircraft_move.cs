using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_move : MonoBehaviour
{
    public static float aircraft_speed; // 전투기의 속도
    float distance;
    int Aircraft_HP;
    int roket_cnt = 2;
    float roket_fire_distanace; // 로켓 발사 지점 거리 
    float fire_time;
    float max_time = 0.5f;
    GameObject target; //Base
    public AudioClip aircraft_destroy;
    public Transform explosion;
    public GameObject roket_prefab;
    // Start is called before the first frame update
    void Start()
    {
        switch (Base_script.level) //전투기 레벨별 능력치 
        {
            case 1:
                aircraft_speed = 200.0f;
                Aircraft_HP = 100;
                break;
            case 2:
                aircraft_speed = 250.0f;
                Aircraft_HP = 100;
                break;
            case 3:
                aircraft_speed = 300.0f;
                Aircraft_HP = 200;
                break;
        }

        fire_time = 0.0f;
        target = GameObject.Find("Base_attack_position");
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position); //베이스를 바라봄
        roket_fire_distanace = UnityEngine.Random.Range(500.0f, 800.0f); // 로켓발사거리 설정
    }

    // Update is called once per frame
    void Update()
    {
        fire_time += Time.deltaTime; //로켓발사 후 시간 (처음엔 계속증가)

        distance = Vector3.Distance(target.transform.position, this.transform.position); //베이스와 거리
        transform.Translate(Vector3.forward * aircraft_speed * Time.deltaTime);

        if (distance > 10000)
            Destroy(gameObject);

        if (Aircraft_HP <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(aircraft_destroy, this.transform.position);
            Destroy(this.gameObject);
        }

        if (roket_cnt >= 1 && distance < roket_fire_distanace)
        {
            if (fire_time >= max_time)
                attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet")) //총알 태그를 가진 물체에 맞으면 HP감소
            Aircraft_HP -= 100;
    }

    public void attack()
    {
        GameObject roket_spawn = transform.Find("Roket_fire_point").gameObject; //자식객체에서 찾음 
        GameObject roket = Instantiate(roket_prefab, roket_spawn.transform.position, roket_spawn.transform.rotation);
        roket_cnt--;
        fire_time = 0.0f;
        //Debug.Log(distance);
    }
}

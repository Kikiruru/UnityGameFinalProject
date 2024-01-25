using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_move : MonoBehaviour
{
    float speed; // 폭격기의 속도
    float distance;
    int Bomber_HP;
    int Bomb_cnt = 10;
    float Bomb_fire_distanace; // 폭탄 투하 지점 거리 
    float fire_time;
    float max_time = 0.05f;
    GameObject target; //Base
    public AudioClip bomber_destroy;
    public Transform explosion;
    public GameObject Bomb_prefab;
    // Start is called before the first frame update
    void Start()
    {
        switch (Base_script.level) //폭격기 레벨별 능력치 
        {
            case 1:
                speed = 100.0f;
                Bomber_HP = 300;
                break;
            case 2:
                speed = 150.0f;
                Bomber_HP = 400;
                break;
            case 3:
                speed = 200.0f;
                Bomber_HP = 500;
                break;
        }

        fire_time = 0.0f;
        target = GameObject.Find("Base_attack_position");
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position); //베이스를 바라봄
        Bomb_fire_distanace = UnityEngine.Random.Range(30.0f, 80.0f);
    }

    // Update is called once per frame
    void Update()
    {
        fire_time += Time.deltaTime; //폭탄투하 후 시간 (처음엔 계속증가)
        distance = Vector3.Distance(target.transform.position, this.transform.position); //베이스와 거리
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed)
        //Debug.Log("HP : "+ Bomber_HP);
        if (distance > 8000)
            Destroy(gameObject);
        if (Bomber_HP <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(bomber_destroy, this.transform.position);
            Destroy(this.gameObject);
        }

        if (Bomb_cnt >= 1 && distance < Bomb_fire_distanace)
        {
            if (fire_time >= max_time)
                attack();
        }

        //Debug.Log("firetime : "+ fire_time);
        //Debug.Log("Bomb_cnt : "+ Bomb_cnt);
        //Debug.Log("distance : "+distance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet")) //총알 태그를 가진 물체에 맞으면 HP감소
            Bomber_HP -= 100;
    }

    public void attack()
    {
        GameObject Bomb_spawn = transform.Find("Bomb_spawn_point").gameObject; //자식객체에서 찾음 
        GameObject bomb = Instantiate(Bomb_prefab, Bomb_spawn.transform.position, Bomb_spawn.transform.rotation);
        Bomb_cnt--;
        fire_time = 0.0f;
        //Debug.Log(distance);
    }
}

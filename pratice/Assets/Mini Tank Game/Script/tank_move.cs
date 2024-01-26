using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_move : MonoBehaviour
{
    private float tank_speed = 5.0f;
    private float rot_speed = 120.0f;
    public float bullet_power = 600.0f;
    public GameObject turret; // inspector에서 포탑(turret) 연결
    public Transform bullet;
    public GameObject barrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance_per_frame = tank_speed * Time.deltaTime;
        float degrees_per_frame = rot_speed * Time.deltaTime;

        float moving_velocity = Input.GetAxis("Vertical");
        float tank_angle = Input.GetAxis("Horizontal");
        float turret_angle = Input.GetAxis("TurretRotation");

        this.transform.Translate(Vector3.forward * moving_velocity * distance_per_frame); //탱크의 전후 이동 
        this.transform.Rotate(0.0f, tank_angle * degrees_per_frame, 0.0f); //탱크의 회전
        turret.transform.Rotate(Vector3.up * turret_angle * degrees_per_frame * 0.5f); //포탑의 회전

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject spawn_point = GameObject.Find("sp_bullet");
            Transform prefab_bullet = Instantiate(bullet, spawn_point.transform.position, spawn_point.transform.rotation);
            prefab_bullet.GetComponent<Rigidbody>().AddForce(barrel.transform.up * bullet_power); //up y축 방향
            // 포신이 향하는 방향으로 발사 각도 수정
        }
    }
}

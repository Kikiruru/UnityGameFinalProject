using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    //Turret_move
    GameObject child;
    public Transform bullet;
    public AudioClip reload_sound;
    float turret_row_speed = 100.0f; // 포탑 회전 속도
    float movement_row; // 포탑 회전 방향
    float gun_col_speed = 100.0f; // 포 고각 속도
    float movement_col; // 고각 회전 방향
    float bullet_power = 1500.0f; // 총알 속도
    float fire_time = 0.0f; // 발사 후 시간 
    float fire_interval = 0.125f; //총알 발사 간격
    public static float reload_now = 0.0f; //현재 장전 시간 
    float reload_time = 5.0f; //장전 소요 시간
    bool is_reload = false; // 재장전중인지 확인
    public static int bullet_total = 600; //총 장탄수 
    public static int bullet_cnt = 0; // 발사한 총알

    // Start is called before the first frame update
    void Start()
    {
        child = GameObject.Find("Turret_child");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(child.transform.eulerAngles.x);
        //Debug.Log(movement_col);
        // 포탑 회전 
        if (Input.GetButton("Horizontal"))
        {
            movement_row = Input.GetAxisRaw("Horizontal");
            transform.Rotate(Vector3.up, movement_row * turret_row_speed * Time.deltaTime);
        }
        //조준속도 
        if (Input.GetKey(KeyCode.Space)) //정밀한 조준 
        {
            turret_row_speed = 10.0f;
            gun_col_speed = 10.0f;
        }
        else
        {
            turret_row_speed = 100.0f;
            gun_col_speed = 100.0f;
        }
        //포 구동 범위 설정 
        if (Input.GetButton("Vertical"))
        {
            movement_col = Input.GetAxisRaw("Vertical");
            // 주포의 고각 80도에서 -15도 까지 360도 기준 280도 ~ 15도
            if (child.transform.eulerAngles.x < 15 || child.transform.eulerAngles.x > 280)
            {
                if(movement_col > 0)
                    child.transform.Rotate(Vector3.left, movement_col * gun_col_speed * Time.deltaTime);
                else if(movement_col < 0)
                    child.transform.Rotate(Vector3.left, movement_col * gun_col_speed * Time.deltaTime);
            }
            // 280도보다 작아지면(주포가 80도 이상 올라가면) 올라가는건 불가능
            // 15도보다 커지면 (주포가 -15도 이하 내려가면) 내려가는건 불가능 
            else if (child.transform.eulerAngles.x >= 15 && child.transform.eulerAngles.x < 30 && movement_col > 0)
                child.transform.Rotate(Vector3.left, movement_col * gun_col_speed * Time.deltaTime);
            else if (child.transform.eulerAngles.x <= 280 && child.transform.eulerAngles.x > 260 && movement_col < 0)
                child.transform.Rotate(Vector3.left, movement_col * gun_col_speed * Time.deltaTime);
        }
        //주포 연사속도
        if (Input.GetMouseButton(0) && !is_reload)
        {
            // 분당 600발 -> 초당 10발 -> 각 주포당 초당 10발 발사
            Fire();
            if (fire_time < fire_interval)
                fire_time += Time.deltaTime;
        }
        //재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            is_reload = true;
            AudioSource.PlayClipAtPoint(reload_sound, this.transform.position);
        }

        if (is_reload)
        {
            reload_now += Time.deltaTime;
            if (reload_now > reload_time)
                Reload();
        }
    }

    private void Fire()
    {
        if (fire_time < fire_interval) return;
        if (bullet_cnt < bullet_total)
        {
            bullet_cnt += 2;
            GameObject spawn_left = GameObject.Find("bullet_spawn_left");
            GameObject spawn_right = GameObject.Find("bullet_spawn_right");
            Transform prefab_bullet = Instantiate(bullet, spawn_left.transform.position, spawn_left.transform.rotation);
            Transform prefab_bullet2 = Instantiate(bullet, spawn_right.transform.position, spawn_right.transform.rotation);
            prefab_bullet.GetComponent<Rigidbody>().AddForce(spawn_left.transform.forward * bullet_power);
            prefab_bullet2.GetComponent<Rigidbody>().AddForce(spawn_right.transform.forward * bullet_power);
        }
        //Debug.Log(bullet_cnt);
        fire_time = 0.0f;
    }

    private void Reload()
    {
        if (reload_now < reload_time) return;
        else
        {
            bullet_cnt = 0;
            is_reload = false;
            reload_now = 0.0f;
        }
    }
}
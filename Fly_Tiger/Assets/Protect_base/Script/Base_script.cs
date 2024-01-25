using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base_script : MonoBehaviour
{
    public float timer = 0.0f;
    public static int level = 1;
    public GameObject bomber_prepab;
    public GameObject aircraft_prepab;
    public GameObject base_effect_prepab;

    int base_hp = 1500; //기지 체력
    float Bomber_Spawn_Time = 0.0f; //폭격기 스폰시간
    float Bomber_wait_time = 30.0f; //폭격기 스폰대기 시간
    int Bomber_num = 2; //폭격기 멤버 수 (+2 추가)
    float Aircraft_Spawn_Time = 0.0f; //전투기 스폰시간
    float Aircraft_wait_time = 60.0f; //전투기 스폰대기 시간
    int Aircraft_num = 0; //전투기 멤버 수(1대 스폰)
    bool effect_spawn = false;

    private GUIStyle white_style = new GUIStyle();
    string remain_time;
    string remain_bullet;
    string reload_bullet_time;
    string base_hp_text;
    // Start is called before the first frame update
    void Start()
    {
        Bomber_spawn(Bomber_num);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Bomber_Spawn_Time += Time.deltaTime;
        Aircraft_Spawn_Time += Time.deltaTime;

        if (timer > 100.0f && timer <= 200.0f) //시간별 레벨
            level = 2;
        else if (timer > 200.0f)
            level = 3;

        switch (level) //레벨별 스폰옵션
        {
            case 1: break;
            case 2:
                Bomber_num = 3;
                Bomber_wait_time = 20.0f;
                Aircraft_wait_time = 50.0f;
                Aircraft_num = 1;
                break;
            case 3:
                Bomber_num = 4;
                Aircraft_num = 2;
                Bomber_wait_time = 15.0f;
                Aircraft_wait_time = 40.0f;
                break;
        }

        if (Bomber_Spawn_Time > Bomber_wait_time) //30초마다 폭격기 스폰 (무작위 위치에서)
        {
            Bomber_spawn(Bomber_num);
            Bomber_Spawn_Time = 0.0f;
        }

        if (Aircraft_Spawn_Time > Aircraft_wait_time) //60초마다 전투기 스폰 (무작위 위치에서)
        {
            Aircraft_spawn(Aircraft_num);
            Aircraft_Spawn_Time = 0.0f;
        }

        if (base_hp < 800 && !effect_spawn) //기지 손상 효과
        {
            GameObject point1 = transform.Find("point1").gameObject;
            GameObject point2 = transform.Find("point2").gameObject;
            GameObject point3 = transform.Find("point3").gameObject;
            GameObject point4 = transform.Find("point4").gameObject;

            Instantiate(base_effect_prepab, point1.transform.position, point1.transform.rotation);
            Instantiate(base_effect_prepab, point2.transform.position, point1.transform.rotation);
            Instantiate(base_effect_prepab, point3.transform.position, point1.transform.rotation);
            Instantiate(base_effect_prepab, point4.transform.position, point1.transform.rotation);

            effect_spawn = true; //한번만 스폰
        }

        if (base_hp <= 0) // 게임 승리 조건
            SceneManager.LoadScene("Fail");
        if (timer >= 300.0f)
            SceneManager.LoadScene("Win");

        Debug.Log("level : "+ level);
        //Debug.Log("hp : " + base_hp);
    }

    void Bomber_spawn(int num) //폭격기 스폰(무리지어 생성될 숫자)
    {
        Vector3 pos = Spawn_point(4000.0f, 5000.0f);
        Instantiate(bomber_prepab, pos, bomber_prepab.transform.rotation);
        while(num != 0)
        {
            pos.x += Random.Range(-500.0f, 500.0f);
            pos.z += Random.Range(-500.0f, 500.0f);
            pos.y += Random.Range(-100.0f, 100.0f);
            Instantiate(bomber_prepab, pos, bomber_prepab.transform.rotation);
            num--;
        }
    }

    void Aircraft_spawn(int num) //전투기 스폰
    {
        Vector3 pos = Spawn_point(8000.0f, 9000.0f);
        Instantiate(aircraft_prepab, pos, aircraft_prepab.transform.rotation);
        while (num != 0)
        {
            pos.x += Random.Range(-500.0f, 500.0f);
            pos.z += Random.Range(-500.0f, 500.0f);
            pos.y += Random.Range(-100.0f, 100.0f);
            Instantiate(aircraft_prepab, pos, aircraft_prepab.transform.rotation);
            num--;
        }
    }

    Vector3 Spawn_point(float x1, float z1) //도넛모양의 스폰지역
    {//기지로부터 일정한 거리에 떨어진 지점의 위치값 반환 (기지의 위치 0,0,0)
        float r = Random.Range(x1, z1); //반지름
        float x = Random.Range(-r, r); //범위의 최솟값
        float temp = Mathf.Pow(r, 2) - Mathf.Pow(x, 2); // x^2 + y^2 = r^2에서 y^2 = r^2 - x^2
        float z = Mathf.Sqrt(temp); //(x, z)
        float negative_number = Random.Range(-1.0f, 1.0f);
        if (negative_number < 0)
            z *= -1;
        float y = Random.Range(400.0f, 1000.0f); //높이 400 ~ 1000에서 스폰
        return new Vector3(x, y, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bomb"))
            base_hp -= 10;
        if (collision.collider.CompareTag("Roket"))
            base_hp -= 50;
    }

    private void OnGUI()
    {
        white_style.fontSize = 20;
        white_style.normal.textColor = Color.white;
        remain_time = (string.Format("{1}:{0,2:D2}", (int)(300 - timer) % 60, (int)(300 - timer) / 60)).ToString();
        base_hp_text = (string.Format("{0,4:D4}", base_hp)).ToString();
        reload_bullet_time = (string.Format("{0:N2}", 5 - Camera_move.reload_now)).ToString();
        remain_bullet = (string.Format("{0,3:D3}/{1}", Camera_move.bullet_total - Camera_move.bullet_cnt, Camera_move.bullet_total)).ToString();
        GUI.Label(new Rect(Screen.width / 2, 30, 0, 0), "남은시간 : " + remain_time, white_style);
        GUI.Label(new Rect(Screen.width / 2, 60, 0, 0), "Base HP : " + base_hp_text, white_style);
        GUI.Label(new Rect(Screen.width / 2 + 200, 60, 0, 0), "Reload : "+ reload_bullet_time, white_style);
        GUI.Label(new Rect(Screen.width / 2 + 200, 30, 0, 0), "Ammo " + remain_bullet, white_style);
    }
}

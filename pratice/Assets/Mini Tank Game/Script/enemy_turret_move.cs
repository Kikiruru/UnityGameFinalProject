using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_turret_move : MonoBehaviour
{
    [SerializeField]
    private float power = 1200.0f;
    private float elqpsed_time = 0.0f;
    public float fire_interval = 2.0f;
    public Transform bullet; // 포탄prefab
    public Transform target; // 아군 탱크
    public Transform sp_point_enemy; //적군 탱크의 spawn point

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target); //적군이 항상 아군 탱크를 주시     
        elqpsed_time += Time.deltaTime;

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(sp_point_enemy.transform.position, fwd * 5, Color.green);
        if (Physics.Raycast(sp_point_enemy.transform.position, fwd, out hit, 5) == false
            || hit.collider.gameObject.tag != "Tank"
            || elqpsed_time < fire_interval)
            return;
        Debug.Log(hit.collider.gameObject.name);
        //                      반직선의 시작점	                반직선의 방향     반직선의 길이 
        // 반직선(Ray)을 쏴서 부딪힌 물체가 있을 경우 true 반환

        Transform enemy_bullet = Instantiate(bullet, sp_point_enemy.transform.position, Quaternion.identity);
        // Quaternaion.identity : 회전 초기값 
        enemy_bullet.GetComponent<Rigidbody>().AddForce(fwd * power);
        elqpsed_time = 0.0f;
    }
}

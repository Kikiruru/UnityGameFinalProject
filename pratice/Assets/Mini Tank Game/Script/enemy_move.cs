using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    public float rot_angle = 15.0f;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();

        /*
        float current_angle = rot_angle * Time.deltaTime;

        this.transform.RotateAround(Vector3.zero, Vector3.up, current_angle);
        //transform.RotateAround(<중심점>, <회전축>, <회전각도/초>)
        //자신의 위치와 중심점 간의 거리를 반지름으로 회전
        */
    }

    void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
    }
}

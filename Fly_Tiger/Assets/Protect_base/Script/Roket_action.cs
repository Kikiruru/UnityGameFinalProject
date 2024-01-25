using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket_action : MonoBehaviour
{
    float speed; //로켓 속력 = 전투기 속력
    float accel = 20.0f; //로켓 가속력
    public GameObject target;
    public AudioClip collision_sound;
    public Transform explosion_effect;
    // Start is called before the first frame update
    void Start()
    {
        speed = Aircraft_move.aircraft_speed + accel;
        target = GameObject.Find("Base_attack_ground_position"); //베이스 위치
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position); //베이스를 바라봄
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //베이스를 향해 날아감
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Aircraft") && !other.collider.CompareTag("Bomb") && !other.collider.CompareTag("Roket"))
        {
            Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(collision_sound, this.transform.position); // 사운드, 사운드 위치
            Destroy(this.gameObject);
        }
    }
}

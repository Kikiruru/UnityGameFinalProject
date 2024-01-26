using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_action : MonoBehaviour
{
    public AudioClip collision_sound; // inspector에서 충돌에 사용할 사운드 파일 연결
    public Transform explosion_effect;

    void OnTriggerEnter(Collider other) // other : 포탄과 부딪친 다른 물체 
    {
        Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
        AudioSource.PlayClipAtPoint(collision_sound, this.transform.position); // 사운드, 사운드 위치
        Destroy(this.gameObject);

        if (other.gameObject.tag == "Obstacle")
            Destroy(other.gameObject);
        else if (other.gameObject.tag == "Enemy")
        {
            score_record.win++;
            if (score_record.win > 5)
                Destroy(other.transform.root.gameObject);
        }
    }
}

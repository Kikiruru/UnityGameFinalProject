using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_action : MonoBehaviour
{
    public AudioClip collision_sound; // inspector에서 충돌에 사용할 사운드 파일 연결
    public AudioClip fire_sound;
    public Transform explosion_effect;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(fire_sound, this.transform.position);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 8.0f) // 발사 8초가 지나면 폭발하면서 총알 삭제
        {
            Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(collision_sound, this.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
        AudioSource.PlayClipAtPoint(collision_sound, this.transform.position); // 사운드, 사운드 위치
        Destroy(this.gameObject);
    }
}

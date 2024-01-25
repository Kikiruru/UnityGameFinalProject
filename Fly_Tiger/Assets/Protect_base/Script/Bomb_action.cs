using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_action : MonoBehaviour
{
    public AudioClip collision_sound; // inspector에서 충돌에 사용할 사운드 파일 연결
    public Transform explosion_effect;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!other.collider.CompareTag("Aircraft") && !other.collider.CompareTag("Bomb") && !other.collider.CompareTag("Roket"))
        {
            Instantiate(explosion_effect, this.transform.position, this.transform.rotation);
            AudioSource.PlayClipAtPoint(collision_sound, this.transform.position); // 사운드, 사운드 위치
            Destroy(this.gameObject);
        }
    }
}

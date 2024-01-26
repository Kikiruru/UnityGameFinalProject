using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_move : MonoBehaviour
{
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f) * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.Translate(new Vector3(0.0f, 0.0f, -2.0f) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.Translate(new Vector3(-2.0f, 0.0f, 0.0f) * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 launch_direction = new Vector3(10, 500, 300);
        if(collision.gameObject.name == "Sphere")
            ball.GetComponent<Rigidbody>().AddForce(launch_direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float jump_power;
    private float timer = 0;
    private string text = null;
    private GUIStyle guistyle = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        jump_power = Random.Range(5.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
            GetComponent<Rigidbody>().velocity = new Vector3(0, jump_power, 0);
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("Mini Jumping Game");
    }

    private void OnGUI()
    {
        guistyle.fontSize = 40;
        text = ((int)timer).ToString();
        GUI.Label(new Rect(Screen.width/2, 40, 256, 64), text, guistyle);
        GUI.Label(new Rect(0, 40, 256, 64), "Jump Power = "+ jump_power, guistyle);
    }
}

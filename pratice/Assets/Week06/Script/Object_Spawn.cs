using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Object_Spawn : MonoBehaviour
{
    public GameObject prefab = null;
    public Texture2D icon = null;
    private int count = 0;
    private GUIStyle guistyle = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab).transform.position = new Vector3(0, Random.Range(10.0f, 15.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 10)
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void OnCollisionEnter(Collision collision)
    {
        count++;
    }

    void OnGUI()
    {
        guistyle.fontSize = 40;
        guistyle.normal.textColor = Color.white;
        GUI.DrawTexture(new Rect(0, Screen.height - 128, 128, 128), icon);
        GUI.Label(new Rect(130, Screen.height-40, 256, 64), count.ToString(), guistyle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_clear : MonoBehaviour
{
    GUIStyle mission_clear = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene("Protect_base");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
# endif
        }

    }

    private void OnGUI()
    {
        mission_clear.fontSize = 100;
        mission_clear.normal.textColor = Color.yellow;

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, 0, 0), "MISSION CLEAR!!!\n\nRestart press F5\nQuit press ESC", mission_clear);
    }
}

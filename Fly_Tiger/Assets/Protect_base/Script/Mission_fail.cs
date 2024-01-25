using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_fail : MonoBehaviour
{
    GUIStyle mission_fail = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("Protect_base");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
# if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
# endif
        }
    }

    private void OnGUI()
    {
        mission_fail.fontSize = 100;
        mission_fail.normal.textColor = Color.red;

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, 0, 0), "MISSION FAIL...\n\nRestart press F5\nQuit press ESC", mission_fail);
    }
}

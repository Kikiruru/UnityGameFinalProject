using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongi_prefab;
    public static int score = 0;
    int cnt = 0;
    private string text = null;
    private string text2 = null;
    private GUIStyle guistyle = new GUIStyle();
    private GUIStyle guistyle2 = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cnt < 5)
        {
            GameObject bamsongi = Instantiate(bamsongi_prefab) as GameObject;
            Ray screen_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 shooting_ray = screen_ray.direction;
            bamsongi.GetComponent<BamsongiCtrl>().Shoot(shooting_ray * 1000);
            cnt++;
        }

        if(cnt >= 5)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Mini Dart Game");
                score = 0;
            }
        }
    }

    private void OnGUI()
    {
        guistyle.fontSize = 40;
        text = (string.Format("{0:0.#}", BamsongiCtrl.row_wind)).ToString();
        text2 = (string.Format("{0:0.#}", BamsongiCtrl.col_wind)).ToString();
        GUI.Label(new Rect(0, 0, 256, 64), "(" + text + ", " + text2 + ", 0.0)", guistyle);
        GUI.Label(new Rect(0, 40, 256, 64), "시도 횟수 = " + cnt, guistyle);
        GUI.Label(new Rect(0, 80, 256, 64), "점수 = " + score, guistyle);
        if (cnt >= 5)
            guistyle2.fontSize = 80;
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, 256, 64), "Press 'R' to Continue...", guistyle2);
    }
}

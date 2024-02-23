using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public LevelParser levelParser;
    private DateTime startTime;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        DateTime timeNow = DateTime.Now;
        TimeSpan interval = timeNow - startTime;
        int time = 400 - (int)((interval.TotalSeconds) * 2.5f);
        string timeStr = $"Time \n {time}";
        timerText.text = timeStr;
        if (time == 0)
        {
            levelParser.ReloadLevel();
            startTime = DateTime.Now;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("mouse clicked");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Debug.DrawRay(mainCamera.transform.position, Input.mousePosition * 10, Color.blue);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log($"{hitObject} hit");
                if (hitObject.CompareTag("Brick"))
                {
                    Destroy(hitObject);
                }
            }
        }
    }
}

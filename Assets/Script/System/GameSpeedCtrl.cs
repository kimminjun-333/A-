using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSpeedCtrl : SingletonManager<GameSpeedCtrl>
{
    internal float speed = 1f;
    public TMP_Text text;

    private void Start()
    {
        Time.timeScale = 1f;
        speed = Time.timeScale;
    }

    private void Update()
    {
        text.text = "X" + speed;
    }

    public void SpeedUp()
    {
        if(speed == 1f)
        {
            speed = 2f;
        }
        else if(speed == 2f)
        {
            speed = 3f;
        }
        else if (speed == 3f)
        {
            speed = 1f;
        }
        else
        {
            Time.timeScale = speed;
        }
        Time.timeScale = speed;
    }
}

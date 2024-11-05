using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneMove : MonoBehaviour
{
    private float movespeed = 0.4f;



    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(new Vector2(transform.position.x, transform.position.y + 0.2f));
            yield return new WaitForSeconds(movespeed);
            transform.Translate(new Vector2(transform.position.x, transform.position.y - 0.4f));
            yield return new WaitForSeconds(movespeed);
        }
    }

    private void OnEnable()
    {
        if (GameSpeedCtrl.Instance != null)
        {
            GameSpeedCtrl.Instance.speed = 1f;
        }
        Time.timeScale = 1f;
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopCoroutine(Move());
    }

}

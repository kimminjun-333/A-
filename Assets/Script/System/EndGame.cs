using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public Button reset;
    public Button Exit;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void Resetgame()
    {
        SceneManager.LoadScene("StartScene");
        Destroy(GameManager.Instance.gameObject);
    }

    public void Exitgame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

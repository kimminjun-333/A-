using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public GameObject pauseimage;
    public Button Restartbutton;
    public Button Mainmenu;
    public Button Exitbutton;
    private bool pauseon = false;

    private void Start()
    {
        pauseimage.SetActive(false);
    }

    private void Update()
    {
        if (pauseon == true)
        {
            Time.timeScale = 0f;
            pauseimage.SetActive(true);
        }
        if (pauseon == false)
        {
            Time.timeScale = 1f;
            pauseimage.SetActive(false);
        }
    }

    public void Pause()
    {
        if(pauseon == false)
        {
            pauseon = true;
        }
        else if(pauseon == true)
        {
            pauseon = false;
        }
    }

    public void RestartGame()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}

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
    public Sprite pausesprite;
    private Sprite Wsprite;

    private void Start()
    {
        pauseimage.SetActive(false);
        Wsprite = gameObject.GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(pauseon == false)
        {
            pauseon = true;
            Time.timeScale = 0f;
            gameObject.GetComponent<Image>().sprite = pausesprite;
            pauseimage.SetActive(true);
            
        }
        else if(pauseon == true)
        {
            pauseon = false;
            Time.timeScale = GameSpeedCtrl.Instance.speed;
            gameObject.GetComponent<Image>().sprite = Wsprite;
            pauseimage.SetActive(false);
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

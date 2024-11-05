using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSkillButton : MonoBehaviour
{

    public TMP_Text text;
    private Button button;
    private Image image;
    private Color thiscolor;
    private bool buttonon = false;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        thiscolor = image.color;
    }
    Coroutine coroutine;
    private void Update()
    {
        if (button.interactable == false)
        {
            image.color = Color.gray;

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
                buttonon = false;
            }

        }
        if (button.interactable == true)
        {
            image.color = thiscolor;
            if(buttonon == false)
            {
                if (coroutine == null)
                {
                    coroutine = StartCoroutine(ColorC());
                }
                buttonon = true;
            }
        }
    }

    private IEnumerator ColorC()
    {
        while (true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            image.color = thiscolor;
            yield return new WaitForSeconds(0.5f);
        }
    }

}

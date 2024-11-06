using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BottonClass : MonoBehaviour
{
    public Button Button;
    public Image image;
    public Image cooltimebar;
    public Image cooltimebarfraim;
    public int price;
    public DogSpawner spawner;
    public Dog DogPrefab;
    public TMP_Text text;
    public bool CoolTime;

    private void Start()
    {
        Button = this.GetComponent<Button>();
        price = DogPrefab.price;
        text.text = DogPrefab.price + "¿ø";
    }

    public void Spawn()
    {
        spawner.Gold -= price;
        float y = Random.Range(spawner.spawnPoint.x, spawner.spawnPoint.y);
        Dog dogs = Instantiate(DogPrefab, new Vector3(spawner.transform.position.x, y), Quaternion.identity);
        CoolTimeOn();
    }

    private void CoolTimeOn()
    {
        CoolTime = true;
        image.gameObject.SetActive(true);
        cooltimebarfraim.gameObject.SetActive(true);
        cooltimebar.gameObject.SetActive(true);
        Button.interactable = false;
        StartCoroutine(Cool());
    }

    private IEnumerator Cool()
    {
        float s = spawner.spawncooltime;
        while(s > 0f)
        {
            yield return new WaitForFixedUpdate();
            s -= Time.deltaTime;
            cooltimebar.fillAmount = (s / spawner.spawncooltime);
        }
        image.gameObject.SetActive(false);
        Button.interactable = true;
        cooltimebarfraim.gameObject.SetActive(false);
        cooltimebar.gameObject.SetActive(false);
        CoolTime = false;
    }


}

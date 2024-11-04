using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatSpawner : Spawner
{
    public float HP;
    private float maxhp;

    [Tooltip("스폰지점 y\nx : 최소값, y는 최대값")]
    public Vector2 spawnPoint;

    public Cat[] catprefab;

    public TMP_Text hptext;

    private void Start()
    {
        maxhp = HP;
        StartCoroutine(SpawnTime(catprefab[0], catprefab[0].delay, catprefab[0].time));
        StartCoroutine(SpawnTime(catprefab[1], catprefab[1].delay, catprefab[1].time));
        StartCoroutine(SpawnTime(catprefab[2], catprefab[2].delay, catprefab[2].time));

    }

    private void Update()
    {
        hptext.text = HP + " / " + maxhp;
    }

    public void clik()
    {
        spawn(catprefab[0]);
    }

    private IEnumerator SpawnTime(Cat catprefab, float delay, float time)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            spawn(catprefab);
            yield return new WaitForSeconds(time);
        }
    }

    public void spawn(Cat catprefab)
    {
        float y = Random.Range(spawnPoint.x, spawnPoint.y);
        Cat catss = Instantiate(catprefab, new Vector3(this.transform.position.x, y), Quaternion.identity);
        GameManager.Instance.cats.Add(catss);
    }

    internal void Die()
    {
        if (HP < 0)
        {
            GameManager.Instance.Win.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}

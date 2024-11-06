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
        for(int i = 0; i < catprefab.Length; i++)
        {
            StartCoroutine(SpawnTime(catprefab[i]));
        }
    }

    private void Update()
    {
        hptext.text = HP + " / " + maxhp;
    }

    private IEnumerator SpawnTime(Cat catprefab)
    {
        yield return new WaitForSeconds(catprefab.delay);
        while (true)
        {
            Spawn(catprefab);
            yield return new WaitForSeconds(catprefab.time);
        }
    }

    public void Spawn(Cat catprefab)
    {
        float y = Random.Range(spawnPoint.x, spawnPoint.y);
        Cat catss = Instantiate(catprefab, new Vector3(this.transform.position.x, y), Quaternion.identity);
    }

    internal void Die()
    {
        if (HP < 0)
        {
            GameManager.Instance.Win.gameObject.SetActive(true);
        }
    }
}

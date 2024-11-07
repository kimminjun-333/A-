using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cat : Unit, ITakeDamage
{
    
    [Tooltip("delay : 게임시작 후 스폰시작전 딜레이")]
    public float delay;
    [Tooltip("time : 스폰간격")]
    public float time;

    private Dog target;
    private DogSpawner spawnertarget;

    [Tooltip("이동 방향\nx : -1(왼쪽이동) / 1(오른쪽이동), y : 0")]
    public Vector2 movePos = new Vector2(0.15f, 0);

    private IEnumerator Start()
    {
        GameManager.Instance.cats.Add(this);

        yield return null;

        Wcolor = Renderer.material.color;

        Pos = movePos;

        maxhp = hp;

        overlapBox.size = new Vector2(range, 80);
        overlapBox.offset = new Vector2(range / 2, 0);
    }

    private void Update()
    {
        if(hp <= 0)
        {
            Die();
        }

        if (AllTargeting == false)
        {
            OneTarget();
        }
        if (AllTargeting == true)
        {
            AllTarget();
        }
        if (target == null && spawnertarget == null)
        {
            Move();
        }

        hpBar.fillAmount = hpAmount;
        text.text = name.ToString();
    }

    private void AllTarget()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + overlapBox.offset, overlapBox.size, 0);
        foreach (Collider2D coll in colls)
        {
            if (coll.CompareTag("Dog"))
            {
                target = coll.GetComponent<Dog>();
                if (AttCoolTime == false)
                {
                    Att(target);
                    onattAll = true;
                }
            }
            if (coll.CompareTag("Dogspawner"))
            {
                spawnertarget = coll.GetComponent<DogSpawner>();
                if (AttCoolTime == false)
                {
                    Att(spawnertarget);
                    onattAll = true;
                }
            }
        }
        if (onattAll == true)
        {
            AttCoolTime = true;
            Invoke("inv", AttSpeed);
            onattAll = false;
        }
    }

    private void OneTarget()
    {
        var colls = Physics2D.OverlapBoxAll((Vector2)transform.position + overlapBox.offset, overlapBox.size, 0);
        Dog targetenemy = null;
        if (target == null)
        {
            foreach (Collider2D coll in colls)
            {
                if (coll.CompareTag("Dog"))
                {
                    if (targetenemy == null || targetenemy.hp > coll.GetComponent<Dog>().hp)
                    {
                        targetenemy = coll.GetComponent<Dog>();
                    }
                }
                if (coll.CompareTag("Dogspawner"))
                {
                    spawnertarget = coll.GetComponent<DogSpawner>();
                }
            }
            target = targetenemy;
        }

        if (target != null)
        {
            if (AttCoolTime == false)
            {
                Att(target);
                AttCoolTime = true;
                Invoke("inv", AttSpeed);
            }
        }
        if (target == null && spawnertarget != null)
        {
            if (AttCoolTime == false)
            {
                Att(spawnertarget);
                AttCoolTime = true;
                Invoke("inv", AttSpeed);
            }
        }
    }

    public void TakeDamage(float enemydamage)
    {
        hp -= enemydamage;
        StartCoroutine(this.Hit());
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(die, new Vector2(transform.position.x,transform.position.y + 2.5f), Quaternion.identity);
        GameManager.Instance.cats.Remove(this);
        Destroy(gameObject);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public string name;

    public float moveSpeed;
    public float AttSpeed;
    public float range;
    public float damage;
    private float maxhp;
    public float hp;
    [Tooltip("runtime : 게임시작 후 스폰시작전 딜레이")]
    public float delay;
    [Tooltip("time : 스폰간격")]
    public float time;

    private bool AttCoolTime = false;
    public bool AllTargeting;

    public float hpAmount { get { return hp / maxhp; } }

    private Dog target;
    private DogSpawner spawnertarget;
    public Image hpBar;
    private Rigidbody2D rb;
    public BoxCollider2D overlapBox;
    public TMP_Text text;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        GameManager.Instance.cats.Add(this);

        yield return null;

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
    bool onatt;
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
                    onatt = true;
                }
            }
            if (coll.CompareTag("Dogspawner"))
            {
                spawnertarget = coll.GetComponent<DogSpawner>();
                if (AttCoolTime == false)
                {
                    Att(spawnertarget);
                    onatt = true;
                }
            }
        }
        if (onatt == true)
        {
            AttCoolTime = true;
            Invoke("inv", 1f);
            onatt = false;
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
                Invoke("inv", 1f);
            }
        }
        if (target == null && spawnertarget != null)
        {
            if (AttCoolTime == false)
            {
                Att(spawnertarget);
                Invoke("inv", 1f);
            }
        }
    }

    private void inv()
    {
        print("공격쿨타임종료");
        AttCoolTime = false;
    }


    private void Move()
    {
        Vector2 movePos = rb.position + (new Vector2(1, 0) * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }


    private void Att(Dog target)
    {
        AttCoolTime = true;
        print($"{name}의 공격");
        //공격
        target.TakeDamage(damage);
    }

    private void Att(DogSpawner dogspawner)
    {
        AttCoolTime = true;
        print($"{name}의 공격");
        //공격
        dogspawner.HP -= damage;
        if (dogspawner.HP <= 0)
        {
            dogspawner.Die();
        }
    }

    public void TakeDamage(float enemydamage)
    {
        hp -= enemydamage;
        if(hp <= 0)
        {
            Die();
        }
    }

   

    public void Die()
    {
        GameManager.Instance.cats.Remove(this);
        Destroy(gameObject);
    }

}

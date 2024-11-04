using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Dog : MonoBehaviour
{
    public string name;

    public float moveSpeed;
    public float AttSpeed;
    public float range;
    public float damage;
    private float maxhp;
    public float hp;
    public int price;
    public float colltime;

    private bool AttCoolTime = false;
    public bool AllTargeting;

    public float hpAmount { get { return hp / maxhp; } }
    public Image hpBar;
    

    private Cat target;
    private CatSpawner spawnertarget;
    private Rigidbody2D rb;
    public BoxCollider2D overlapBox;
    public TMP_Text text;
    public Renderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        GameManager.Instance.dogs.Add(this);

        yield return null;

        maxhp = hp;

        overlapBox.size = new Vector2(range, 80);
        overlapBox.offset = new Vector2(-(range / 2), 0);
    }

    private void Update()
    {
        if (hp <= 0)
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
            if (coll.CompareTag("Cat"))
            {
                target = coll.GetComponent<Cat>();
                if (AttCoolTime == false)
                {
                    Att(target);
                    onatt = true;
                }
            }
            if (coll.CompareTag("Catspawner"))
            {
                spawnertarget = coll.GetComponent<CatSpawner>();
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
        Cat targetenemy = null;
        if (target == null)
        {
            foreach (Collider2D coll in colls)
            {
                if (coll.CompareTag("Cat"))
                {
                    if (targetenemy == null || targetenemy.hp > coll.GetComponent<Cat>().hp)
                    {
                        targetenemy = coll.GetComponent<Cat>();
                    }
                }
                if (coll.CompareTag("Catspawner"))
                {
                    spawnertarget = coll.GetComponent<CatSpawner>();
                }
            }
            target = targetenemy;
        }

        if (target != null)
        {
            if (AttCoolTime == false)
            {
                AttCoolTime = true;
                Att(target);
                Invoke("inv", 1f);
            }
        }
        if (target == null && spawnertarget != null)
        {
            if (AttCoolTime == false)
            {
                AttCoolTime = true;
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
        Vector2 movePos = rb.position + (new Vector2(-1, 0) * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(movePos);
    }


    private void Att(Cat target)
    {
        print($"{name}의 공격");
        //공격
        target.TakeDamage(damage);
    }

    private void Att(CatSpawner catspawner)
    {
        print($"{name}의 공격");
        //공격
        catspawner.HP -= damage;
        if (catspawner.HP <= 0)
        {
            catspawner.Die();
        }
    }
    public void TakeDamage(float enemydamage)
    {
        hp -= enemydamage;
        if (hp <= 0)
        {
            Die();
        }
    }



    public void Die()
    {
        GameManager.Instance.dogs.Remove(this);
        Destroy(gameObject);
    }

}

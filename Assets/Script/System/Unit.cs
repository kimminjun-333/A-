using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public new string name;

    [Tooltip("이동속도")]
    public float moveSpeed;
    [Tooltip("공격속도")]
    public float AttSpeed;
    [Tooltip("사정거리")]
    public float range;
    [Tooltip("공격력")]
    public float damage;
    protected float maxhp;
    [Tooltip("체력")]
    public float hp;

    protected bool AttCoolTime = false;
    [Tooltip("전체공격(체크O)/단일공격(체크X)")]
    public bool AllTargeting;
    protected bool onattAll;
    public float hpAmount { get { return hp / maxhp; } }
    public Image hpBar;

    public BoxCollider2D overlapBox;
    public TMP_Text text;
    protected Color Wcolor;
    public Renderer Renderer;
    public GameObject die;
    protected Vector2 Pos;

    protected void inv()
    {
        AttCoolTime = false;
    }
    protected void Move()
    {
        transform.Translate(Pos * moveSpeed * Time.deltaTime);
    }

    protected void Att(ITakeDamage a)
    {
        a.TakeDamage(damage);
    }

    protected IEnumerator Hit()
    {
        Renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Renderer.material.color = Wcolor;
    }

}

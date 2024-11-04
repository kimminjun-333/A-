using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class DogSpawner : Spawner
{
    [Tooltip("�ִ뷹��")]
    public int LevelMax;
    private int Level;
    [Tooltip("�������� �ʿ��� ���")]
    public int[] levelupprice;
    [Tooltip("�������� �ڿ� ���Ѽ�")]
    public int[] levelupmaxgold;
    public float HP;
    private float maxhp;
    public int Gold;
    public int maxgold;

    public float skilldamage;
    public float skillCoolTime;

    [Tooltip("�������� y\nx : �ּҰ�, y�� �ִ밪")]
    public Vector2 spawnPoint;
    //public Button[] buttons;

    public Dog[] dogprefab;

    public Button LevelUpButton;
    public Button SkillButton;
    public BottonClass[] BottonClasses;

    public TMP_Text hptext;
    public TMP_Text goldtext;
    public BoxCollider2D skillbox;


    private void Start()
    {
        Level = 1;
        maxgold = levelupmaxgold[Level];
        maxhp = HP;
        for (int i = 0; i < BottonClasses.Length; i++)
        {
            BottonClasses[i].image.gameObject.SetActive(false);
            BottonClasses[i].cooltimebar.gameObject.SetActive(false);
            BottonClasses[i].cooltimebarfraim.gameObject.SetActive(false);
        }
        StartCoroutine(goldup());
        SkillOff();
        Invoke("SkillOn", skillCoolTime);

        GameManager.Instance.Win.gameObject.SetActive(false);
        GameManager.Instance.Lose.gameObject.SetActive(false);
    }

    private void Update()
    {
        Text();

        for (int i = 0; i < BottonClasses.Length; i++)
        {
            if (BottonClasses[i].price > Gold || BottonClasses[i].CoolTime == true)
            {                 
                BottonClasses[i].image.gameObject.SetActive(true);
                BottonClasses[i].Button.interactable = false;
            }
            else
            {
                BottonClasses[i].image.gameObject.SetActive(false);
                BottonClasses[i].Button.interactable = true;
            }
        }
        if(Gold >= levelupprice[Level])
        {
            LevelUpButton.GetComponent<LevelSkillButton>().hideimage.gameObject.SetActive(false);
            LevelUpButton.interactable = true;
        }
        if(Gold < levelupprice[Level])
        {
            LevelUpButton.GetComponent<LevelSkillButton>().hideimage.gameObject.SetActive(true);
            LevelUpButton.interactable = false;
        }

    }

    private void Text()
    {

        goldtext.text = Gold + " / " + maxgold + "��";
        hptext.text = HP + " / " + maxhp;
        if (Level < LevelMax)
        {
            LevelUpButton.GetComponent<LevelSkillButton>().text.text = "Lv." + Level + "\n"
                + "<size=50>���� ��</size>" + "\n" + levelupprice[Level] + "��";
        }
        else
        {
            LevelUpButton.GetComponent<LevelSkillButton>().text.text = "Lv." + Level + "\n"
               + "<size=50>���� ��</size>" + "\n" + "�ִ� ����";
        }

    }

    public void Skill()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + skillbox.offset, skillbox.size, 0);
        foreach (Collider2D coll in colls)
        {
            if(coll.CompareTag("Cat"))
            {
                coll.GetComponent<Cat>().TakeDamage(skilldamage);
            }
        }
        SkillOff();
        Invoke("SkillOn", skillCoolTime);
    }

    public void SkillOn()
    {
        SkillButton.GetComponent<LevelSkillButton>().hideimage.gameObject.SetActive(false);
        SkillButton.interactable = true;
    }

    public void SkillOff()
    {
        SkillButton.GetComponent<LevelSkillButton>().hideimage.gameObject.SetActive(true);
        SkillButton.interactable = false;
    }

    public void LeveUp()
    {
        if (Level < LevelMax)
        {
            Gold -= levelupprice[Level];
            Level++;
            maxgold = levelupmaxgold[Level];
        }
        if(Level >= LevelMax)
        {
            print("�ִ� ���� �Դϴ�");
        }
    }

    private IEnumerator goldup()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Gold < maxgold)
            {
                Gold += 6 + ((Level - 1) * 4);
                if (Gold > maxgold)
                {
                    Gold = maxgold;
                }
            }
        }
    }

    public void clik(Button a)
    {
        if (a == BottonClasses[0].Button)
        {
            BottonClasses[0].Spawn();
        }
        if (a == BottonClasses[1].Button)
        {
            BottonClasses[1].Spawn();
        }
        if (a == BottonClasses[2].Button)
        {
            BottonClasses[2].Spawn();
        }
        if (a == BottonClasses[3].Button)
        {
            BottonClasses[3].Spawn();
        }
        if (a == BottonClasses[4].Button)
        {
            BottonClasses[4].Spawn();
        }

    }

    internal void Die()
    {
        if (HP <= 0)
        {
            GameManager.Instance.Lose.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }


}

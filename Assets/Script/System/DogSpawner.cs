using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DogSpawner : MonoBehaviour, ITakeDamage
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
    [Tooltip("�ʴ� ������ �⺻���")]
    public int Upgold;
    [Tooltip("���� ���")]
    public int Gold;
    private int maxgold;
    private float goldDuration = 0.5f;

    [Tooltip("�ʻ�� ������")]
    public float skilldamage;
    [Tooltip("�ʻ�� ��Ÿ��")]
    public float skillCoolTime;

    [Tooltip("�������� y\nx : �ּҰ�, y�� �ִ밪")]
    public Vector2 spawnPoint;
    [Tooltip("���� ��Ÿ��")]
    public float spawncooltime;

    public Dog[] dogprefab;
    public GameObject skill;

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
        StartCoroutine(Goldup());
        SkillOff();
        Invoke("SkillOn", skillCoolTime);

        GameManager.Instance.Win.gameObject.SetActive(false);
        GameManager.Instance.Lose.gameObject.SetActive(false);
    }

    private void Update()
    {
        Text();

        Dogspawncooltimecheck();
        
        Levelupcheck();

        if(Input.GetKeyDown(KeyCode.C))
        {
            Gold += 100;
        }

    }

    private void Dogspawncooltimecheck()
    {
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
    }

    private void Levelupcheck()
    {
        if (Gold >= levelupprice[Level])
        {
            LevelUpButton.interactable = true;
        }
        if (Gold < levelupprice[Level])
        {
            LevelUpButton.interactable = false;
        }

        if (Level >= LevelMax)
        {
            if (LevelUpButton.GetComponent<LevelSkillButton>().maxlevel == false)
            {
                LevelUpButton.GetComponent<LevelSkillButton>().maxlevel = true;
            }
        }
    }

    private void Text()
    {
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

    private void Skill()
    {
        bool targeton = false;
        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + skillbox.offset, skillbox.size, 0);
        foreach (Collider2D coll in colls)
        {
            if(coll.CompareTag("Cat"))
            {
                Instantiate(skill, new Vector2(coll.transform.position.x, coll.transform.position.y + 20), Quaternion.identity);
                coll.GetComponent<Cat>().TakeDamage(skilldamage);
                if (targeton == false)
                {
                    targeton = true;
                }
            }
        }
        if (targeton == true)
        {
            SkillOff();
            Invoke("SkillOn", skillCoolTime);
        }
    }

    private void SkillOn()
    {
        SkillButton.interactable = true;
    }

    private void SkillOff()
    {
        SkillButton.interactable = false;
    }

    private void LeveUp()
    {
        if (Level < LevelMax)
        {
            Gold -= levelupprice[Level];
            Level++;
            maxgold = levelupmaxgold[Level];
        }
    }

    private IEnumerator Goldup()
    {
        while (true)
        {
            if (Gold < maxgold)
            {
                int plusgold = Upgold + ((Level - 1) * 4);
                StartCoroutine(DisplayGold(Gold));
                Gold += plusgold;
                if (Gold > maxgold)
                {
                    Gold = maxgold;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DisplayGold(int startgold)
    {
        float endtime = Time.time + goldDuration;

        while (Time.time < endtime)
        {
            goldtext.text = Mathf.Lerp(Gold, startgold, (endtime - Time.time)/goldDuration).ToString("n0") + " / " + maxgold + "��";

            yield return null;
        }
    }

    public void Clik(Button a)
    {
        for(int i = 0; i < BottonClasses.Length; i++)
        {
            if (a == BottonClasses[i].Button)
            {
                BottonClasses[i].Spawn();
            }
        }
    }

    public void TakeDamage(float enemydamage)
    {
        HP -= enemydamage;
        
        if (HP <= 0)
        {
            Die();
        }
    }

    internal void Die()
    {
        if (HP <= 0)
        {
            GameManager.Instance.Lose.gameObject.SetActive(true);
        }
    }


}

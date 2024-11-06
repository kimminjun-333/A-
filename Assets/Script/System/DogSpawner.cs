using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DogSpawner : Spawner
{
    [Tooltip("최대레벨")]
    public int LevelMax;
    private int Level;
    [Tooltip("레벨업에 필요한 비용")]
    public int[] levelupprice;
    [Tooltip("레벨업시 자원 상한선")]
    public int[] levelupmaxgold;
    public float HP;
    private float maxhp;
    [Tooltip("초당 오르는 기본골드")]
    public int Upgold;
    [Tooltip("현재 골드")]
    public int Gold;
    private int maxgold;
    private float goldDuration = 0.5f;

    [Tooltip("필살기 데미지")]
    public float skilldamage;
    [Tooltip("필살기 쿨타임")]
    public float skillCoolTime;

    [Tooltip("스폰지점 y\nx : 최소값, y는 최대값")]
    public Vector2 spawnPoint;
    [Tooltip("스폰 쿨타임")]
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
                + "<size=50>레벨 업</size>" + "\n" + levelupprice[Level] + "원";
        }
        else
        {
            LevelUpButton.GetComponent<LevelSkillButton>().text.text = "Lv." + Level + "\n"
               + "<size=50>레벨 업</size>" + "\n" + "최대 레벨";
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
            yield return new WaitForSeconds(1f);
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
        }
    }

    private IEnumerator DisplayGold(int startgold)
    {
        float endtime = Time.time + goldDuration;

        while (Time.time < endtime)
        {
            goldtext.text = Mathf.Lerp(Gold, startgold, (endtime - Time.time)/goldDuration).ToString("n0") + " / " + maxgold + "원";

            yield return null;
        }
    }

    public void Clik(Button a)
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
        }
    }


}

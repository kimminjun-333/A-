using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float time = 1f;
    Vector2 dir = new Vector2(0, -10);

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * 5f);
        
    }


}

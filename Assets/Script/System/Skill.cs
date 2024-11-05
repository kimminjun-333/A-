using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float time = 0.5f;
    Vector2 dir = new Vector2(0, -9);

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * 5f);
        
    }


}

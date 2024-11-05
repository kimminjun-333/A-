using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    float time = 0;
    public float fadetime = 2f;

    Vector2 dir = new Vector2(0, 10);


    private void Update()
    {
        transform.Translate(dir * Time.deltaTime);

        if(time < fadetime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f - time / fadetime);
        }
        else
        {
            Destroy(this.gameObject);
        }
        time += Time.deltaTime;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fades : MonoBehaviour
{
    public float coroutineTime = 0.1f, speed;
    private SpriteRenderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        for (float alpha = 1; alpha > 0; alpha -= speed * Time.deltaTime)
        {
            Color newColor = rend.color;
            newColor.a = alpha;
            rend.color = newColor;

            yield return new WaitForSeconds(coroutineTime);
        }
    }
}

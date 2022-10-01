using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollection : MonoBehaviour
{
    public float wait = 10;
    public IEnumerator cycle()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(wait);
        gameObject.GetComponent<BoxCollider2D>().enabled = true; 
        gameObject.GetComponent<SpriteRenderer>().enabled = true; 
    }
}

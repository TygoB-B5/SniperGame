using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public void Die()
    {
        Destroy(gameObject, 0);
    }
    void Update()
    {
        transform.Translate(Vector3.right * 10 * Time.deltaTime);
        Destroy(gameObject, 10);
    }
}

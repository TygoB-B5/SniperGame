using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    public GameObject enemy;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        Vector3 randPos = new Vector3(-25, 0.5f, Random.Range(25, 280));
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        Instantiate(enemy, randPos, rot);
        yield return new WaitForSeconds(2);
        StartCoroutine("Spawn");
        yield return null;
    }
}

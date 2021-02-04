using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treeplacer : MonoBehaviour
{
    public GameObject[] trees;
    public int maxTrees;

    void Start()
    {
        for(int i = 0; i < maxTrees / 2; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-9, -50), 0, Random.Range(-1, 300));
            Quaternion randRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(trees[Random.Range(0, trees.Length)], randPos, randRot);
        }

        for (int i = 0; i < maxTrees / 2; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(9, 50), 0, Random.Range(-1, 300));
            Quaternion randRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(trees[Random.Range(0, trees.Length)], randPos, randRot);
        }
    }
}

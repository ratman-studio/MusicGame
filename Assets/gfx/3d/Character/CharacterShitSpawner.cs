using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShitSpawner : MonoBehaviour
{
    private GameObject ShitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ShitPrefab,transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collectible : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

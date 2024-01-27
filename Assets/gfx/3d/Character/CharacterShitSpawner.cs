using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ShitPrefab;
    // Start is called before the first frame update
    private GroundedCharacterController _characterController;

    void Start()
    {
        _characterController = FindObjectOfType<GroundedCharacterController>();
        _characterController.OnShit += () => { Instantiate(ShitPrefab, transform.parent);};

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

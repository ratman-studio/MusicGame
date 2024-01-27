using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private StudioGlobalParameterTrigger PlayerSpeed;
    [SerializeField] private StudioGlobalParameterTrigger Mistake;

    [SerializeField] private GroundedCharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        InSceneLevelSwitcher.OnLevelStart += StartMusic;
    }

    private void OnDisable()
    {
        InSceneLevelSwitcher.OnLevelStart -= StartMusic;
    }

    private void StartMusic()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BoostLevel(float boostLevel)
    {
        throw new System.NotImplementedException();
    }
}

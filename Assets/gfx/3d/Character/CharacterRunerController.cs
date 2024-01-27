using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeedMaxController : MonoBehaviour
{
    [SerializeField] private GroundedCharacterController _characterController;
    [SerializeField] private List<float> MaxSpeed = new List<float>();

    private float boostLevel = 0;

    private float boostDecreaseRatio = 1f;

    // boost levels 0-5 , 5-10, over 10
    private float MaxBoostLevel = 15;

    void ItemCollected(float boost)
    {
        boostLevel += boost;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var time = Time.deltaTime;
        DecreaseBoostLevel(time);
    }

    private void DecreaseBoostLevel(float time)
    {
        boostLevel -= boostDecreaseRatio * time;
        OnBoostLevelChanged();
    }

    private void AddBoostLevel(float amount)
    {
    }

    private void OnBoostLevelChanged()
    {
        // clamp boost
        boostLevel = Mathf.Clamp(boostLevel, 0, MaxBoostLevel);
        HudSystem.Get().topHud.UpdateBoostLevel(boostLevel, MaxBoostLevel);
        // update max speed
        RefreshMaxSpeed();
        // _characterController.SetWalkForce(currentMaxSpeed);
        HudSystem.Get().topHud.UpdateSpeedLevel(_characterController.GetCurrentVelocity(), MaxSpeed[2]);
        // update boost hud
    }

    private float currentMaxSpeed;
    private void RefreshMaxSpeed()
    {

        currentMaxSpeed = MaxSpeed[0];
        if (boostLevel >= 5 && boostLevel < 10)
        {
            currentMaxSpeed = MaxSpeed[1];
        }

        if (boostLevel >= 10)
        {
            currentMaxSpeed = MaxSpeed[2];
        }


    }
}